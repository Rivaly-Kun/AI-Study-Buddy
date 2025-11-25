using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ai_Study_Buddy___Gemini
{
    public class MockExamControl : UserControl
    {
        private readonly GeminiApiClient _api;
        private readonly string _source;
        private readonly string _contextLabel;
        private readonly bool _hasMaterial;

        // Header controls
        private Label _timerLabel;
        private Label _questionCountLabel;
        private ProgressBar _progressBar;

        // Setup controls
        private Panel _setupPanel;
        private ComboBox _difficultyCombo;
        private ComboBox _questionTypeCombo;
        private NumericUpDown _questionCount;
        private Button _startExamBtn;

        // Exam controls
        private Panel _examPanel;
        private Panel _questionContainer;
        private Label _questionNumberLabel;
        private Label _questionTextLabel;
        private FlowLayoutPanel _optionsPanel;
        private TextBox _enumerationInput;
        private Button _enumerationSubmit;
        private Button _prevBtn;
        private Button _nextBtn;
        private Button _flagBtn;
        private Button _submitBtn;
        private CheckBox _showFlaggedOnly;

        // Results controls
        private Panel _resultsPanel;

        // State
        private System.Windows.Forms.Timer _timer;
        private int _secondsRemaining;
        private int _totalExamSeconds;
        private int _currentIndex;
        private Question[] _questions = Array.Empty<Question>();
        private bool _examStarted;
        private bool _examFinished;
        private string _examQuestionType = "choices";

        public MockExamControl(GeminiApiClient apiClient, string sourceText, string contextLabel, bool hasMaterial)
        {
            _api = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _source = sourceText ?? string.Empty;
            _contextLabel = contextLabel;
            _hasMaterial = hasMaterial;
            BuildUi();
        }

        private void BuildUi()
        {
            SuspendLayout();

            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(248, 249, 252);
            Padding = new Padding(0);

            // Header with timer and progress
            var header = CreateHeader();

            // Setup screen (shown initially)
            _setupPanel = CreateSetupPanel();

            // Exam screen (shown during exam)
            _examPanel = CreateExamPanel();
            _examPanel.Visible = false;

            // Results screen (shown after completion)
            _resultsPanel = CreateResultsPanel();
            _resultsPanel.Visible = false;

            // Timer initialization
            _timer = new System.Windows.Forms.Timer { Interval = 1000 };
            _timer.Tick += OnTimerTick;

            // Assembly
            Controls.Add(_resultsPanel);
            Controls.Add(_examPanel);
            Controls.Add(_setupPanel);
            Controls.Add(header);

            ResumeLayout();
        }

        private Panel CreateHeader()
        {
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(16, 185, 129),
                Padding = new Padding(16, 12, 16, 8)
            };

            var titleLabel = new Label
            {
                Text = "Mock Exam",
                Dock = DockStyle.Top,
                Height = 24,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            var statsContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            _timerLabel = new Label
            {
                Text = "00:00",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(16, 0)
            };

            _questionCountLabel = new Label
            {
                Text = "0/0",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            statsContainer.Resize += (s, e) =>
            {
                _questionCountLabel.Location = new Point(
                    statsContainer.Width - _questionCountLabel.Width - 16,
                    4
                );
            };

            _progressBar = new ProgressBar
            {
                Dock = DockStyle.Bottom,
                Height = 4,
                Style = ProgressBarStyle.Continuous,
                Value = 0
            };

            statsContainer.Controls.Add(_questionCountLabel);
            statsContainer.Controls.Add(_timerLabel);

            header.Controls.Add(statsContainer);
            header.Controls.Add(_progressBar);
            header.Controls.Add(titleLabel);

            return header;
        }

        private Panel CreateSetupPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Context info badge
            var contextBadge = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = _hasMaterial ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242),
                Padding = new Padding(12)
            };

            var contextIcon = new Label
            {
                Text = _hasMaterial ? "✓" : "⚠",
                Dock = DockStyle.Left,
                Width = 30,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = _hasMaterial ? Color.FromArgb(16, 185, 129) : Color.FromArgb(220, 38, 38),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var contextText = new Label
            {
                Text = _contextLabel,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F),
                ForeColor = _hasMaterial ? Color.FromArgb(21, 128, 61) : Color.FromArgb(153, 27, 27),
                TextAlign = ContentAlignment.MiddleLeft
            };

            contextBadge.Controls.Add(contextText);
            contextBadge.Controls.Add(contextIcon);

            // Settings card
            var settingsCard = new Panel
            {
                Dock = DockStyle.Top,
                Height = 200,
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(16),
                Margin = new Padding(0, 16, 0, 0)
            };

            var cardTitle = new Label
            {
                Text = "Exam Configuration",
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55)
            };

            // Difficulty setting
            var difficultyRow = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(0, 12, 0, 0)
            };

            var diffLabel = new Label
            {
                Text = "Difficulty Level",
                Dock = DockStyle.Left,
                Width = 120,
                Font = new Font("Segoe UI", 9F),
                TextAlign = ContentAlignment.MiddleLeft
            };

            _difficultyCombo = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            _difficultyCombo.Items.AddRange(new[] { "Easy", "Mixed", "Hard" });
            _difficultyCombo.SelectedIndex = 1;

            difficultyRow.Controls.Add(_difficultyCombo);
            difficultyRow.Controls.Add(diffLabel);

            // Question type setting
            var typeRow = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(0, 12, 0, 0)
            };

            var typeLabel = new Label
            {
                Text = "Question Type",
                Dock = DockStyle.Left,
                Width = 120,
                Font = new Font("Segoe UI", 9F),
                TextAlign = ContentAlignment.MiddleLeft
            };

            _questionTypeCombo = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            _questionTypeCombo.Items.AddRange(new[] { "Choices", "Enumeration", "Mixed" });
            _questionTypeCombo.SelectedIndex = 0;

            typeRow.Controls.Add(_questionTypeCombo);
            typeRow.Controls.Add(typeLabel);

            // Question count setting
            var countRow = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(0, 12, 0, 0)
            };

            var countLabel = new Label
            {
                Text = "Number of Questions",
                Dock = DockStyle.Left,
                Width = 150,
                Font = new Font("Segoe UI", 9F),
                TextAlign = ContentAlignment.MiddleLeft
            };

            _questionCount = new NumericUpDown
            {
                Minimum = 5,
                Maximum = 50,
                Value = 15,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F)
            };

            var estimateLabel = new Label
            {
                Text = "~15 min",
                Dock = DockStyle.Right,
                Width = 60,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleRight
            };

            _questionCount.ValueChanged += (s, e) =>
            {
                int minutes = Math.Max(10, (int)_questionCount.Value);
                estimateLabel.Text = $"~{minutes} min";
            };

            countRow.Controls.Add(estimateLabel);
            countRow.Controls.Add(_questionCount);
            countRow.Controls.Add(countLabel);

            settingsCard.Controls.Add(countRow);
            settingsCard.Controls.Add(typeRow);
            settingsCard.Controls.Add(difficultyRow);
            settingsCard.Controls.Add(cardTitle);

            // Start button
            _startExamBtn = new Button
            {
                Text = "🎯 Start Exam",
                Dock = DockStyle.Top,
                Height = 48,
                BackColor = Color.FromArgb(16, 185, 129),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Enabled = _hasMaterial,
                Margin = new Padding(0, 20, 0, 0)
            };
            _startExamBtn.FlatAppearance.BorderSize = 0;
            _startExamBtn.Click += async (s, e) => await StartExamAsync();

            // Instructions
            var instructions = new Label
            {
                Text = "• Answer all questions within the time limit\n" +
                       "• Flag difficult questions to review later\n" +
                       "• You can navigate between questions freely\n" +
                       "• Submit when ready or when time expires",
                Dock = DockStyle.Top,
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(107, 114, 128),
                Padding = new Padding(0, 16, 0, 0)
            };

            panel.Controls.Add(instructions);
            panel.Controls.Add(_startExamBtn);
            panel.Controls.Add(settingsCard);
            panel.Controls.Add(contextBadge);

            return panel;
        }

        private Panel CreateExamPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(0)
            };

            // Question display area
            _questionContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20),
                AutoScroll = true
            };

            _questionNumberLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 30,
                Text = "Question 1 of 15",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(107, 114, 128)
            };

            _questionTextLabel = new Label
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                MaximumSize = new Size(340, 0),
                Text = "",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39),
                Padding = new Padding(0, 8, 0, 16)
            };

            _optionsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 0)
            };

            // Enumeration input (hidden by default)
            _enumerationInput = new TextBox
            {
                Width = 340,
                Height = 35,
                Location = new Point(0, 8),
                Font = new Font("Segoe UI", 10F),
                Visible = false,
                BorderStyle = BorderStyle.FixedSingle
            };
            _enumerationInput.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    HandleEnumerationSubmit();
                }
            };

            _enumerationSubmit = new Button
            {
                Text = "Submit Answer",
                Width = 120,
                Height = 35,
                Location = new Point(0, 50),
                BackColor = Color.FromArgb(79, 70, 229),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };
            _enumerationSubmit.FlatAppearance.BorderSize = 0;
            _enumerationSubmit.Click += (_, __) => HandleEnumerationSubmit();

            _questionContainer.Controls.Add(_enumerationSubmit);
            _questionContainer.Controls.Add(_enumerationInput);
            _questionContainer.Controls.Add(_optionsPanel);
            _questionContainer.Controls.Add(_questionTextLabel);
            _questionContainer.Controls.Add(_questionNumberLabel);

            // Navigation footer
            var footer = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 120,
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(16, 12, 16, 12)
            };

            _showFlaggedOnly = new CheckBox
            {
                Text = "🚩 Show flagged only",
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(107, 114, 128)
            };
            _showFlaggedOnly.CheckedChanged += (s, e) => FilterQuestions();

            var navContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 8, 0, 0)
            };

            _prevBtn = new Button
            {
                Text = "◀ Previous",
                Width = 100,
                Height = 40,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand
            };
            _prevBtn.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            _prevBtn.Click += (s, e) => NavigateQuestion(-1);

            _nextBtn = new Button
            {
                Text = "Next ▶",
                Width = 100,
                Height = 40,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand,
                Location = new Point(108, 0)
            };
            _nextBtn.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            _nextBtn.Click += (s, e) => NavigateQuestion(1);

            _flagBtn = new Button
            {
                Text = "🚩 Flag",
                Width = 80,
                Height = 40,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Right
            };
            _flagBtn.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            _flagBtn.Click += (s, e) => ToggleFlag();

            _submitBtn = new Button
            {
                Text = "✓ Submit Exam",
                Height = 40,
                BackColor = Color.FromArgb(16, 185, 129),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            _submitBtn.FlatAppearance.BorderSize = 0;
            _submitBtn.Click += (s, e) => FinishExam();

            navContainer.Resize += (s, e) =>
            {
                _flagBtn.Location = new Point(navContainer.Width - 180, 0);
                _submitBtn.Location = new Point(navContainer.Width - 92, 0);
                _submitBtn.Width = 92;
            };

            navContainer.Controls.Add(_submitBtn);
            navContainer.Controls.Add(_flagBtn);
            navContainer.Controls.Add(_nextBtn);
            navContainer.Controls.Add(_prevBtn);

            footer.Controls.Add(navContainer);
            footer.Controls.Add(_showFlaggedOnly);

            panel.Controls.Add(_questionContainer);
            panel.Controls.Add(footer);

            return panel;
        }

        private Panel CreateResultsPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20),
                AutoScroll = true
            };

            return panel;
        }

        private async Task StartExamAsync()
        {
            if (!_hasMaterial || string.IsNullOrWhiteSpace(_source))
            {
                MessageBox.Show(
                    "Please upload study material or chat with Bud first.",
                    "No Material",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            _startExamBtn.Enabled = false;
            _startExamBtn.Text = "⏳ Generating exam...";

            // Remember the selected question type so we can force enumeration UI
            _examQuestionType = _questionTypeCombo.SelectedItem?.ToString()?.ToLower() ?? "choices";

            try
            {
                var difficulty = _difficultyCombo.SelectedItem?.ToString()?.ToLower() ?? "mixed";
                var questionType = _questionTypeCombo.SelectedItem?.ToString()?.ToLower() ?? "choices";
                int count = (int)_questionCount.Value;

                _questions = (await _api.GenerateMockExamAsync(_source, count, difficulty, questionType)).ToArray();

                if (_questions.Length == 0)
                {
                    MessageBox.Show("Failed to generate questions. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _startExamBtn.Enabled = true;
                    _startExamBtn.Text = "🎯 Start Exam";
                    return;
                }

                // Calculate exam time (1 minute per question, minimum 10 minutes)
                _totalExamSeconds = Math.Max(600, count * 60);
                _secondsRemaining = _totalExamSeconds;
                _currentIndex = 0;
                _examStarted = true;
                _examFinished = false;

                // Switch to exam view
                _setupPanel.Visible = false;
                _examPanel.Visible = true;

                _timer.Start();
                RenderCurrentQuestion();
                UpdateExamProgress();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating exam: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _startExamBtn.Enabled = true;
                _startExamBtn.Text = "🎯 Start Exam";
            }
        }

        private void RenderCurrentQuestion()
        {
            if (_questions.Length == 0 || _currentIndex >= _questions.Length) return;

            var question = _questions[_currentIndex];

            _questionNumberLabel.Text = $"Question {_currentIndex + 1} of {_questions.Length}";
            _questionTextLabel.Text = question.Text;

            // Update flag button
            _flagBtn.Text = question.IsFlagged ? "🚩 Flagged" : "🚩 Flag";
            _flagBtn.BackColor = question.IsFlagged ? Color.FromArgb(254, 226, 226) : Color.White;

            // Clear and rebuild options
            _optionsPanel.Controls.Clear();
            _enumerationInput.Visible = false;
            _enumerationSubmit.Visible = false;
            _enumerationInput.Text = string.Empty;
            _enumerationInput.BackColor = Color.White;
            _enumerationInput.ForeColor = Color.Black;

            // If this is an enumeration (short-answer) question or the exam was requested as enumeration, show the text box
            if (_examQuestionType == "enumeration" || (question.Options != null && question.Options.Count == 1))
            {
                _optionsPanel.Visible = false;
                _enumerationInput.Visible = true;
                _enumerationSubmit.Visible = true;
            }
            else
            {
                _optionsPanel.Visible = true;
                // Create option buttons for multiple choice
                if (question.Options != null)
                {
                    for (int i = 0; i < question.Options.Count; i++)
                    {
                        var optionPanel = CreateOptionButton(question, i);
                        _optionsPanel.Controls.Add(optionPanel);
                    }
                }
            }

            // Update navigation buttons
            _prevBtn.Enabled = _currentIndex > 0;
            _nextBtn.Enabled = _currentIndex < _questions.Length - 1;
        }

        private Panel CreateOptionButton(Question question, int optionIndex)
        {
            var panel = new Panel
            {
                Width = 340,
                Height = 50,
                BackColor = question.UserChoice == optionIndex
                    ? Color.FromArgb(219, 234, 254)
                    : Color.FromArgb(249, 250, 251),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 0, 8),
                Tag = optionIndex
            };

            var radioButton = new RadioButton
            {
                Width = 20,
                Height = 20,
                Location = new Point(12, 15),
                Checked = question.UserChoice == optionIndex,
                Tag = optionIndex
            };

            var optionLabel = new Label
            {
                Text = question.Options[optionIndex].Text,
                Location = new Point(40, 8),
                AutoSize = false,
                Width = 285,
                Height = 34,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(31, 41, 55),
                TextAlign = ContentAlignment.MiddleLeft
            };


            radioButton.CheckedChanged += (s, e) =>
            {
                if (radioButton.Checked)
                {
                    question.UserChoice = optionIndex;
                    RenderCurrentQuestion();
                    UpdateExamProgress();
                }
            };

            panel.Click += (s, e) => radioButton.Checked = true;
            optionLabel.Click += (s, e) => radioButton.Checked = true;

            panel.Controls.Add(optionLabel);
            panel.Controls.Add(radioButton);

            return panel;
        }

        private void HandleEnumerationSubmit()
        {
            if (_questions.Length == 0 || _currentIndex >= _questions.Length) return;
            var question = _questions[_currentIndex];

            // Disable submit to prevent double submits
            _enumerationSubmit.Enabled = false;

            var userAnswer = _enumerationInput.Text?.Trim() ?? string.Empty;
            var correctAnswer = question.Options != null && question.Options.Count > 0 ? question.Options[0].Text : string.Empty;

            bool isCorrect = string.Equals(userAnswer, correctAnswer, StringComparison.OrdinalIgnoreCase);

            // Store the answer (we'll use UserChoice to indicate if they answered)
            question.UserChoice = isCorrect ? 0 : -1; // 0 for correct, -1 for incorrect

            // Show feedback by updating the input styling
            if (isCorrect)
            {
                _enumerationInput.BackColor = Color.FromArgb(220, 252, 231);
                _enumerationInput.ForeColor = Color.FromArgb(21, 128, 61);
            }
            else
            {
                _enumerationInput.BackColor = Color.FromArgb(254, 226, 226);
                _enumerationInput.ForeColor = Color.FromArgb(185, 28, 28);
                // Show correct answer in a tooltip or message
                _enumerationInput.Text = $"{userAnswer} (Correct: {correctAnswer})";
            }

            UpdateExamProgress();

            // Re-enable submit for future questions
            _enumerationSubmit.Enabled = true;
        }

        private void NavigateQuestion(int direction)
        {
            var visibleQuestions = GetVisibleQuestions();
            if (visibleQuestions.Length == 0) return;

            int currentPos = Array.IndexOf(visibleQuestions, _currentIndex);
            if (currentPos == -1) currentPos = 0;

            int newPos = Math.Clamp(currentPos + direction, 0, visibleQuestions.Length - 1);
            _currentIndex = visibleQuestions[newPos];

            RenderCurrentQuestion();
        }

        private int[] GetVisibleQuestions()
        {
            if (!_showFlaggedOnly.Checked)
            {
                return Enumerable.Range(0, _questions.Length).ToArray();
            }

            return _questions
                .Select((q, i) => new { Question = q, Index = i })
                .Where(x => x.Question.IsFlagged)
                .Select(x => x.Index)
                .ToArray();
        }

        private void FilterQuestions()
        {
            var visible = GetVisibleQuestions();
            if (visible.Length > 0)
            {
                _currentIndex = visible[0];
                RenderCurrentQuestion();
            }
        }

        private void ToggleFlag()
        {
            if (_questions.Length == 0 || _currentIndex >= _questions.Length) return;

            _questions[_currentIndex].IsFlagged = !_questions[_currentIndex].IsFlagged;
            RenderCurrentQuestion();
        }

        private void FinishExam()
        {
            var result = MessageBox.Show(
                "Are you sure you want to submit your exam?\nThis action cannot be undone.",
                "Submit Exam",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes) return;

            _timer.Stop();
            _examFinished = true;

            // Calculate results
            int answered = _questions.Count(q => q.UserChoice.HasValue);
            int correct = _questions.Count(q => q.UserChoice == q.CorrectIndex);
            double percentage = _questions.Length > 0 ? (double)correct / _questions.Length * 100 : 0;

            // Show results
            _examPanel.Visible = false;
            _resultsPanel.Visible = true;

            DisplayResults(answered, correct, percentage);
        }

        private void DisplayResults(int answered, int correct, double percentage)
        {
            _resultsPanel.Controls.Clear();

            // Score card
            var scoreCard = new Panel
            {
                Dock = DockStyle.Top,
                Height = 180,
                BackColor = percentage >= 70 ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242),
                Padding = new Padding(20)
            };

            var scoreIcon = new Label
            {
                Text = percentage >= 70 ? "🎉" : "📊",
                Font = new Font("Segoe UI", 48F),
                AutoSize = true,
                Location = new Point(140, 20)
            };

            var scoreLabel = new Label
            {
                Text = $"{percentage:0.0}%",
                Font = new Font("Segoe UI", 32F, FontStyle.Bold),
                ForeColor = percentage >= 70 ? Color.FromArgb(21, 128, 61) : Color.FromArgb(153, 27, 27),
                AutoSize = true,
                Location = new Point(120, 85)
            };

            var statusLabel = new Label
            {
                Text = percentage >= 70 ? "Great job!" : "Keep practicing!",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = true,
                Location = new Point(135, 135)
            };

            scoreCard.Controls.Add(statusLabel);
            scoreCard.Controls.Add(scoreLabel);
            scoreCard.Controls.Add(scoreIcon);

            // Stats
            var statsPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 140,
                Padding = new Padding(20, 16, 20, 16)
            };

            var stat1 = CreateStatLabel("Questions Answered", $"{answered}/{_questions.Length}", 0);
            var stat2 = CreateStatLabel("Correct Answers", correct.ToString(), 35);
            var stat3 = CreateStatLabel("Time Taken", FormatTime(_totalExamSeconds - _secondsRemaining), 70);

            statsPanel.Controls.Add(stat3);
            statsPanel.Controls.Add(stat2);
            statsPanel.Controls.Add(stat1);

            // Retry button
            var retryBtn = new Button
            {
                Text = "🔄 Try Another Exam",
                Dock = DockStyle.Top,
                Height = 48,
                BackColor = Color.FromArgb(16, 185, 129),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(20, 16, 20, 0)
            };
            retryBtn.FlatAppearance.BorderSize = 0;
            retryBtn.Click += (s, e) => RestartExam();

            _resultsPanel.Controls.Add(retryBtn);
            _resultsPanel.Controls.Add(statsPanel);
            _resultsPanel.Controls.Add(scoreCard);
        }

        private Panel CreateStatLabel(string label, string value, int topOffset)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30
            };

            var labelControl = new Label
            {
                Text = label,
                Dock = DockStyle.Left,
                Width = 180,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(107, 114, 128),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var valueControl = new Label
            {
                Text = value,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                TextAlign = ContentAlignment.MiddleRight
            };

            panel.Controls.Add(valueControl);
            panel.Controls.Add(labelControl);

            return panel;
        }

        private void RestartExam()
        {
            _questions = Array.Empty<Question>();
            _currentIndex = 0;
            _examStarted = false;
            _examFinished = false;
            _secondsRemaining = 0;

            _resultsPanel.Visible = false;
            _setupPanel.Visible = true;
            _startExamBtn.Enabled = _hasMaterial;
            _startExamBtn.Text = "🎯 Start Exam";

            UpdateExamProgress();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (_secondsRemaining <= 0)
            {
                _timer.Stop();
                MessageBox.Show("Time's up! Your exam will be submitted.", "Time Expired",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                FinishExam();
                return;
            }

            _secondsRemaining--;
            UpdateExamProgress();

            // Warning at 1 minute remaining
            if (_secondsRemaining == 60)
            {
                MessageBox.Show("Only 1 minute remaining!", "Time Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateExamProgress()
        {
            // Update timer
            _timerLabel.Text = FormatTime(_secondsRemaining);
            _timerLabel.ForeColor = _secondsRemaining <= 60 ? Color.FromArgb(220, 38, 38) : Color.White;

            // Update question count
            _questionCountLabel.Text = _examStarted ? $"{_currentIndex + 1}/{_questions.Length}" : "0/0";

            // Update progress bar
            if (_questions.Length > 0)
            {
                int answered = _questions.Count(q => q.UserChoice.HasValue);
                _progressBar.Value = (int)((double)answered / _questions.Length * 100);
            }
            else
            {
                _progressBar.Value = 0;
            }
        }

        private string FormatTime(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            return $"{minutes:00}:{seconds:00}";
        }
    }
}