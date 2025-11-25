using Microsoft.Data.Sqlite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using UglyToad.PdfPig;
using System.Linq;

namespace Ai_Study_Buddy___Gemini
{
    public partial class Dashboard : Form
    {
        private static readonly HttpClient httpClient = new();
        private readonly List<string> conversationHistory = new();
        private string? loadedPdfText;
        private string? loadedPdfPath;
        private readonly List<string> loadedPdfPaths = new();
        private readonly Dictionary<string, string> loadedPdfTextMap = new();
        private bool highlighterMode;
        private bool eraserMode;
        private Color selectedHighlightColor = Color.Yellow;
        private ContextMenuStrip chatContextMenu;
        private readonly DatabaseHelper dbHelper = new();
        private Panel modePanel = null!;
        private Panel modeHeaderPanel = null!;
        private Panel modeControlHost = null!;
        private Label modeTitleLabel = null!;
        private Label modeDescriptionLabel = null!;
        private Button buttonCollapseModes = null!;
        private GeminiApiClient geminiApi;
        private AppSettings _settings = new();
        private bool _sessionReminderShown;

        // Persistent mode controls
        private QuizModeControl? _quizControl;
        private MockExamControl? _mockControl;
        private CramModeControl? _cramControl;
        private FocusModeControl? _focusControl;
        private int? _quizContextSignature;
        private int? _mockContextSignature;

        public Dashboard()
        {
            Batteries.Init();
            InitializeComponent();
            LoadSettingsFromStorage();

            // Wire up PDF management UI buttons inside the Cram card
            var btnRemovePdf = new Button
            {
                Text = "Remove Selected",
                Size = new Size(150, 30),
                Location = new Point(20, 140),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRemovePdf.FlatAppearance.BorderSize = 1;
            btnRemovePdf.Click += (_, __) => RemoveSelectedPdf();
            panelCramCard.Controls.Add(btnRemovePdf);

            var btnClearPdfs = new Button
            {
                Text = "Clear All PDFs",
                Size = new Size(150, 30),
                Location = new Point(200, 140),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnClearPdfs.FlatAppearance.BorderSize = 1;
            btnClearPdfs.Click += (_, __) => ClearAllPdfs();
            panelCramCard.Controls.Add(btnClearPdfs);

            // Wire generate button on the Cram card to open Cram mode and trigger generate
            btnGenerateTopics.Click += async (_, __) => await GenerateTopicsFromCramCardAsync();

            geminiApi = new GeminiApiClient(httpClient, "AIzaSyA3QmEdnbzewFlffYGDu9LfVPrtl2DGSqw");

            buttonSettings.Click += buttonSettings_Click;

            InitializeModePanel();
            ToggleModePanel(false);

            chatHistoryBox.MouseUp += chatHistoryBox_MouseUp;

            chatContextMenu = new ContextMenuStrip();
            var addNoteItem = new ToolStripMenuItem("Add to Notes");
            addNoteItem.Click += AddToNotes_Click;
            chatContextMenu.Items.Add(addNoteItem);

            var removeHighlightItem = new ToolStripMenuItem("Remove Highlight");
            removeHighlightItem.Click += RemoveHighlight_Click;
            chatContextMenu.Items.Add(removeHighlightItem);

            chatHistoryBox.ContextMenuStrip = chatContextMenu;

            // Add eraser button next to highlighter
            var buttonEraser = new Button
            {
                Text = "🖍 Erase",
                Size = new Size(80, 36),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            buttonEraser.FlatAppearance.BorderSize = 0;
            buttonEraser.Click += (s, e) =>
            {
                eraserMode = !eraserMode;
                if (eraserMode)
                {
                    buttonEraser.BackColor = Color.LightGray;
                    highlighterMode = false;
                    buttonHighlighter.BackColor = Color.White;
                }
                else
                {
                    buttonEraser.BackColor = Color.White;
                }
            };
            // Assume adding to the same panel as buttonHighlighter
            if (buttonHighlighter.Parent != null)
            {
                // Add eraser and reposition both buttons to ensure visibility (move left of current highlighter)
                // compute Y based on highlighter
                int y = buttonHighlighter.Location.Y;
                // place eraser to the left of the highlighter and shift highlighter accordingly
                int spacing = 10;
                int eraserX = Math.Max(8, buttonHighlighter.Location.X - (buttonEraser.Width + spacing));
                buttonEraser.Location = new Point(eraserX, y);
                buttonHighlighter.Location = new Point(eraserX + buttonEraser.Width + spacing, y);
                buttonHighlighter.Parent.Controls.Add(buttonEraser);
            }

            // Add right-click color chooser for highlighter
            buttonHighlighter.MouseUp += buttonHighlighter_MouseUp;
        }

        private void SafeAppend(string txt)
        {
            if (chatHistoryBox == null) return;
            if (chatHistoryBox.InvokeRequired)
            {
                chatHistoryBox.Invoke(new Action(() => SafeAppend(txt)));
                return;
            }

            if (chatHistoryBox.TextLength > 0 && !chatHistoryBox.Text.EndsWith("\n"))
            {
                chatHistoryBox.AppendText("\n");
            }

            // Clean bullet formatting before passing to TextFormatter
            string cleanedText = CleanBulletFormatting(txt);

            // Delegate formatting to shared formatter (supports bullets, bold, headers)
            TextFormatter.AppendFormatted(chatHistoryBox, cleanedText);
        }

        private void LoadSettingsFromStorage()
        {
            int fontSize = 11;
            int.TryParse(dbHelper.LoadSetting("FontSize", "11"), out fontSize);

            bool autoSave = true;
            if (bool.TryParse(dbHelper.LoadSetting("AutoSave", "true"), out bool autoParsed))
                autoSave = autoParsed;

            bool notifications = false;
            if (bool.TryParse(dbHelper.LoadSetting("Notifications", "false"), out bool notifParsed))
                notifications = notifParsed;

            _settings = new AppSettings
            {
                AiName = dbHelper.LoadSetting("AiName", "Bud"),
                Theme = dbHelper.LoadSetting("Theme", "Light"),
                FontSize = fontSize,
                ResponseStyle = dbHelper.LoadSetting("ResponseStyle", "Detailed"),
                AutoSave = autoSave,
                NotificationsEnabled = notifications,
                DefaultMode = dbHelper.LoadSetting("DefaultMode", "Chat")
            };
        }

        private void ApplySettings(AppSettings updated)
        {
            _settings = updated.Clone();
            ApplyTheme(_settings.Theme);
            ApplyFontSize(_settings.FontSize);
            MaybeShowReminder();
        }

        private void ApplyTheme(string themeName)
        {
            bool dark = string.Equals(themeName, "Dark", StringComparison.OrdinalIgnoreCase);
            if (themeName.StartsWith("Auto", StringComparison.OrdinalIgnoreCase))
            {
                dark = SystemInformation.HighContrast;
            }

            Color appBg = dark ? Color.FromArgb(18, 18, 28) : Color.White;
            Color surfaceBg = dark ? Color.FromArgb(24, 25, 38) : Color.White;
            Color cardBg = dark ? Color.FromArgb(30, 31, 45) : Color.FromArgb(248, 249, 252);
            Color textColor = dark ? Color.Gainsboro : Color.FromArgb(45, 45, 45);

            BackColor = appBg;
            mainContentPanel.BackColor = appBg;
            SetControlColors(panelChat, surfaceBg, textColor);
            SetControlColors(panelCram, surfaceBg, textColor);
            SetControlColors(panelFocus, surfaceBg, textColor);
            SetControlColors(panelExam, surfaceBg, textColor);
            SetControlColors(panelQuiz, surfaceBg, textColor);
            SetControlColors(panelSettings, cardBg, textColor);
            if (modePanel != null) SetControlColors(modePanel, cardBg, textColor);
            if (modeControlHost != null) modeControlHost.BackColor = cardBg;
            if (modeHeaderPanel != null) modeHeaderPanel.BackColor = dark ? Color.FromArgb(35, 36, 50) : Color.White;

            if (chatHistoryBox != null)
            {
                chatHistoryBox.BackColor = dark ? Color.FromArgb(28, 29, 43) : Color.White;
                chatHistoryBox.ForeColor = textColor;
            }

            if (textBox1 != null)
            {
                textBox1.BackColor = dark ? Color.FromArgb(24, 24, 35) : Color.White;
                textBox1.ForeColor = textColor;
            }

            if (buttonSend != null)
            {
                buttonSend.BackColor = dark ? Color.FromArgb(99, 102, 241) : Color.FromArgb(79, 70, 229);
                buttonSend.ForeColor = Color.White;
            }

            if (buttonUpload != null)
            {
                buttonUpload.BackColor = dark ? Color.FromArgb(40, 41, 58) : Color.White;
                buttonUpload.ForeColor = textColor;
            }

            if (panelCramCard != null)
            {
                panelCramCard.BackColor = dark ? Color.FromArgb(32, 33, 48) : Color.WhiteSmoke;
                panelCramCard.ForeColor = textColor;
            }

            if (lblSettingsTitle != null)
            {
                lblSettingsTitle.ForeColor = dark ? Color.Gainsboro : Color.Gray;
            }
        }

        private static void SetControlColors(Control? ctrl, Color bg, Color fore)
        {
            if (ctrl == null) return;
            ctrl.BackColor = bg;
            ctrl.ForeColor = fore;
        }

        private void ApplyFontSize(int requestedSize)
        {
            int size = Math.Clamp(requestedSize, 9, 16);
            if (chatHistoryBox != null)
            {
                chatHistoryBox.Font = new Font(chatHistoryBox.Font.FontFamily, size);
            }
            if (textBox1 != null)
            {
                textBox1.Font = new Font(textBox1.Font.FontFamily, size);
            }
        }

        private void ApplyDefaultMode()
        {
            if (string.Equals(_settings.DefaultMode, "Chat", StringComparison.OrdinalIgnoreCase))
            {
                panelSettings.Visible = false;
                return;
            }

            if (string.Equals(_settings.DefaultMode, "Quiz Mode", StringComparison.OrdinalIgnoreCase))
            {
                ShowMode(StudyModeType.Quiz);
            }
            else if (string.Equals(_settings.DefaultMode, "Cram Mode", StringComparison.OrdinalIgnoreCase))
            {
                ShowMode(StudyModeType.Cram);
            }
            else if (string.Equals(_settings.DefaultMode, "Focus Mode", StringComparison.OrdinalIgnoreCase))
            {
                ShowMode(StudyModeType.Focus);
            }
            else if (string.Equals(_settings.DefaultMode, "Mock Exam", StringComparison.OrdinalIgnoreCase))
            {
                ShowMode(StudyModeType.MockExam);
            }
        }

        private void EnsureSettingsControl()
        {
            foreach (var ctrl in panelSettings.Controls.OfType<SettingsControl>().ToList())
            {
                panelSettings.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            panelSettings.Padding = new Padding(0, 80, 0, 20);

            var settingsControl = new SettingsControl(dbHelper, _settings.Clone(), OnSettingsApplied)
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(40)
            };

            panelSettings.Controls.Add(settingsControl);
            settingsControl.BringToFront();
            lblSettingsTitle.BringToFront();
        }

        private void OnSettingsApplied(AppSettings updated)
        {
            ApplySettings(updated);
            ApplyDefaultMode();
        }

        private string BuildResponseStyleInstruction()
        {
            return _settings.ResponseStyle switch
            {
                "Concise" => "Respond in 2-3 clear sentences focusing on the key takeaways.",
                "Conversational" => "Reply in a friendly, conversational tone and relate explanations to everyday scenarios when possible.",
                _ => "Provide a structured, detailed explanation with helpful examples when relevant."
            };
        }

        private void MaybeShowReminder()
        {
            if (_sessionReminderShown || !_settings.NotificationsEnabled)
                return;

            SafeAppend("🔔 Quick reminder: schedule short breaks every 25 minutes and log your study wins in Notes for later review.\n\n");
            _sessionReminderShown = true;
        }

        private void buttonSettings_Click(object? sender, EventArgs e)
        {
            if (panelSettings.Visible)
            {
                panelSettings.Visible = false;
                panelSettings.SendToBack();
            }
            else
            {
                ToggleModePanel(false);
                panelSettings.Visible = true;
                panelSettings.BringToFront();
                EnsureSettingsControl();
            }
        }

        private (string SourceText, string SourceLabel, bool HasMaterial) BuildStudyContext()
        {
            var sb = new StringBuilder();
            bool hasContent = false;

            if (conversationHistory.Count > 0)
            {
                sb.AppendLine("--- Chat History ---");
                sb.AppendLine(string.Join("\n", conversationHistory));
                hasContent = true;
            }

            if (!string.IsNullOrWhiteSpace(loadedPdfText))
            {
                sb.AppendLine("\n--- Reference PDF Content ---");
                sb.AppendLine(loadedPdfText);
                hasContent = true;
            }

            if (hasContent)
            {
                string label = "Based on Chat & PDF Context";
                if (conversationHistory.Count > 0 && string.IsNullOrEmpty(loadedPdfText))
                    label = "Based on Chat History";
                else if (conversationHistory.Count == 0 && !string.IsNullOrEmpty(loadedPdfText))
                {
                    if (loadedPdfPaths.Count <= 1)
                        label = $"Based on PDF: {Path.GetFileName(loadedPdfPath)}";
                    else
                        label = $"Based on PDFs: {string.Join(", ", loadedPdfPaths.Select(p => Path.GetFileName(p)))}";
                }

                return (sb.ToString(), label, true);
            }

            string assistantName = string.IsNullOrWhiteSpace(_settings.AiName) ? "your AI" : _settings.AiName;
            return (string.Empty, $"No study material yet. Upload a PDF or chat with {assistantName} to create context.", false);
        }

        private static int ComputeContextSignature((string SourceText, string SourceLabel, bool HasMaterial) context)
        {
            int sourceHash = context.SourceText == null ? 0 : StringComparer.Ordinal.GetHashCode(context.SourceText);
            int labelHash = context.SourceLabel == null ? 0 : StringComparer.Ordinal.GetHashCode(context.SourceLabel);
            return HashCode.Combine(context.HasMaterial, sourceHash, labelHash);
        }

        // Update the combo box in the Cram card with the loaded PDF filenames
        private void UpdateCramDocumentList()
        {
            if (comboCramSubject == null) return;
            comboCramSubject.Items.Clear();
            foreach (var path in loadedPdfPaths)
            {
                comboCramSubject.Items.Add(Path.GetFileName(path));
            }
            if (comboCramSubject.Items.Count > 0)
            {
                comboCramSubject.SelectedIndex = Math.Max(0, comboCramSubject.Items.Count - 1);
                comboCramSubject.Text = comboCramSubject.Items[comboCramSubject.SelectedIndex].ToString();
            }
            else
            {
                comboCramSubject.Text = "Choose a document...";
            }
        }

        private void RemoveSelectedPdf()
        {
            if (comboCramSubject == null) return;
            int idx = comboCramSubject.SelectedIndex;
            if (idx < 0 || idx >= loadedPdfPaths.Count) return;
            var path = loadedPdfPaths[idx];
            loadedPdfPaths.RemoveAt(idx);
            loadedPdfTextMap.Remove(path);

            // rebuild combined text
            var combined = new StringBuilder();
            for (int i = 0; i < loadedPdfPaths.Count; i++)
            {
                var p = loadedPdfPaths[i];
                if (!loadedPdfTextMap.TryGetValue(p, out var t)) continue;
                if (i > 0) combined.AppendLine().AppendLine($"--- Additional PDF: {Path.GetFileName(p)} ---");
                combined.AppendLine(t);
            }
            loadedPdfText = combined.ToString();
            if (loadedPdfPaths.Count > 0) loadedPdfPath = loadedPdfPaths[0]; else loadedPdfPath = null;
            UpdateCramDocumentList();
            SafeAppend($"🗑 Removed PDF: {Path.GetFileName(path)}\n");
            _cramControl?.RefreshContext();
        }

        private void ClearAllPdfs()
        {
            loadedPdfPaths.Clear();
            loadedPdfTextMap.Clear();
            loadedPdfText = null;
            loadedPdfPath = null;
            UpdateCramDocumentList();
            SafeAppend("🗑 Cleared all uploaded PDFs\n");
            _cramControl?.RefreshContext();
        }

        private async Task GenerateTopicsFromCramCardAsync()
        {
            // Ensure the cram panel/control is visible and up to date, then trigger generation
            ShowMode(StudyModeType.Cram);
            // Allow UI to create the control
            await Task.Delay(100);
            if (_cramControl != null)
            {
                await _cramControl.GenerateSummaryAsync();
            }
        }

        private void InitializeModePanel()
        {
            modePanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 400,
                BackColor = Color.WhiteSmoke,
                Visible = false
            };
            Controls.Add(modePanel);
            modePanel.BringToFront();

            modeHeaderPanel = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(20, 20, 20, 10),
                BackColor = Color.White
            };

            buttonCollapseModes = new Button
            {
                Text = "×",
                Width = 36,
                Height = 36,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(245, 245, 245),
                ForeColor = Color.DimGray,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            buttonCollapseModes.FlatAppearance.BorderSize = 0;
            buttonCollapseModes.Click += (_, __) => ToggleModePanel(false);

            modeTitleLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(79, 70, 229),
                Height = 35,
                Text = "Study Mode",
                TextAlign = ContentAlignment.MiddleLeft
            };

            modeDescriptionLabel = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(60, 60, 60),
                Margin = new Padding(0, 10, 0, 0),
                Text = "Select a mode to see details.",
                MaximumSize = new Size(360, 0)
            };

            modeHeaderPanel.Controls.Add(modeDescriptionLabel);
            modeHeaderPanel.Controls.Add(modeTitleLabel);
            modeHeaderPanel.Controls.Add(buttonCollapseModes);
            modeHeaderPanel.Resize += (_, __) => PositionCollapseButton();
            buttonCollapseModes.BringToFront();

            modeControlHost = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                BackColor = Color.FromArgb(248, 249, 252)
            };

            modePanel.Controls.Add(modeControlHost);
            modePanel.Controls.Add(modeHeaderPanel);
            modePanel.SizeChanged += (_, __) => UpdateModeInfoSizing();
            UpdateModeInfoSizing();
        }

        private void ToggleModePanel(bool visible)
        {
            if (modePanel.Visible == visible) return;
            modePanel.Visible = visible;
            mainContentPanel.Padding = visible ? new Padding(0, 0, modePanel.Width, 0) : Padding.Empty;
        }

        private void PositionCollapseButton()
        {
            if (buttonCollapseModes == null || modeHeaderPanel == null) return;
            buttonCollapseModes.Location = new Point(
                Math.Max(10, modeHeaderPanel.ClientSize.Width - buttonCollapseModes.Width - 10),
                10);
        }

        private void UpdateModeInfoSizing()
        {
            if (modeDescriptionLabel == null || modeHeaderPanel == null) return;
            int maxWidth = Math.Max(120, modeHeaderPanel.ClientSize.Width - 40);
            modeDescriptionLabel.MaximumSize = new Size(maxWidth, 0);
            modeHeaderPanel.PerformLayout();
            PositionCollapseButton();
        }

        // Send button
        private async void buttonSend_Click(object sender, EventArgs e)
        {
            string userInput = textBox1?.Text ?? "";
            if (string.IsNullOrWhiteSpace(userInput))
            {
                SafeAppend("⚠ Please enter a question!\n\n");
                return;
            }

            // UI Loading State
            buttonSend.Enabled = false;
            textBox1.Enabled = false;
            string originalText = buttonSend.Text;
            buttonSend.Text = "Sending...";

            try
            {
                string apiKey = "AIzaSyA3QmEdnbzewFlffYGDu9LfVPrtl2DGSqw";
                string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

                conversationHistory.Add($"user: {userInput}");
                string historyContext = string.Join(", ", conversationHistory);
                string pdfContext = string.IsNullOrEmpty(loadedPdfText)
                    ? "No PDF is loaded. Answer normally."
                    : $"Reference PDF content: {loadedPdfText}";
                string assistantName = string.IsNullOrWhiteSpace(_settings.AiName) ? "Bud" : _settings.AiName;
                string toneInstruction = BuildResponseStyleInstruction();
                string persona = $"You are an AI study buddy named {assistantName}. {toneInstruction}";

                string prompt = $"{persona}\nPrevious conversation: [{historyContext}], new message: user: {userInput}. {pdfContext}";

                var requestBody = new
                {
                    contents = new[]
                    {
                        new {
                            role = "user",
                            parts = new[] { new { text = prompt } }
                        }
                    }
                };

                string jsonBody = JsonSerializer.Serialize(requestBody);
                using var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content).ConfigureAwait(true);
                string responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

                if (!response.IsSuccessStatusCode)
                {
                    SafeAppend($"❌ API Error: {response.StatusCode}\n{responseText}\n\n");
                    return;
                }

                string? output = null;
                try
                {
                    using var doc = JsonDocument.Parse(responseText);
                    if (doc.RootElement.TryGetProperty("candidates", out var candidates) &&
                        candidates.ValueKind == JsonValueKind.Array &&
                        candidates.GetArrayLength() > 0)
                    {
                        var first = candidates[0];
                        if (first.TryGetProperty("content", out var contentEl) &&
                            contentEl.TryGetProperty("parts", out var partsEl) &&
                            partsEl.ValueKind == JsonValueKind.Array &&
                            partsEl.GetArrayLength() > 0)
                        {
                            var part0 = partsEl[0];
                            if (part0.TryGetProperty("text", out var textEl))
                                output = textEl.GetString();
                        }
                    }
                    output ??= "[No structured response]";
                }
                catch (Exception jsonEx)
                {
                    output = $"[Unparsed raw response]\n{responseText}\n(Parsing error: {jsonEx.Message})";
                }

                conversationHistory.Add($"ai: {output}");
                SafeAppend($"👤 User: {userInput}\n🤖 {assistantName}: {output}\n\n");
                textBox1?.Clear();
            }
            catch (Exception ex)
            {
                SafeAppend($"❌ Error: {ex.Message}\n\n");
            }
            finally
            {
                buttonSend.Enabled = true;
                textBox1.Enabled = true;
                buttonSend.Text = originalText;
                textBox1?.Focus();
            }
        }

        // Upload button
        private async void buttonUpload_Click(object sender, EventArgs e) => await LoadPdfAsync();

        private async Task LoadPdfAsync()
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "Select a PDF file"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            loadedPdfPath = ofd.FileName;
            loadedPdfPaths.Add(loadedPdfPath);

            // UI Loading State
            buttonUpload.Enabled = false;
            buttonSend.Enabled = false;
            textBox1.Enabled = false;
            string originalText = buttonUpload.Text;
            buttonUpload.Text = "Loading...";

            try
            {
                SafeAppend($"📄 Loading PDF: {Path.GetFileName(loadedPdfPath)}...\n");
                var sb = new StringBuilder();
                int totalImages = 0;
                int pageNum = 0;

                await Task.Run(async () =>
                {
                    using var document = PdfDocument.Open(loadedPdfPath!);
                    foreach (var page in document.GetPages())
                    {
                        pageNum++;
                        sb.AppendLine($"--- Page {pageNum} ---");
                        sb.AppendLine(page.Text);
                        sb.AppendLine();

                        var images = page.GetImages();
                        if (images != null && images.Any())
                        {
                            int imgIndex = 0;
                            foreach (var image in images)
                            {
                                imgIndex++;
                                totalImages++;
                                try
                                {
                                    byte[] bytes = image.RawBytes.ToArray();
                                    if (bytes.Length > 0)
                                    {
                                        string mime = DetectImageMimeType(bytes);
                                        string base64 = Convert.ToBase64String(bytes);
                                        string desc = await DescribeImageWithGemini(base64, mime);
                                        sb.AppendLine($"[Image {imgIndex} on Page {pageNum}: {desc}]");
                                    }
                                }
                                catch (Exception)
                                {
                                    // Ignore image errors during loading to keep UI clean
                                }
                            }
                        }
                    }
                });

                var newPdfText = sb.ToString();
                // store per-pdf text
                loadedPdfTextMap[loadedPdfPath] = newPdfText;

                // Rebuild combined loadedPdfText in the order of loadedPdfPaths
                var combined = new StringBuilder();
                for (int i = 0; i < loadedPdfPaths.Count; i++)
                {
                    var p = loadedPdfPaths[i];
                    if (!loadedPdfTextMap.TryGetValue(p, out var t)) continue;
                    if (i > 0) combined.AppendLine().AppendLine($"--- Additional PDF: {Path.GetFileName(p)} ---");
                    combined.AppendLine(t);
                }
                loadedPdfText = combined.ToString();

                SafeAppend($"✅ Loaded PDF: {Path.GetFileName(loadedPdfPath)}\n");
                UpdateCramDocumentList();
                _cramControl?.RefreshContext();
            }
            catch (Exception ex)
            {
                SafeAppend($"❌ Error loading PDF: {ex.Message}\n\n");
            }
            finally
            {
                buttonUpload.Enabled = true;
                buttonSend.Enabled = true;
                textBox1.Enabled = true;
                buttonUpload.Text = originalText;
            }
        }

        private string DetectImageMimeType(byte[] b)
        {
            if (b.Length >= 8 && b[0] == 0x89 && b[1] == 0x50 && b[2] == 0x4E && b[3] == 0x47) return "image/png";
            if (b.Length >= 2 && b[0] == 0xFF && b[1] == 0xD8) return "image/jpeg";
            if (b.Length >= 3 && b[0] == 0x47 && b[1] == 0x49 && b[2] == 0x46) return "image/gif";
            return "image/jpeg";
        }
        private static string CleanBulletFormatting(string markdown)
        {
            if (string.IsNullOrWhiteSpace(markdown)) return markdown;

            var lines = markdown.Split(new[] { '\r', '\n' }, StringSplitOptions.None);
            var result = new System.Text.StringBuilder();

            foreach (var line in lines)
            {
                var trimmed = line.TrimStart();

                // If line starts with "- " or "* ", remove it since TextFormatter adds bullets
                if (trimmed.StartsWith("- "))
                {
                    result.AppendLine(trimmed.Substring(2));
                }
                else if (trimmed.StartsWith("* "))
                {
                    result.AppendLine(trimmed.Substring(2));
                }
                else
                {
                    result.AppendLine(line);
                }
            }

            return result.ToString();
        }
        private async Task<string> DescribeImageWithGemini(string base64, string mime)
        {
            try
            {
                string apiKey = "AIzaSyA3QmEdnbzewFlffYGDu9LfVPrtl2DGSqw";
                string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";
                var body = new
                {
                    contents = new[]
                    {
                        new
                        {
                            role = "user",
                            parts = new object[]
                            {
                                new
                                {
                                    inlineData = new { mimeType = mime, data = base64 }
                                }
                            }
                        }
                    }
                };
                string json = JsonSerializer.Serialize(body);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = await httpClient.PostAsync(url, content).ConfigureAwait(false);
                string raw = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!resp.IsSuccessStatusCode) return $"API Error ({resp.StatusCode}): {raw}";
                try
                {
                    using var doc = JsonDocument.Parse(raw);
                    if (doc.RootElement.TryGetProperty("candidates", out var cands) &&
                        cands.ValueKind == JsonValueKind.Array && cands.GetArrayLength() > 0)
                    {
                        var first = cands[0];
                        if (first.TryGetProperty("content", out var contentEl) &&
                            contentEl.TryGetProperty("parts", out var partsEl) &&
                            partsEl.ValueKind == JsonValueKind.Array && partsEl.GetArrayLength() > 0)
                        {
                            var p0 = partsEl[0];
                            if (p0.TryGetProperty("text", out var textEl))
                                return textEl.GetString() ?? "No text";
                        }
                    }
                    return "No description found";
                }
                catch (Exception parseEx)
                {
                    return $"Parse error: {parseEx.Message}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (panelChatInput == null) return;
            int margin = 10;
            var size = TextRenderer.MeasureText(
                textBox1.Text + " ",
                textBox1.Font,
                new Size(textBox1.Width, int.MaxValue),
                TextFormatFlags.WordBreak);
            int newHeight = size.Height + margin;

            // Ensure minimum height matches the buttons (60px)
            if (newHeight < 60) newHeight = 60;

            // Cap maximum height
            if (newHeight > 200) newHeight = 200;

            // Add padding: panelChatInput (44) + panelInputContainer (24) = 68
            panelChatInput.Height = newHeight + 68;
        }

        private void buttonClearMemory_Click(object sender, EventArgs e)
        {
            conversationHistory.Clear();
            SafeAppend("🧹 Memory cleared!\n\n");
        }

        private void ShowMode(StudyModeType mode)
        {
            ToggleModePanel(true);

            var (title, description) = GetModeDetails(mode);
            modeTitleLabel.Text = title;
            modeDescriptionLabel.Text = description;
            UpdateModeInfoSizing();

            var studyContext = BuildStudyContext();
            int contextSignature = ComputeContextSignature(studyContext);
            modeControlHost.Controls.Clear();

            Control control;
            switch (mode)
            {
                case StudyModeType.Quiz:
                    if (_quizControl == null || _quizContextSignature != contextSignature)
                    {
                        _quizControl = new QuizModeControl(geminiApi, studyContext.SourceText, studyContext.SourceLabel, studyContext.HasMaterial);
                        _quizContextSignature = contextSignature;
                    }
                    control = _quizControl;
                    break;
                case StudyModeType.MockExam:
                    if (_mockControl == null || _mockContextSignature != contextSignature)
                    {
                        _mockControl = new MockExamControl(geminiApi, studyContext.SourceText, studyContext.SourceLabel, studyContext.HasMaterial);
                        _mockContextSignature = contextSignature;
                    }
                    control = _mockControl;
                    break;
                case StudyModeType.Cram:
                    if (_cramControl == null)
                        _cramControl = new CramModeControl(geminiApi, BuildStudyContext);
                    _cramControl.RefreshContext();
                    control = _cramControl;
                    break;
                case StudyModeType.Focus:
                    if (_focusControl == null)
                        _focusControl = new FocusModeControl();
                    control = _focusControl;
                    break;
                default:
                    control = new Label { Text = "Unknown mode", Dock = DockStyle.Fill };
                    break;
            }

            control.Dock = DockStyle.Fill;
            modeControlHost.Controls.Add(control);

            if (!studyContext.HasMaterial && mode != StudyModeType.Focus)
                SafeAppend("ℹ️ Upload a handout or build a chat conversation to power this mode.\n\n");
        }

        private (string Title, string Description) GetModeDetails(StudyModeType mode)
        {
            return mode switch
            {
                StudyModeType.Quiz => ("❓ Quiz Mode",
                    "Purpose: Quick knowledge testing and reinforcement.\nUse case: Fast Q&A sessions on specific topics without pressure.\nFeatures: Instant feedback, multiple question types, highlights weaknesses quickly.\nBest for: Daily review, quick checks after studying, casual self-assessment."),
                StudyModeType.MockExam => ("📝 Mock Exam Mode",
                    "Purpose: Full exam simulation for serious practice.\nUse case: Prepare for real exams under realistic, timed conditions.\nFeatures: Timers, adjustable difficulty, section organization, flag/review options, scoring.\nBest for: Final prep, building stamina, practicing time management."),
                StudyModeType.Cram => ("📚 Cram Mode",
                    "Purpose: Quick review via organized summaries.\nUse case: Review large material fast, especially before exams.\nFeatures: AI-generated summaries grouped by key concepts.\nBest for: Last-minute review, topic overviews, memory refreshers."),
                StudyModeType.Focus => ("⏱ Focus Mode",
                    "Purpose: Deep study sessions balanced with breaks.\nUse case: Maintain concentration with structure and healthy pacing.\nFeatures: Pomodoro cycles, optional text-to-speech for hands-free learning.\nBest for: Long sessions, preventing burnout, auditory learners, productivity boosts."),
                _ => ("Study Mode", "Select a mode to see its purpose and tools.")
            };
        }

        private void QuizzMode(object sender, EventArgs e)
        {
            ShowMode(StudyModeType.Quiz);
        }

        private void CramMode(object sender, EventArgs e)
        {
            ShowMode(StudyModeType.Cram);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowMode(StudyModeType.Focus);
        }

        private void MockExamMode(object sender, EventArgs e)
        {
            ShowMode(StudyModeType.MockExam);
        }

        // Highlighter (Designer wires to buttonHighlighter_Click)
        private void buttonHighlighter_Click(object sender, EventArgs e)
        {
            highlighterMode = !highlighterMode;
            if (highlighterMode)
            {
                buttonHighlighter.BackColor = selectedHighlightColor;
                eraserMode = false; // turn off eraser
            }
            else
            {
                buttonHighlighter.BackColor = Color.White;
            }
        }

        private void buttonHighlighter_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                using (ColorDialog cd = new ColorDialog())
                {
                    cd.Color = selectedHighlightColor;
                    if (cd.ShowDialog() == DialogResult.OK)
                    {
                        selectedHighlightColor = cd.Color;
                        if (highlighterMode)
                        {
                            buttonHighlighter.BackColor = selectedHighlightColor;
                        }
                    }
                }
            }
        }

        private void chatHistoryBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (highlighterMode && chatHistoryBox.SelectionLength > 0)
            {
                chatHistoryBox.SelectionBackColor = selectedHighlightColor;
                if (_settings.AutoSave)
                {
                    string selected = chatHistoryBox.SelectedText.Trim();
                    if (!string.IsNullOrWhiteSpace(selected))
                    {
                        dbHelper.AddNote(selected);
                    }
                }
            }
            else if (eraserMode && chatHistoryBox.SelectionLength > 0)
            {
                chatHistoryBox.SelectionBackColor = chatHistoryBox.BackColor;
            }
        }

        private void AddToNotes_Click(object? sender, EventArgs e)
        {
            if (chatHistoryBox.SelectionLength <= 0) return;
            string selected = chatHistoryBox.SelectedText.Trim();
            if (string.IsNullOrEmpty(selected)) return;
            dbHelper.AddNote(selected);
            SafeAppend($"📝 Saved note: \"{selected}\"\n\n");
        }

        private void RemoveHighlight_Click(object? sender, EventArgs e)
        {
            if (chatHistoryBox.SelectionLength > 0)
                chatHistoryBox.SelectionBackColor = chatHistoryBox.BackColor;
        }

        private void chatHistoryBox_TextChanged(object sender, EventArgs e) { }
    }

    public class DatabaseHelper
    {
        private const string DbFile = "studybuddydb.db";

        public DatabaseHelper()
        {
            if (!File.Exists(DbFile))
            {
                MessageBox.Show(
                    $"Database file not found.\nA new one will be created at:\n{Path.GetFullPath(DbFile)}",
                    "Database Created",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                File.Create(DbFile).Dispose();
            }

            using var conn = new SqliteConnection($"Data Source={DbFile}");
            conn.Open();
            string createTable = @"
                CREATE TABLE IF NOT EXISTS Notes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    NoteText TEXT NOT NULL,
                    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                );
                CREATE TABLE IF NOT EXISTS Settings (
                    Key TEXT PRIMARY KEY,
                    Value TEXT NOT NULL
                );";
            using var cmd = new SqliteCommand(createTable, conn);
            cmd.ExecuteNonQuery();
        }

        public void AddNote(string text)
        {
            using var conn = new SqliteConnection($"Data Source={DbFile}");
            conn.Open();
            using var cmd = new SqliteCommand("INSERT INTO Notes (NoteText) VALUES (@text)", conn);
            cmd.Parameters.AddWithValue("@text", text);
            cmd.ExecuteNonQuery();
        }

        public void SaveSetting(string key, string value)
        {
            using var conn = new SqliteConnection($"Data Source={DbFile}");
            conn.Open();
            using var cmd = new SqliteCommand("INSERT OR REPLACE INTO Settings (Key, Value) VALUES (@key, @value)", conn);
            cmd.Parameters.AddWithValue("@key", key);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.ExecuteNonQuery();
        }

        public string LoadSetting(string key, string defaultValue = "")
        {
            using var conn = new SqliteConnection($"Data Source={DbFile}");
            conn.Open();
            using var cmd = new SqliteCommand("SELECT Value FROM Settings WHERE Key = @key", conn);
            cmd.Parameters.AddWithValue("@key", key);
            var result = cmd.ExecuteScalar();
            return result?.ToString() ?? defaultValue;
        }
    }
}