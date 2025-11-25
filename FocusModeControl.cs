using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Ai_Study_Buddy___Gemini
{
    public class FocusModeControl : UserControl
    {
        // Core timer controls
        private Label _timerLabel = null!;
        private Label _statusLabel = null!;
        private Button _startBtn = null!;
        private Button _resetBtn = null!;
        private Panel _timerCircle = null!;

        // Settings controls
        private NumericUpDown _workMinutes = null!;
        private NumericUpDown _breakMinutes = null!;
        private CheckBox _autoStartBreaks = null!;
        private CheckBox _soundEnabled = null!;

        // Stats
        private Panel _sessionsLabel = null!;
        private Panel _totalTimeLabel = null!;
        private Label _sessionsValueLabel = null!;
        private Label _totalTimeValueLabel = null!;

        // State
        private System.Windows.Forms.Timer _timer = null!;
        private int _secondsRemaining;
        private int _totalSecondsInSession;
        private bool _onBreak;
        private int _completedSessions;
        private int _totalMinutesWorked;
        private bool _isRunning;

        public FocusModeControl()
        {
            BuildUi();
        }

        private void BuildUi()
        {
            SuspendLayout();

            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(248, 249, 252);
            Padding = new Padding(0);
            AutoScroll = true;

            // Header with mode indicator
            var header = CreateHeader();

            // Main timer display
            var timerSection = CreateTimerSection();

            // Control buttons
            var controlsSection = CreateControlsSection();

            // Stats section
            var statsSection = CreateStatsSection();

            // Settings section (collapsible)
            var settingsSection = CreateSettingsSection();

            // Quick tips
            var tipsSection = CreateTipsSection();

            // Assembly
            Controls.Add(tipsSection);
            Controls.Add(settingsSection);
            Controls.Add(statsSection);
            Controls.Add(controlsSection);
            Controls.Add(timerSection);
            Controls.Add(header);

            // Initialize timer
            _timer = new System.Windows.Forms.Timer { Interval = 1000 };
            _timer.Tick += OnTimerTick;

            InitializeSession();
            ResumeLayout();
        }

        private Panel CreateHeader()
        {
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(234, 88, 12),
                Padding = new Padding(16, 12, 16, 12)
            };

            var modeIcon = new Label
            {
                Dock = DockStyle.Left,
                Width = 30,
                Text = "⏱️",
                Font = new Font("Segoe UI", 14F),
                TextAlign = ContentAlignment.MiddleLeft
            };

            _statusLabel = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Ready to focus",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            header.Controls.Add(_statusLabel);
            header.Controls.Add(modeIcon);

            return header;
        }

        private Panel CreateTimerSection()
        {
            var section = new Panel
            {
                Dock = DockStyle.Top,
                Height = 220,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Circular timer display
            _timerCircle = new Panel
            {
                Width = 180,
                Height = 180,
                BackColor = Color.FromArgb(255, 247, 237)
            };
            _timerCircle.Paint += DrawCircularTimer;

            _timerLabel = new Label
            {
                Text = "25:00",
                Font = new Font("Segoe UI", 36F, FontStyle.Bold),
                ForeColor = Color.FromArgb(234, 88, 12),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var phaseLabel = new Label
            {
                Text = "FOCUS TIME",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(150, 150, 160),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            section.Resize += (s, e) =>
            {
                int centerX = section.Width / 2;
                _timerCircle.Location = new Point(centerX - 90, 20);
                _timerLabel.Location = new Point(
                    centerX - _timerLabel.Width / 2,
                    80
                );
                phaseLabel.Location = new Point(
                    centerX - phaseLabel.Width / 2,
                    140
                );
            };

            section.Controls.Add(phaseLabel);
            section.Controls.Add(_timerLabel);
            section.Controls.Add(_timerCircle);

            return section;
        }

        private void DrawCircularTimer(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var rect = new Rectangle(10, 10, 160, 160);

            // Background circle
            using (var bgBrush = new SolidBrush(Color.FromArgb(240, 240, 245)))
            {
                g.FillEllipse(bgBrush, rect);
            }

            // Progress arc
            if (_totalSecondsInSession > 0)
            {
                float percentage = (float)(_totalSecondsInSession - _secondsRemaining) / _totalSecondsInSession;
                float angle = 360 * percentage;

                using (var progressPen = new Pen(_onBreak ? Color.FromArgb(34, 197, 94) : Color.FromArgb(234, 88, 12), 8))
                {
                    progressPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    progressPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    g.DrawArc(progressPen, rect, -90, angle);
                }
            }
        }

        private Panel CreateControlsSection()
        {
            var section = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(248, 249, 252),
                Padding = new Padding(16, 12, 16, 12)
            };

            _startBtn = new Button
            {
                Text = "▶ Start",
                Height = 46,
                BackColor = Color.FromArgb(234, 88, 12),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            _startBtn.FlatAppearance.BorderSize = 0;
            _startBtn.Click += (s, e) => StartOrPause();

            _resetBtn = new Button
            {
                Width = 46,
                Height = 46,
                Text = "⟲",
                BackColor = Color.White,
                ForeColor = Color.FromArgb(100, 100, 120),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 16F),
                Cursor = Cursors.Hand,
                Dock = DockStyle.Right
            };
            _resetBtn.FlatAppearance.BorderColor = Color.FromArgb(220, 220, 230);
            _resetBtn.FlatAppearance.BorderSize = 1;
            _resetBtn.Click += (s, e) => ResetAll();

            var resetWrapper = new Panel
            {
                Dock = DockStyle.Right,
                Width = 54,
                Padding = new Padding(8, 0, 0, 0)
            };
            resetWrapper.Controls.Add(_resetBtn);

            _startBtn.Dock = DockStyle.Fill;

            section.Controls.Add(_startBtn);
            section.Controls.Add(resetWrapper);

            return section;
        }

        private Panel CreateStatsSection()
        {
            var section = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.White,
                Padding = new Padding(16, 12, 16, 12)
            };

            var titleLabel = new Label
            {
                Text = "Today's Progress",
                Dock = DockStyle.Top,
                Height = 20,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 120)
            };

            var statsContainer = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 0)
            };

            _sessionsLabel = CreateStatCard("🎯", "0", "Sessions", out _sessionsValueLabel);
            _totalTimeLabel = CreateStatCard("⏰", "0m", "Focus Time", out _totalTimeValueLabel);

            statsContainer.Controls.Add(_sessionsLabel);
            statsContainer.Controls.Add(_totalTimeLabel);

            section.Controls.Add(statsContainer);
            section.Controls.Add(titleLabel);

            return section;
        }

        private Panel CreateStatCard(string emoji, string value, string label, out Label valueLabel)
        {
            var card = new Panel
            {
                Width = 155,
                Height = 50,
                BackColor = Color.FromArgb(248, 249, 252),
                Margin = new Padding(0, 0, 8, 0)
            };

            var emojiLabel = new Label
            {
                Text = emoji,
                Font = new Font("Segoe UI", 16F),
                Location = new Point(10, 12),
                AutoSize = true
            };

            valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(234, 88, 12),
                Location = new Point(45, 8),
                AutoSize = true
            };

            var descLabel = new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.Gray,
                Location = new Point(45, 28),
                AutoSize = true
            };

            card.Controls.Add(descLabel);
            card.Controls.Add(valueLabel);
            card.Controls.Add(emojiLabel);

            return card;
        }

        private Panel CreateSettingsSection()
        {
            var section = new Panel
            {
                Dock = DockStyle.Top,
                Height = 180,
                BackColor = Color.White,
                Padding = new Padding(16, 12, 16, 12)
            };

            var titleLabel = new Label
            {
                Text = "Session Settings",
                Dock = DockStyle.Top,
                Height = 24,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 120)
            };

            _workMinutes = new NumericUpDown
            {
                Minimum = 5,
                Maximum = 120,
                Value = 25,
                Width = 60,
                Font = new Font("Segoe UI", 10F)
            };
            _workMinutes.ValueChanged += (s, e) =>
            {
                if (!_isRunning && !_onBreak)
                {
                    _secondsRemaining = (int)_workMinutes.Value * 60;
                    _totalSecondsInSession = _secondsRemaining;
                    UpdateDisplay();
                }
            };

            _breakMinutes = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 30,
                Value = 5,
                Width = 60,
                Font = new Font("Segoe UI", 10F)
            };

            var workRow = CreateSettingRow("Focus Duration", _workMinutes, "minutes");
            var breakRow = CreateSettingRow("Break Duration", _breakMinutes, "minutes");

            _autoStartBreaks = new CheckBox
            {
                Text = "Auto-start breaks",
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 9F),
                Checked = false,
                Margin = new Padding(0, 8, 0, 0)
            };

            _soundEnabled = new CheckBox
            {
                Text = "Sound notifications",
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 9F),
                Checked = true
            };

            section.Controls.Add(_soundEnabled);
            section.Controls.Add(_autoStartBreaks);
            section.Controls.Add(breakRow);
            section.Controls.Add(workRow);
            section.Controls.Add(titleLabel);

            return section;
        }

        private Panel CreateSettingRow(string labelText, Control control, string suffix)
        {
            var row = new Panel
            {
                Dock = DockStyle.Top,
                Height = 36,
                Padding = new Padding(0, 8, 0, 0)
            };

            var label = new Label
            {
                Text = labelText,
                Dock = DockStyle.Left,
                Width = 120,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9F)
            };

            var suffixLabel = new Label
            {
                Text = suffix,
                Dock = DockStyle.Right,
                Width = 60,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray
            };

            control.Dock = DockStyle.Right;

            row.Controls.Add(suffixLabel);
            row.Controls.Add(control);
            row.Controls.Add(label);

            return row;
        }

        private Panel CreateTipsSection()
        {
            var section = new Panel
            {
                Dock = DockStyle.Top,
                Height = 160,
                BackColor = Color.FromArgb(255, 247, 237),
                Padding = new Padding(16, 12, 16, 12)
            };

            var titleLabel = new Label
            {
                Text = "💡 Focus Tips",
                Dock = DockStyle.Top,
                Height = 24,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(234, 88, 12)
            };

            var tipsLabel = new Label
            {
                Text = "• Remove all distractions\n" +
                       "• Focus on one task only\n" +
                       "• Take your breaks seriously\n" +
                       "• Stay hydrated\n" +
                       "• Stretch during breaks",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(80, 80, 90),
                Padding = new Padding(0, 8, 0, 0)
            };

            section.Controls.Add(tipsLabel);
            section.Controls.Add(titleLabel);

            return section;
        }

        private void InitializeSession()
        {
            _onBreak = false;
            _isRunning = false;
            _secondsRemaining = (int)_workMinutes.Value * 60;
            _totalSecondsInSession = _secondsRemaining;
            _statusLabel.Text = "Ready to focus";
            UpdateDisplay();
        }

        private void StartOrPause()
        {
            if (!_isRunning)
            {
                _timer.Start();
                _isRunning = true;
                _startBtn.Text = "⏸ Pause";
                _statusLabel.Text = _onBreak ? "Break in progress..." : "Focus session active";
                _startBtn.BackColor = Color.FromArgb(220, 38, 38);
            }
            else
            {
                _timer.Stop();
                _isRunning = false;
                _startBtn.Text = "▶ Resume";
                _statusLabel.Text = "Paused";
                _startBtn.BackColor = Color.FromArgb(234, 88, 12);
            }
        }

        private void ResetAll()
        {
            _timer.Stop();
            _isRunning = false;
            _completedSessions = 0;
            _totalMinutesWorked = 0;
            UpdateStatsDisplay();
            InitializeSession();
            _startBtn.Text = "▶ Start";
            _startBtn.BackColor = Color.FromArgb(234, 88, 12);
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            if (_secondsRemaining <= 0)
            {
                _timer.Stop();
                _isRunning = false;

                if (_onBreak)
                {
                    // Break finished
                    _completedSessions++;
                    _totalMinutesWorked += (int)_workMinutes.Value;
                    UpdateStatsDisplay();

                    if (_soundEnabled.Checked)
                        SystemSounds.Exclamation.Play();

                    MessageBox.Show(
                        "Break time is over! Ready to focus again?",
                        "Break Complete",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    InitializeSession();
                    _startBtn.Text = "▶ Start";
                    _startBtn.BackColor = Color.FromArgb(234, 88, 12);
                }
                else
                {
                    // Work session finished
                    if (_soundEnabled.Checked)
                        SystemSounds.Asterisk.Play();

                    var result = MessageBox.Show(
                        "Great job! You've completed a focus session.\nTime for a break?",
                        "Session Complete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information
                    );

                    _onBreak = true;
                    _secondsRemaining = (int)_breakMinutes.Value * 60;
                    _totalSecondsInSession = _secondsRemaining;
                    _statusLabel.Text = "Break time!";
                    UpdateDisplay();

                    if (_autoStartBreaks.Checked || result == DialogResult.Yes)
                    {
                        _timer.Start();
                        _isRunning = true;
                        _startBtn.Text = "⏸ Pause";
                        _startBtn.BackColor = Color.FromArgb(34, 197, 94);
                    }
                    else
                    {
                        _startBtn.Text = "▶ Start Break";
                        _startBtn.BackColor = Color.FromArgb(34, 197, 94);
                    }
                }
                return;
            }

            _secondsRemaining--;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            _timerLabel.Text = $"{_secondsRemaining / 60:00}:{_secondsRemaining % 60:00}";
            _timerCircle.Invalidate(); // Trigger redraw for circular progress
        }

        private void UpdateStatsDisplay()
        {
            _sessionsValueLabel.Text = _completedSessions.ToString();
            _totalTimeValueLabel.Text = $"{_totalMinutesWorked}m";
        }
    }
}