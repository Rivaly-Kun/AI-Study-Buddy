using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ai_Study_Buddy___Gemini
{
    public class CramModeControl : UserControl
    {
        private readonly GeminiApiClient _api;
        private readonly Func<(string SourceText, string ContextLabel, bool HasMaterial)> _contextProvider;
        private string _currentSource = string.Empty;
        private string _contextLabel = string.Empty;
        private bool _hasMaterial;

        // UI Controls
        private Button _generateBtn;
        private Button _exportBtn;
        private Panel _summaryContainer;
        private RichTextBox _summaryBox;
        private Label _contextBadge;
        private Panel _emptyStatePanel;
        private ProgressBar _progressBar;

        public CramModeControl(
            GeminiApiClient apiClient,
            Func<(string SourceText, string ContextLabel, bool HasMaterial)> contextProvider)
        {
            _api = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));
            RefreshContextValues();
            BuildUi();
        }

        public void RefreshContext()
        {
            RefreshContextValues();
            ApplyContextToUi();
        }

        public async Task GenerateSummaryAsync()
        {
            await GenerateAsync();
        }

        private void BuildUi()
        {
            SuspendLayout();

            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(248, 249, 252);
            Padding = new Padding(0);

            // Compact header with context badge
            var headerPanel = CreateHeaderPanel();

            // Main content area - summary or empty state
            _summaryContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(12),
                AutoScroll = true
            };

            // Summary display (hidden initially)
            _summaryBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9.5F),
                ScrollBars = RichTextBoxScrollBars.Vertical,
                Visible = false
            };

            // Empty state (shown when no summary)
            _emptyStatePanel = CreateEmptyStatePanel();

            // Progress bar (hidden by default)
            _progressBar = new ProgressBar
            {
                Dock = DockStyle.Bottom,
                Height = 3,
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 20,
                Visible = false
            };

            // Action buttons footer
            var footerPanel = CreateFooterPanel();

            // Assembly
            _summaryContainer.Controls.Add(_summaryBox);
            _summaryContainer.Controls.Add(_emptyStatePanel);

            Controls.Add(_summaryContainer);
            Controls.Add(_progressBar);
            Controls.Add(footerPanel);
            Controls.Add(headerPanel);

            ResumeLayout();
        }

        private Panel CreateHeaderPanel()
        {
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45,
                BackColor = Color.FromArgb(79, 70, 229),
                Padding = new Padding(12, 8, 12, 8)
            };

            _contextBadge = new Label
            {
                Dock = DockStyle.Fill,
                Text = _contextLabel,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };

            var statusIcon = new Label
            {
                Dock = DockStyle.Left,
                Width = 24,
                Text = _hasMaterial ? "📄" : "⚠️",
                Font = new Font("Segoe UI", 12F),
                TextAlign = ContentAlignment.MiddleCenter
            };

            header.Controls.Add(_contextBadge);
            header.Controls.Add(statusIcon);

            return header;
        }

        private Panel CreateEmptyStatePanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Visible = true
            };

            var iconLabel = new Label
            {
                Text = "📚",
                Font = new Font("Segoe UI", 48F),
                ForeColor = Color.FromArgb(200, 200, 210),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var messageLabel = new Label
            {
                Text = "No summary yet",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 120),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var hintLabel = new Label
            {
                Text = _hasMaterial
                    ? "Click 'Generate Summary' below to create a study guide"
                    : "Upload a PDF or chat with Bud first",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                AutoSize = false,
                Width = 280,
                Height = 60,
                TextAlign = ContentAlignment.TopCenter
            };

            panel.Resize += (s, e) =>
            {
                int centerX = panel.Width / 2;
                int centerY = panel.Height / 2;

                iconLabel.Location = new Point(centerX - iconLabel.Width / 2, centerY - 80);
                messageLabel.Location = new Point(centerX - messageLabel.Width / 2, centerY - 10);
                hintLabel.Location = new Point(centerX - hintLabel.Width / 2, centerY + 20);
            };

            panel.Controls.Add(iconLabel);
            panel.Controls.Add(messageLabel);
            panel.Controls.Add(hintLabel);

            return panel;
        }

        private Panel CreateFooterPanel()
        {
            var footer = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 52,
                BackColor = Color.FromArgb(248, 249, 252),
                Padding = new Padding(8, 8, 8, 8)
            };

            _generateBtn = new Button
            {
                Dock = DockStyle.Fill,
                Text = "📝 Generate Summary",
                Height = 36,
                Enabled = _hasMaterial,
                BackColor = Color.FromArgb(79, 70, 229),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            _generateBtn.FlatAppearance.BorderSize = 0;
            _generateBtn.Click += async (_, __) => await GenerateAsync();

            _exportBtn = new Button
            {
                Dock = DockStyle.Right,
                Width = 36,
                Height = 36,
                Text = "💾",
                BackColor = Color.White,
                ForeColor = Color.FromArgb(79, 70, 229),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F),
                Cursor = Cursors.Hand,
                Visible = false,
                Margin = new Padding(8, 0, 0, 0)
            };
            _exportBtn.FlatAppearance.BorderColor = Color.FromArgb(220, 220, 230);
            _exportBtn.FlatAppearance.BorderSize = 1;
            _exportBtn.Click += ExportSummary;

            var exportWrapper = new Panel
            {
                Dock = DockStyle.Right,
                Width = 44,
                Padding = new Padding(8, 0, 0, 0)
            };
            exportWrapper.Controls.Add(_exportBtn);

            footer.Controls.Add(_generateBtn);
            footer.Controls.Add(exportWrapper);

            return footer;
        }

        private async Task GenerateAsync()
        {
            RefreshContext();

            if (!_hasMaterial || string.IsNullOrWhiteSpace(_currentSource))
            {
                ShowEmptyState("Add study material first", "Upload a PDF or chat with Bud to generate summaries");
                return;
            }

            // Show loading state
            _emptyStatePanel.Visible = false;
            _summaryBox.Visible = false;
            _progressBar.Visible = true;
            _generateBtn.Enabled = false;
            _generateBtn.Text = "Generating...";

            try
            {
                string sanitized = SanitizeSource(_currentSource);
                string prompt = $@"You are Bud, a concise study partner. Read the context below and produce a structured study summary.

Rules:
- Organize output into 3-6 sections with '###' headings.
- Under each heading, use short bullet points that paraphrase the ideas (no copy-paste).
- Highlight key terms or definitions in **bold**.
- Keep bullets concise (1-2 lines max).
- Ignore page markers, repeated strings, and filler headings like '--- Reference PDF Content ---'.
- If the context is long, focus on key themes, processes, roles, and takeaways.
- Never return the raw context verbatim.

Context:
````text
{sanitized}
```";

                var summary = await _api.GenerateTextAsync(prompt);

                // Display the summary
                _summaryBox.Clear();

                // Clean up the summary to remove duplicate bullets
                string cleanedSummary = CleanBulletFormatting(summary);
                TextFormatter.AppendFormatted(_summaryBox, cleanedSummary);

                _summaryBox.Visible = true;
                _emptyStatePanel.Visible = false;
                _exportBtn.Visible = true;
                _generateBtn.Text = "🔄 Regenerate";
            }
            catch (Exception ex)
            {
                ShowEmptyState("Generation failed", $"Error: {ex.Message}");
                _exportBtn.Visible = false;
            }
            finally
            {
                _progressBar.Visible = false;
                _generateBtn.Enabled = _hasMaterial;
            }
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

        private void ShowEmptyState(string title, string message)
        {
            _emptyStatePanel.Visible = true;
            _summaryBox.Visible = false;

            var messageLabel = _emptyStatePanel.Controls.OfType<Label>()
                .FirstOrDefault(l => l.Font.Bold);
            var hintLabel = _emptyStatePanel.Controls.OfType<Label>()
                .LastOrDefault();

            if (messageLabel != null) messageLabel.Text = title;
            if (hintLabel != null) hintLabel.Text = message;
        }

        private void ExportSummary(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_summaryBox.Text)) return;

            using var sfd = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf",
                DefaultExt = "txt",
                FileName = $"Study_Summary_{DateTime.Now:yyyyMMdd_HHmmss}"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (sfd.FileName.EndsWith(".rtf", StringComparison.OrdinalIgnoreCase))
                    {
                        _summaryBox.SaveFile(sfd.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        System.IO.File.WriteAllText(sfd.FileName, _summaryBox.Text);
                    }

                    MessageBox.Show("Summary exported successfully!", "Export Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to export: {ex.Message}", "Export Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static string SanitizeSource(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            var lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                             .Where(l => !l.StartsWith("--- Reference", StringComparison.OrdinalIgnoreCase))
                             .Where(l => !l.StartsWith("--- Additional", StringComparison.OrdinalIgnoreCase))
                             .Where(l => !l.StartsWith("--- Page", StringComparison.OrdinalIgnoreCase))
                             .Select(l => l.Trim())
                             .Where(l => !string.IsNullOrWhiteSpace(l));

            var cleaned = string.Join(" ", lines);
            const int maxLen = 6000;
            if (cleaned.Length > maxLen)
                cleaned = cleaned.Substring(0, maxLen) + " ...";
            return cleaned;
        }

        private void RefreshContextValues()
        {
            if (_contextProvider == null)
            {
                _currentSource = string.Empty;
                _contextLabel = "No study material";
                _hasMaterial = false;
                return;
            }

            var ctx = _contextProvider.Invoke();
            _currentSource = ctx.SourceText ?? string.Empty;
            _contextLabel = string.IsNullOrWhiteSpace(ctx.ContextLabel)
                ? "No study material"
                : ctx.ContextLabel;
            _hasMaterial = ctx.HasMaterial && !string.IsNullOrWhiteSpace(_currentSource);
        }

        private void ApplyContextToUi()
        {
            if (_generateBtn != null)
            {
                _generateBtn.Enabled = _hasMaterial;
                _generateBtn.Text = _summaryBox.Visible ? "🔄 Regenerate" : "📝 Generate Summary";
            }

            if (_contextBadge != null)
            {
                _contextBadge.Text = _contextLabel;
            }

            // Update empty state message
            if (_emptyStatePanel != null && _emptyStatePanel.Visible)
            {
                var hintLabel = _emptyStatePanel.Controls.OfType<Label>().LastOrDefault();
                if (hintLabel != null)
                {
                    hintLabel.Text = _hasMaterial
                        ? "Click 'Generate Summary' below to create a study guide"
                        : "Upload a PDF or chat with Bud first";
                }
            }
        }
    }
}
