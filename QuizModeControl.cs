using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ai_Study_Buddy___Gemini
{
    public class QuizModeControl : UserControl
    {
        private readonly GeminiApiClient _api;
        private readonly string _source;
        private readonly string _contextLabel;
        private readonly bool _hasMaterial;

        // UI Components
        private Panel _headerPanel;
        private Panel _contentPanel;
        private Panel _footerPanel;
        private Button _generateBtn;
        private Button _nextBtn;
        private Button _prevBtn;
        private Label _progressLabel;
        private Label _scoreLabel;
        private Label _questionLabel;
        private FlowLayoutPanel _optionsPanel;
        private Panel _feedbackPanel;
        private Label _feedbackLabel;

        // Quiz State
        private List<Question> _questions;
        private int _currentQuestionIndex = 0;
        private int _correctAnswers = 0;
        private int _totalAttempts = 0;

        public QuizModeControl(GeminiApiClient apiClient, string sourceText, string contextLabel, bool hasMaterial)
        {
            _api = apiClient;
            _source = sourceText ?? string.Empty;
            _contextLabel = contextLabel;
            _hasMaterial = hasMaterial;
            _questions = new List<Question>();
            BuildUi();
        }

        private void BuildUi()
        {
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(248, 249, 252);
            Padding = new Padding(0);

            // Header Panel
            _headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.White,
                Padding = new Padding(20, 15, 20, 10)
            };

            _progressLabel = new Label
            {
                Text = "Ready to start",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(79, 70, 229),
                AutoSize = true,
                Location = new Point(20, 15)
            };

            _scoreLabel = new Label
            {
                Text = "Score: 0/0",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(20, 45)
            };

            _generateBtn = new Button
            {
                Text = _hasMaterial ? "🎯 Start Quiz" : "⚠ Add Material",
                Size = new Size(120, 36),
                BackColor = _hasMaterial ? Color.FromArgb(79, 70, 229) : Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = _hasMaterial ? Cursors.Hand : Cursors.Default,
                Enabled = _hasMaterial
            };
            _generateBtn.FlatAppearance.BorderSize = 0;
            _generateBtn.Click += async (_, __) => await GenerateQuizAsync();

            // Position generate button on the right
            _headerPanel.Resize += (_, __) =>
            {
                _generateBtn.Location = new Point(
                    _headerPanel.Width - _generateBtn.Width - 20,
                    (_headerPanel.Height - _generateBtn.Height) / 2
                );
            };

            _headerPanel.Controls.Add(_progressLabel);
            _headerPanel.Controls.Add(_scoreLabel);
            _headerPanel.Controls.Add(_generateBtn);

            // Content Panel (Scrollable)
            var contentContainer = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20, 20, 20, 10),
                BackColor = Color.FromArgb(248, 249, 252)
            };

            _contentPanel = new Panel
            {
                Width = 340,
                AutoSize = true,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Question Label
            _questionLabel = new Label
            {
                Text = _hasMaterial ? "Click 'Start Quiz' to begin" : "Upload study material to unlock quizzes",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = _hasMaterial ? Color.FromArgb(40, 40, 40) : Color.Firebrick,
                AutoSize = false,
                Width = 300,
                Height = 60,
                Location = new Point(0, 0)
            };

            // Options Panel
            _optionsPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                Width = 300,
                Location = new Point(0, 70),
                WrapContents = false
            };

            // Feedback Panel
            _feedbackPanel = new Panel
            {
                Width = 300,
                Height = 60,
                BackColor = Color.Transparent,
                Visible = false,
                Location = new Point(0, 80)
            };

            _feedbackLabel = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                AutoSize = false
            };

            _feedbackPanel.Controls.Add(_feedbackLabel);

            _contentPanel.Controls.Add(_questionLabel);
            _contentPanel.Controls.Add(_optionsPanel);
            _contentPanel.Controls.Add(_feedbackPanel);

            contentContainer.Controls.Add(_contentPanel);
            contentContainer.Resize += (_, __) =>
            {
                _contentPanel.Location = new Point(
                    Math.Max(0, (contentContainer.ClientSize.Width - _contentPanel.Width) / 2),
                    10
                );
            };

            // Footer Panel (Navigation)
            _footerPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.White,
                Padding = new Padding(20, 15, 20, 15),
                Visible = false
            };

            _prevBtn = new Button
            {
                Text = "← Previous",
                Size = new Size(100, 40),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(79, 70, 229),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Location = new Point(20, 15)
            };
            _prevBtn.FlatAppearance.BorderColor = Color.FromArgb(79, 70, 229);
            _prevBtn.Click += (_, __) => NavigateQuestion(-1);

            _nextBtn = new Button
            {
                Text = "Next →",
                Size = new Size(100, 40),
                BackColor = Color.FromArgb(79, 70, 229),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            _nextBtn.FlatAppearance.BorderSize = 0;
            _nextBtn.Click += (_, __) => NavigateQuestion(1);

            _footerPanel.Resize += (_, __) =>
            {
                _nextBtn.Location = new Point(
                    _footerPanel.Width - _nextBtn.Width - 20,
                    15
                );
            };

            _footerPanel.Controls.Add(_prevBtn);
            _footerPanel.Controls.Add(_nextBtn);

            // Add all panels to control
            Controls.Add(contentContainer);
            Controls.Add(_headerPanel);
            Controls.Add(_footerPanel);
        }

        private async Task GenerateQuizAsync()
        {
            if (!_hasMaterial || string.IsNullOrWhiteSpace(_source))
            {
                MessageBox.Show("Please add study material first!", "No Material",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // UI Loading State
            _generateBtn.Enabled = false;
            _generateBtn.Text = "Generating...";
            _progressLabel.Text = "Generating questions...";
            _questionLabel.Text = "Please wait while we create your quiz...";
            _optionsPanel.Controls.Clear();

            try
            {
                _questions = await _api.GenerateQuizQuestionsAsync(_source);

                if (_questions == null || _questions.Count == 0)
                {
                    _progressLabel.Text = "Failed to generate questions";
                    _questionLabel.Text = "Unable to create quiz. Try uploading more detailed material.";
                    return;
                }

                _currentQuestionIndex = 0;
                _correctAnswers = 0;
                _totalAttempts = 0;

                _footerPanel.Visible = true;
                _generateBtn.Text = "🔄 Restart Quiz";
                _generateBtn.Enabled = true;

                DisplayCurrentQuestion();
            }
            catch (Exception ex)
            {
                _progressLabel.Text = "Error generating quiz";
                _questionLabel.Text = $"Error: {ex.Message}";
                _generateBtn.Text = "🎯 Try Again";
                _generateBtn.Enabled = true;
            }
        }

        private void DisplayCurrentQuestion()
        {
            if (_questions == null || _currentQuestionIndex >= _questions.Count) return;

            var question = _questions[_currentQuestionIndex];
            _optionsPanel.Controls.Clear();
            _feedbackPanel.Visible = false;

            // Update progress
            _progressLabel.Text = $"Question {_currentQuestionIndex + 1} of {_questions.Count}";
            _scoreLabel.Text = $"Score: {_correctAnswers}/{_totalAttempts}";

            // Update question text
            _questionLabel.Text = question.Text;

            // Create option buttons
            for (int i = 0; i < question.Options.Count; i++)
            {
                var option = question.Options[i];
                var optionIndex = i;

                var optionBtn = new Button
                {
                    Text = $"{(char)('A' + i)}. {option.Text}",
                    Width = 300,
                    Height = 50,
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(40, 40, 40),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9.5F),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(15, 0, 15, 0),
                    Cursor = Cursors.Hand,
                    Margin = new Padding(0, 0, 0, 10),
                    Tag = optionIndex
                };
                optionBtn.FlatAppearance.BorderColor = Color.FromArgb(220, 220, 230);
                optionBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 250);

                optionBtn.Click += (_, __) => HandleOptionClick(optionBtn, question, optionIndex);

                _optionsPanel.Controls.Add(optionBtn);
            }

            // Update navigation buttons
            _prevBtn.Enabled = _currentQuestionIndex > 0;
            _nextBtn.Enabled = _currentQuestionIndex < _questions.Count - 1;

            if (_currentQuestionIndex == _questions.Count - 1)
                _nextBtn.Text = "Finish";
            else
                _nextBtn.Text = "Next →";

            // Adjust content panel height
            _contentPanel.Height = _questionLabel.Height + _optionsPanel.Height + _feedbackPanel.Height + 40;
        }

        private void HandleOptionClick(Button clickedBtn, Question question, int selectedIndex)
        {
            // Disable all option buttons after selection
            foreach (Button btn in _optionsPanel.Controls)
            {
                btn.Enabled = false;
            }

            _totalAttempts++;
            bool isCorrect = selectedIndex == question.CorrectIndex;

            if (isCorrect)
            {
                _correctAnswers++;
                clickedBtn.BackColor = Color.FromArgb(220, 252, 231);
                clickedBtn.ForeColor = Color.FromArgb(21, 128, 61);
                clickedBtn.FlatAppearance.BorderColor = Color.FromArgb(134, 239, 172);

                _feedbackLabel.Text = "✓ Correct!";
                _feedbackLabel.ForeColor = Color.FromArgb(21, 128, 61);
                _feedbackPanel.BackColor = Color.FromArgb(220, 252, 231);
            }
            else
            {
                clickedBtn.BackColor = Color.FromArgb(254, 226, 226);
                clickedBtn.ForeColor = Color.FromArgb(185, 28, 28);
                clickedBtn.FlatAppearance.BorderColor = Color.FromArgb(252, 165, 165);

                // Highlight correct answer
                var correctBtn = _optionsPanel.Controls[question.CorrectIndex] as Button;
                if (correctBtn != null)
                {
                    correctBtn.BackColor = Color.FromArgb(220, 252, 231);
                    correctBtn.ForeColor = Color.FromArgb(21, 128, 61);
                    correctBtn.FlatAppearance.BorderColor = Color.FromArgb(134, 239, 172);
                }

                _feedbackLabel.Text = "✗ Incorrect";
                _feedbackLabel.ForeColor = Color.FromArgb(185, 28, 28);
                _feedbackPanel.BackColor = Color.FromArgb(254, 226, 226);
            }

            _feedbackPanel.Visible = true;
            _feedbackPanel.Location = new Point(0, _optionsPanel.Bottom + 10);
            _contentPanel.Height = _questionLabel.Height + _optionsPanel.Height + _feedbackPanel.Height + 50;

            // Update score
            _scoreLabel.Text = $"Score: {_correctAnswers}/{_totalAttempts}";
        }

        private void NavigateQuestion(int direction)
        {
            int newIndex = _currentQuestionIndex + direction;

            if (newIndex < 0 || newIndex >= _questions.Count)
            {
                if (newIndex >= _questions.Count)
                {
                    ShowQuizSummary();
                }
                return;
            }

            _currentQuestionIndex = newIndex;
            DisplayCurrentQuestion();
        }

        private void ShowQuizSummary()
        {
            _footerPanel.Visible = false;
            _optionsPanel.Controls.Clear();
            _feedbackPanel.Visible = false;

            double percentage = _totalAttempts > 0 ? (_correctAnswers / (double)_totalAttempts) * 100 : 0;
            string grade = percentage >= 90 ? "Excellent! 🌟" :
                          percentage >= 70 ? "Good Job! 👍" :
                          percentage >= 50 ? "Keep Practicing 📚" : "Review Material 📖";

            _progressLabel.Text = "Quiz Complete!";
            _scoreLabel.Text = $"Final Score: {_correctAnswers}/{_totalAttempts} ({percentage:F0}%)";

            _questionLabel.Text = $"{grade}\n\nYou answered {_correctAnswers} out of {_totalAttempts} questions correctly.";
            _questionLabel.Height = 80;

            var restartBtn = new Button
            {
                Text = "🔄 Take Quiz Again",
                Width = 300,
                Height = 45,
                BackColor = Color.FromArgb(79, 70, 229),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            restartBtn.FlatAppearance.BorderSize = 0;
            restartBtn.Click += async (_, __) => await GenerateQuizAsync();

            _optionsPanel.Controls.Add(restartBtn);
            _contentPanel.Height = _questionLabel.Height + _optionsPanel.Height + 40;
        }
    }
}