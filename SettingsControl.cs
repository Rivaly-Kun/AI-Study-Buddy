using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ai_Study_Buddy___Gemini
{
    public class SettingsControl : UserControl
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly Action<AppSettings>? _onSettingsApplied;
        private AppSettings _settings;

        // UI Controls - Appearance Section
        private Panel _appearancePanel = null!;
        private Label _lblAppearanceHeader = null!;
        private Label _lblTheme = null!;
        private ComboBox _cmbTheme = null!;
        private Label _lblFontSize = null!;
        private TrackBar _trackFontSize = null!;
        private Label _lblFontSizeValue = null!;

        // UI Controls - AI Assistant Section
        private Panel _aiPanel = null!;
        private Label _lblAiHeader = null!;
        private Label _lblAiName = null!;
        private TextBox _txtAiName = null!;
        private Label _lblResponseStyle = null!;
        private ComboBox _cmbResponseStyle = null!;

        // UI Controls - Study Preferences Section
        private Panel _studyPanel = null!;
        private Label _lblStudyHeader = null!;
        private CheckBox _chkAutoSave = null!;
        private CheckBox _chkNotifications = null!;
        private Label _lblDefaultMode = null!;
        private ComboBox _cmbDefaultMode = null!;

        // Footer with Save Button
        private Panel _footerPanel = null!;
        private Button _btnSave = null!;
        private Button _btnReset = null!;

        public SettingsControl(DatabaseHelper dbHelper, AppSettings settings, Action<AppSettings>? onSettingsApplied)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _settings = settings?.Clone() ?? new AppSettings();
            _onSettingsApplied = onSettingsApplied;
            BuildUi();
            LoadSettings();
        }

        private void BuildUi()
        {
            SuspendLayout();

            // Main container properties
            AutoScroll = true;
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(248, 249, 252);
            Padding = new Padding(30, 20, 30, 20);

            // ===== APPEARANCE SECTION =====
            _appearancePanel = CreateSectionPanel(0);

            _lblAppearanceHeader = new Label
            {
                Text = "🎨 Appearance",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(79, 70, 229),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            _lblTheme = new Label
            {
                Text = "Theme:",
                Location = new Point(20, 60),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            _cmbTheme = new ComboBox
            {
                Location = new Point(20, 85),
                Width = 200,
                Font = new Font("Segoe UI", 9.5F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _cmbTheme.Items.AddRange(new object[] { "Light", "Dark", "Auto (System)" });
            _cmbTheme.SelectedIndex = 0;

            _lblFontSize = new Label
            {
                Text = "Font Size:",
                Location = new Point(20, 125),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            _trackFontSize = new TrackBar
            {
                Location = new Point(20, 150),
                Width = 200,
                Minimum = 8,
                Maximum = 16,
                Value = 11,
                TickFrequency = 2
            };
            _trackFontSize.ValueChanged += (s, e) =>
            {
                _lblFontSizeValue.Text = $"{_trackFontSize.Value}pt";
            };

            _lblFontSizeValue = new Label
            {
                Text = "11pt",
                Location = new Point(230, 165),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray
            };

            _appearancePanel.Controls.AddRange(new Control[] {
                _lblAppearanceHeader, _lblTheme, _cmbTheme,
                _lblFontSize, _trackFontSize, _lblFontSizeValue
            });

            // ===== AI ASSISTANT SECTION =====
            _aiPanel = CreateSectionPanel(220);

            _lblAiHeader = new Label
            {
                Text = "🤖 AI Assistant",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(79, 70, 229),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            _lblAiName = new Label
            {
                Text = "Assistant Name:",
                Location = new Point(20, 60),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            _txtAiName = new TextBox
            {
                Location = new Point(20, 85),
                Width = 200,
                Font = new Font("Segoe UI", 9.5F),
                Text = "Bud"
            };

            _lblResponseStyle = new Label
            {
                Text = "Response Style:",
                Location = new Point(20, 125),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            _cmbResponseStyle = new ComboBox
            {
                Location = new Point(20, 150),
                Width = 200,
                Font = new Font("Segoe UI", 9.5F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _cmbResponseStyle.Items.AddRange(new object[] {
                "Detailed",
                "Concise",
                "Conversational"
            });
            _cmbResponseStyle.SelectedIndex = 0;

            _aiPanel.Controls.AddRange(new Control[] {
                _lblAiHeader, _lblAiName, _txtAiName,
                _lblResponseStyle, _cmbResponseStyle
            });

            // ===== STUDY PREFERENCES SECTION =====
            _studyPanel = CreateSectionPanel(440);

            _lblStudyHeader = new Label
            {
                Text = "📚 Study Preferences",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(79, 70, 229),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            _chkAutoSave = new CheckBox
            {
                Text = "Auto-save notes and highlights",
                Location = new Point(20, 60),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(60, 60, 60),
                Checked = true
            };

            _chkNotifications = new CheckBox
            {
                Text = "Enable study reminders",
                Location = new Point(20, 90),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            _lblDefaultMode = new Label
            {
                Text = "Default Study Mode:",
                Location = new Point(20, 125),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            _cmbDefaultMode = new ComboBox
            {
                Location = new Point(20, 150),
                Width = 200,
                Font = new Font("Segoe UI", 9.5F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _cmbDefaultMode.Items.AddRange(new object[] {
                "Chat",
                "Quiz Mode",
                "Cram Mode",
                "Focus Mode",
                "Mock Exam"
            });
            _cmbDefaultMode.SelectedIndex = 0;

            _studyPanel.Controls.AddRange(new Control[] {
                _lblStudyHeader, _chkAutoSave, _chkNotifications,
                _lblDefaultMode, _cmbDefaultMode
            });

            // ===== FOOTER WITH BUTTONS =====
            _footerPanel = new Panel
            {
                BackColor = Color.White,
                Height = 80,
                Dock = DockStyle.Bottom,
                Padding = new Padding(30, 20, 30, 20)
            };

            _btnSave = new Button
            {
                Text = "💾 Save Settings",
                Size = new Size(140, 40),
                BackColor = Color.FromArgb(79, 70, 229),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Location = new Point(30, 20)
            };
            _btnSave.FlatAppearance.BorderSize = 0;
            _btnSave.Click += BtnSave_Click;

            _btnReset = new Button
            {
                Text = "🔄 Reset to Defaults",
                Size = new Size(140, 40),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(79, 70, 229),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Location = new Point(180, 20)
            };
            _btnReset.FlatAppearance.BorderColor = Color.FromArgb(79, 70, 229);
            _btnReset.Click += BtnReset_Click;

            _footerPanel.Controls.AddRange(new Control[] { _btnSave, _btnReset });

            // Add all sections to main control
            Controls.Add(_appearancePanel);
            Controls.Add(_aiPanel);
            Controls.Add(_studyPanel);
            Controls.Add(_footerPanel);

            ResumeLayout(false);
        }

        private Panel CreateSectionPanel(int topPosition)
        {
            return new Panel
            {
                BackColor = Color.White,
                Location = new Point(0, topPosition),
                Size = new Size(600, 200),
                Padding = new Padding(0),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
        }

        private void LoadSettings()
        {
            _txtAiName.Text = _settings.AiName;
            SelectComboValue(_cmbTheme, _settings.Theme, 0);
            _trackFontSize.Value = Math.Max(_trackFontSize.Minimum, Math.Min(_trackFontSize.Maximum, _settings.FontSize));
            _lblFontSizeValue.Text = $"{_trackFontSize.Value}pt";
            SelectComboValue(_cmbResponseStyle, _settings.ResponseStyle, 0);
            _chkAutoSave.Checked = _settings.AutoSave;
            _chkNotifications.Checked = _settings.NotificationsEnabled;
            SelectComboValue(_cmbDefaultMode, _settings.DefaultMode, 0);
        }

        private static void SelectComboValue(ComboBox combo, string? value, int fallbackIndex)
        {
            if (combo.Items.Count == 0)
            {
                combo.SelectedIndex = -1;
                return;
            }

            if (!string.IsNullOrWhiteSpace(value))
            {
                int index = combo.Items.IndexOf(value);
                if (index >= 0)
                {
                    combo.SelectedIndex = index;
                    return;
                }
            }

            combo.SelectedIndex = Math.Clamp(fallbackIndex, 0, combo.Items.Count - 1);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var sanitizedName = string.IsNullOrWhiteSpace(_txtAiName.Text)
                ? "Bud"
                : _txtAiName.Text.Trim();

            var updated = new AppSettings
            {
                AiName = sanitizedName,
                Theme = _cmbTheme.SelectedItem?.ToString() ?? "Light",
                FontSize = _trackFontSize.Value,
                ResponseStyle = _cmbResponseStyle.SelectedItem?.ToString() ?? "Detailed",
                AutoSave = _chkAutoSave.Checked,
                NotificationsEnabled = _chkNotifications.Checked,
                DefaultMode = _cmbDefaultMode.SelectedItem?.ToString() ?? "Chat"
            };

            _dbHelper.SaveSetting("AiName", updated.AiName);
            _dbHelper.SaveSetting("Theme", updated.Theme);
            _dbHelper.SaveSetting("FontSize", updated.FontSize.ToString());
            _dbHelper.SaveSetting("ResponseStyle", updated.ResponseStyle);
            _dbHelper.SaveSetting("AutoSave", updated.AutoSave.ToString());
            _dbHelper.SaveSetting("Notifications", updated.NotificationsEnabled.ToString());
            _dbHelper.SaveSetting("DefaultMode", updated.DefaultMode);

            _settings = updated.Clone();
            _onSettingsApplied?.Invoke(updated.Clone());

            MessageBox.Show(
                "Settings saved successfully!\n\nChanges that affect appearance are applied immediately.",
                "Settings Saved",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to reset all settings to defaults?",
                "Reset Settings",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Reset to defaults
                _txtAiName.Text = "Bud";
                _cmbTheme.SelectedIndex = 0;
                _trackFontSize.Value = 11;
                _lblFontSizeValue.Text = "11pt";
                _cmbResponseStyle.SelectedIndex = 0;
                _chkAutoSave.Checked = true;
                _chkNotifications.Checked = false;
                _cmbDefaultMode.SelectedIndex = 0;

                MessageBox.Show(
                    "Settings have been reset to defaults.\nClick 'Save Settings' to apply changes.",
                    "Reset Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
    }
}