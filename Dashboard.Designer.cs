namespace Ai_Study_Buddy___Gemini
{
    partial class Dashboard
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.sidebar = new System.Windows.Forms.Panel();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonQuiz = new System.Windows.Forms.Button();
            this.buttonExam = new System.Windows.Forms.Button();
            this.buttonFocus = new System.Windows.Forms.Button();
            this.buttonCram = new System.Windows.Forms.Button();
            this.buttonChat = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.mainContentPanel = new System.Windows.Forms.Panel();
            this.panelChat = new System.Windows.Forms.Panel();
            this.panelChatMessages = new System.Windows.Forms.Panel();
            this.chatHistoryBox = new System.Windows.Forms.RichTextBox();
            this.panelChatHeader = new System.Windows.Forms.Panel();
            this.lblChatSubtitle = new System.Windows.Forms.Label();
            this.lblChatTitle = new System.Windows.Forms.Label();
            this.buttonHighlighter = new System.Windows.Forms.Button();
            this.panelChatInput = new System.Windows.Forms.Panel();
            this.panelInputContainer = new System.Windows.Forms.Panel();
            this.tableLayoutPanelInput = new System.Windows.Forms.TableLayoutPanel();
            this.panelSendButtonContainer = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.panelCram = new System.Windows.Forms.Panel();
            this.lblCramSub = new System.Windows.Forms.Label();
            this.lblCramTitle = new System.Windows.Forms.Label();
            this.panelCramCard = new System.Windows.Forms.Panel();
            this.btnGenerateTopics = new System.Windows.Forms.Button();
            this.comboCramSubject = new System.Windows.Forms.ComboBox();
            this.lblCramSelect = new System.Windows.Forms.Label();
            this.panelFocus = new System.Windows.Forms.Panel();
            this.panelFocusTimer = new System.Windows.Forms.Panel();
            this.lblFocusTimer = new System.Windows.Forms.Label();
            this.lblFocusStatus = new System.Windows.Forms.Label();
            this.btnFocusReset = new System.Windows.Forms.Button();
            this.btnFocusStart = new System.Windows.Forms.Button();
            this.lblFocusTitle = new System.Windows.Forms.Label();
            this.panelExam = new System.Windows.Forms.Panel();
            this.panelExamContent = new System.Windows.Forms.Panel();
            this.lblExamQuestion = new System.Windows.Forms.Label();
            this.radioOption4 = new System.Windows.Forms.RadioButton();
            this.radioOption3 = new System.Windows.Forms.RadioButton();
            this.radioOption2 = new System.Windows.Forms.RadioButton();
            this.radioOption1 = new System.Windows.Forms.RadioButton();
            this.btnExamSubmit = new System.Windows.Forms.Button();
            this.btnExamNext = new System.Windows.Forms.Button();
            this.btnExamPrev = new System.Windows.Forms.Button();
            this.lblExamTitle = new System.Windows.Forms.Label();
            this.panelQuiz = new System.Windows.Forms.Panel();
            this.lblQuizTitle = new System.Windows.Forms.Label();
            this.panelQuizCard = new System.Windows.Forms.Panel();
            this.btnQuizStart = new System.Windows.Forms.Button();
            this.comboQuizDoc = new System.Windows.Forms.ComboBox();
            this.lblQuizSelect = new System.Windows.Forms.Label();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.lblSettingsTitle = new System.Windows.Forms.Label();

            // Initialize the legacy reference to avoid build errors in Dashboard.cs
            this.bottomPanel = this.panelChatInput;

            this.sidebar.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.mainContentPanel.SuspendLayout();
            this.panelChat.SuspendLayout();
            this.panelChatMessages.SuspendLayout();
            this.panelChatHeader.SuspendLayout();
            this.panelChatInput.SuspendLayout();
            this.panelInputContainer.SuspendLayout();
            this.tableLayoutPanelInput.SuspendLayout();
            this.panelCram.SuspendLayout();
            this.panelCramCard.SuspendLayout();
            this.panelFocus.SuspendLayout();
            this.panelFocusTimer.SuspendLayout();
            this.panelExam.SuspendLayout();
            this.panelExamContent.SuspendLayout();
            this.panelQuiz.SuspendLayout();
            this.panelQuizCard.SuspendLayout();
            this.panelSettings.SuspendLayout();
            this.SuspendLayout();

            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(14)))), ((int)(((byte)(27)))));
            this.sidebar.Controls.Add(this.buttonSettings);
            this.sidebar.Controls.Add(this.buttonLogout);
            this.sidebar.Controls.Add(this.buttonQuiz);
            this.sidebar.Controls.Add(this.buttonExam);
            this.sidebar.Controls.Add(this.buttonFocus);
            this.sidebar.Controls.Add(this.buttonCram);
            this.sidebar.Controls.Add(this.buttonChat);
            this.sidebar.Controls.Add(this.panelLogo);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Location = new System.Drawing.Point(0, 0);
            this.sidebar.Name = "sidebar";
            this.sidebar.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            this.sidebar.Size = new System.Drawing.Size(260, 862);
            this.sidebar.TabIndex = 0;
            // 
            // buttonSettings
            // 
            this.buttonSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonSettings.FlatAppearance.BorderSize = 0;
            this.buttonSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.buttonSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(34)))), ((int)(((byte)(50)))));
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.buttonSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonSettings.Location = new System.Drawing.Point(0, 762);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.buttonSettings.Size = new System.Drawing.Size(260, 50);
            this.buttonSettings.TabIndex = 7;
            this.buttonSettings.Text = "⚙  Settings";
            this.buttonSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSettings.UseVisualStyleBackColor = true;
            // 
            // buttonLogout
            // 
            this.buttonLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonLogout.FlatAppearance.BorderSize = 0;
            this.buttonLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.buttonLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(34)))), ((int)(((byte)(50)))));
            this.buttonLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.buttonLogout.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonLogout.Location = new System.Drawing.Point(0, 812);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.buttonLogout.Size = new System.Drawing.Size(260, 50);
            this.buttonLogout.TabIndex = 8;
            this.buttonLogout.Text = "🚪  Logout";
            this.buttonLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLogout.UseVisualStyleBackColor = true;
            // 
            // buttonQuiz
            // 
            this.buttonQuiz.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonQuiz.FlatAppearance.BorderSize = 0;
            this.buttonQuiz.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.buttonQuiz.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(34)))), ((int)(((byte)(50)))));
            this.buttonQuiz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonQuiz.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.buttonQuiz.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonQuiz.Location = new System.Drawing.Point(0, 300);
            this.buttonQuiz.Name = "buttonQuiz";
            this.buttonQuiz.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.buttonQuiz.Size = new System.Drawing.Size(260, 50);
            this.buttonQuiz.TabIndex = 6;
            this.buttonQuiz.Text = "❓  Quiz Mode";
            this.buttonQuiz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonQuiz.UseVisualStyleBackColor = true;
            this.buttonQuiz.Click += new System.EventHandler(this.QuizzMode);
            // 
            // buttonExam
            // 
            this.buttonExam.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonExam.FlatAppearance.BorderSize = 0;
            this.buttonExam.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.buttonExam.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(34)))), ((int)(((byte)(50)))));
            this.buttonExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExam.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.buttonExam.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonExam.Location = new System.Drawing.Point(0, 250);
            this.buttonExam.Name = "buttonExam";
            this.buttonExam.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.buttonExam.Size = new System.Drawing.Size(260, 50);
            this.buttonExam.TabIndex = 5;
            this.buttonExam.Text = "📝  Mock Exam";
            this.buttonExam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExam.UseVisualStyleBackColor = true;
            this.buttonExam.Click += new System.EventHandler(this.MockExamMode);
            // 
            // buttonFocus
            // 
            this.buttonFocus.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonFocus.FlatAppearance.BorderSize = 0;
            this.buttonFocus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.buttonFocus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(34)))), ((int)(((byte)(50)))));
            this.buttonFocus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFocus.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.buttonFocus.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonFocus.Location = new System.Drawing.Point(0, 200);
            this.buttonFocus.Name = "buttonFocus";
            this.buttonFocus.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.buttonFocus.Size = new System.Drawing.Size(260, 50);
            this.buttonFocus.TabIndex = 4;
            this.buttonFocus.Text = "⏱  Focus Mode";
            this.buttonFocus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonFocus.UseVisualStyleBackColor = true;
            this.buttonFocus.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonCram
            // 
            this.buttonCram.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonCram.FlatAppearance.BorderSize = 0;
            this.buttonCram.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.buttonCram.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(34)))), ((int)(((byte)(50)))));
            this.buttonCram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCram.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.buttonCram.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonCram.Location = new System.Drawing.Point(0, 150);
            this.buttonCram.Name = "buttonCram";
            this.buttonCram.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.buttonCram.Size = new System.Drawing.Size(260, 50);
            this.buttonCram.TabIndex = 3;
            this.buttonCram.Text = "📚  Cram Mode";
            this.buttonCram.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCram.UseVisualStyleBackColor = true;
            this.buttonCram.Click += new System.EventHandler(this.CramMode);
            // 
            // buttonChat
            // 
            this.buttonChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(58)))), ((int)(((byte)(180)))));
            this.buttonChat.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonChat.FlatAppearance.BorderSize = 0;
            this.buttonChat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(46)))), ((int)(((byte)(150)))));
            this.buttonChat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(72)))), ((int)(((byte)(210)))));
            this.buttonChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChat.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.buttonChat.ForeColor = System.Drawing.Color.White;
            this.buttonChat.Location = new System.Drawing.Point(0, 100);
            this.buttonChat.Name = "buttonChat";
            this.buttonChat.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.buttonChat.Size = new System.Drawing.Size(260, 50);
            this.buttonChat.TabIndex = 2;
            this.buttonChat.Text = "💬  Chat";
            this.buttonChat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonChat.UseVisualStyleBackColor = false;
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(14)))), ((int)(((byte)(27)))));
            this.panelLogo.Controls.Add(this.lblAppTitle);
            this.panelLogo.Controls.Add(this.lblLogo);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Padding = new System.Windows.Forms.Padding(20, 25, 20, 10);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(260, 100);
            this.panelLogo.TabIndex = 1;
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.Color.White;
            this.lblAppTitle.Location = new System.Drawing.Point(85, 30);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Text = "Study Buddy\nAI Assistant";
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.lblLogo.ForeColor = System.Drawing.Color.MediumPurple;
            this.lblLogo.Location = new System.Drawing.Point(25, 20);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(50, 45);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "🧠";
            // 
            // mainContentPanel
            // 
            this.mainContentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.mainContentPanel.Controls.Add(this.panelChat);
            this.mainContentPanel.Controls.Add(this.panelCram);
            this.mainContentPanel.Controls.Add(this.panelFocus);
            this.mainContentPanel.Controls.Add(this.panelExam);
            this.mainContentPanel.Controls.Add(this.panelQuiz);
            this.mainContentPanel.Controls.Add(this.panelSettings);
            this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentPanel.Location = new System.Drawing.Point(260, 0);
            this.mainContentPanel.Name = "mainContentPanel";
            this.mainContentPanel.Size = new System.Drawing.Size(965, 862);
            this.mainContentPanel.TabIndex = 1;
            // 
            // panelChat
            // 
            this.panelChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.panelChat.Controls.Add(this.panelChatMessages);
            this.panelChat.Controls.Add(this.panelChatInput);
            this.panelChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChat.Location = new System.Drawing.Point(0, 0);
            this.panelChat.Name = "panelChat";
            this.panelChat.Size = new System.Drawing.Size(965, 862);
            this.panelChat.TabIndex = 0;
            // 
            // panelChatMessages
            // 
            this.panelChatMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.panelChatMessages.Controls.Add(this.chatHistoryBox);
            this.panelChatMessages.Controls.Add(this.panelChatHeader);
            this.panelChatMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChatMessages.Location = new System.Drawing.Point(0, 0);
            this.panelChatMessages.Name = "panelChatMessages";
            this.panelChatMessages.Padding = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.panelChatMessages.Size = new System.Drawing.Size(965, 712);
            this.panelChatMessages.TabIndex = 2;
            // 
            // chatHistoryBox
            // 
            this.chatHistoryBox.BackColor = System.Drawing.Color.White;
            this.chatHistoryBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chatHistoryBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatHistoryBox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.chatHistoryBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.chatHistoryBox.Location = new System.Drawing.Point(30, 95);
            this.chatHistoryBox.Name = "chatHistoryBox";
            this.chatHistoryBox.ReadOnly = true;
            this.chatHistoryBox.Size = new System.Drawing.Size(905, 667);
            this.chatHistoryBox.TabIndex = 3;
            this.chatHistoryBox.Text = "Welcome! Select a mode or start chatting.";
            this.chatHistoryBox.TextChanged += new System.EventHandler(this.chatHistoryBox_TextChanged);
            // 
            // panelChatHeader
            // 
            this.panelChatHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.panelChatHeader.Controls.Add(this.lblChatSubtitle);
            this.panelChatHeader.Controls.Add(this.lblChatTitle);
            this.panelChatHeader.Controls.Add(this.buttonHighlighter);
            this.panelChatHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelChatHeader.Location = new System.Drawing.Point(0, 0);
            this.panelChatHeader.Name = "panelChatHeader";
            this.panelChatHeader.Padding = new System.Windows.Forms.Padding(30, 15, 30, 10);
            this.panelChatHeader.Size = new System.Drawing.Size(965, 95);
            this.panelChatHeader.TabIndex = 2;
            // 
            // lblChatTitle
            // 
            this.lblChatTitle.AutoSize = true;
            this.lblChatTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblChatTitle.ForeColor = System.Drawing.Color.Black;
            this.lblChatTitle.Location = new System.Drawing.Point(30, 20);
            this.lblChatTitle.Name = "lblChatTitle";
            this.lblChatTitle.Size = new System.Drawing.Size(95, 25);
            this.lblChatTitle.TabIndex = 2;
            this.lblChatTitle.Text = "New Chat";
            // 
            // lblChatSubtitle
            // 
            this.lblChatSubtitle.AutoSize = true;
            this.lblChatSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblChatSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblChatSubtitle.Location = new System.Drawing.Point(32, 55);
            this.lblChatSubtitle.Name = "lblChatSubtitle";
            this.lblChatSubtitle.Size = new System.Drawing.Size(258, 17);
            this.lblChatSubtitle.TabIndex = 3;
            this.lblChatSubtitle.Text = "Stay curious. Bud adapts to handouts or chats.";
            // 
            // buttonHighlighter
            // 
            this.buttonHighlighter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHighlighter.BackColor = System.Drawing.Color.White;
            this.buttonHighlighter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.buttonHighlighter.FlatAppearance.BorderSize = 1;
            this.buttonHighlighter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(245)))));
            this.buttonHighlighter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(253)))));
            this.buttonHighlighter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHighlighter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonHighlighter.Location = new System.Drawing.Point(815, 26);
            this.buttonHighlighter.Name = "buttonHighlighter";
            this.buttonHighlighter.Size = new System.Drawing.Size(120, 34);
            this.buttonHighlighter.TabIndex = 1;
            this.buttonHighlighter.Text = "🖍 Highlight";
            this.buttonHighlighter.UseVisualStyleBackColor = false;
            this.buttonHighlighter.Click += new System.EventHandler(this.buttonHighlighter_Click);
            // 
            // panelChatInput
            // 
            this.panelChatInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.panelChatInput.Controls.Add(this.panelInputContainer);
            this.panelChatInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelChatInput.Location = new System.Drawing.Point(0, 712);
            this.panelChatInput.Name = "panelChatInput";
            this.panelChatInput.Padding = new System.Windows.Forms.Padding(32, 18, 32, 26);
            this.panelChatInput.Size = new System.Drawing.Size(965, 150);
            this.panelChatInput.TabIndex = 1;
            // 
            // panelInputContainer
            // 
            this.panelInputContainer.BackColor = System.Drawing.Color.White;
            this.panelInputContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInputContainer.Controls.Add(this.tableLayoutPanelInput);
            this.panelInputContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInputContainer.Location = new System.Drawing.Point(32, 18);
            this.panelInputContainer.Name = "panelInputContainer";
            this.panelInputContainer.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.panelInputContainer.Size = new System.Drawing.Size(901, 106);
            this.panelInputContainer.TabIndex = 1;
            // 
            // tableLayoutPanelInput
            // 
            this.tableLayoutPanelInput.ColumnCount = 3;
            this.tableLayoutPanelInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelInput.Controls.Add(this.buttonUpload, 0, 0);
            this.tableLayoutPanelInput.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanelInput.Controls.Add(this.panelSendButtonContainer, 2, 0);
            this.tableLayoutPanelInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelInput.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanelInput.Location = new System.Drawing.Point(15, 12);
            this.tableLayoutPanelInput.Name = "tableLayoutPanelInput";
            this.tableLayoutPanelInput.RowCount = 1;
            this.tableLayoutPanelInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelInput.Size = new System.Drawing.Size(871, 82);
            this.tableLayoutPanelInput.TabIndex = 0;
            // 
            // panelSendButtonContainer
            // 
            this.panelSendButtonContainer.BackColor = System.Drawing.Color.Transparent;
            this.panelSendButtonContainer.Controls.Add(this.buttonSend);
            this.panelSendButtonContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSendButtonContainer.Location = new System.Drawing.Point(735, 0);
            this.panelSendButtonContainer.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.panelSendButtonContainer.MinimumSize = new System.Drawing.Size(130, 60);
            this.panelSendButtonContainer.Name = "panelSendButtonContainer";
            this.panelSendButtonContainer.Padding = new System.Windows.Forms.Padding(10);
            this.panelSendButtonContainer.Size = new System.Drawing.Size(136, 82);
            this.panelSendButtonContainer.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(145, 0);
            this.textBox1.Margin = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.textBox1.MinimumSize = new System.Drawing.Size(200, 60);
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(578, 82);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // buttonSend
            // 
            this.buttonSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226)))));
            this.buttonSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSend.FlatAppearance.BorderSize = 0;
            this.buttonSend.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(33)))), ((int)(((byte)(206)))));
            this.buttonSend.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(63)))), ((int)(((byte)(246)))));
            this.buttonSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSend.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.buttonSend.ForeColor = System.Drawing.Color.White;
            this.buttonSend.Location = new System.Drawing.Point(10, 10);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(116, 62);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Send ↗";
            this.buttonSend.UseVisualStyleBackColor = false;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonUpload
            // 
            this.buttonUpload.BackColor = System.Drawing.Color.White;
            this.buttonUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonUpload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.buttonUpload.FlatAppearance.BorderSize = 1;
            this.buttonUpload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.buttonUpload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.buttonUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUpload.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.buttonUpload.ForeColor = System.Drawing.Color.Black;
            this.buttonUpload.Location = new System.Drawing.Point(0, 0);
            this.buttonUpload.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.buttonUpload.MinimumSize = new System.Drawing.Size(110, 60);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.buttonUpload.Size = new System.Drawing.Size(133, 82);
            this.buttonUpload.TabIndex = 0;
            this.buttonUpload.Text = "📄 Add Material";
            this.buttonUpload.UseVisualStyleBackColor = false;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // panelCram
            // 
            this.panelCram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(253)))));
            this.panelCram.Controls.Add(this.lblCramSub);
            this.panelCram.Controls.Add(this.lblCramTitle);
            this.panelCram.Controls.Add(this.panelCramCard);
            this.panelCram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCram.Location = new System.Drawing.Point(0, 0);
            this.panelCram.Name = "panelCram";
            this.panelCram.Size = new System.Drawing.Size(965, 862);
            this.panelCram.TabIndex = 1;
            this.panelCram.Visible = false;
            // 
            // lblCramSub
            // 
            this.lblCramSub.AutoSize = true;
            this.lblCramSub.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCramSub.ForeColor = System.Drawing.Color.Gray;
            this.lblCramSub.Location = new System.Drawing.Point(45, 65);
            this.lblCramSub.Name = "lblCramSub";
            this.lblCramSub.Size = new System.Drawing.Size(288, 19);
            this.lblCramSub.TabIndex = 2;
            this.lblCramSub.Text = "Organize your study material into digestible topics";
            // 
            // lblCramTitle
            // 
            this.lblCramTitle.AutoSize = true;
            this.lblCramTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblCramTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblCramTitle.Location = new System.Drawing.Point(40, 25);
            this.lblCramTitle.Name = "lblCramTitle";
            this.lblCramTitle.Size = new System.Drawing.Size(193, 37);
            this.lblCramTitle.TabIndex = 1;
            this.lblCramTitle.Text = "🧠 Cram Mode";
            // 
            // panelCramCard
            // 
            this.panelCramCard.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelCramCard.Controls.Add(this.btnGenerateTopics);
            this.panelCramCard.Controls.Add(this.comboCramSubject);
            this.panelCramCard.Controls.Add(this.lblCramSelect);
            this.panelCramCard.Location = new System.Drawing.Point(45, 100);
            this.panelCramCard.Name = "panelCramCard";
            this.panelCramCard.Size = new System.Drawing.Size(400, 200);
            this.panelCramCard.TabIndex = 0;
            // 
            // btnGenerateTopics
            // 
            this.btnGenerateTopics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnGenerateTopics.FlatAppearance.BorderSize = 0;
            this.btnGenerateTopics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateTopics.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGenerateTopics.ForeColor = System.Drawing.Color.White;
            this.btnGenerateTopics.Location = new System.Drawing.Point(20, 100);
            this.btnGenerateTopics.Name = "btnGenerateTopics";
            this.btnGenerateTopics.Size = new System.Drawing.Size(360, 40);
            this.btnGenerateTopics.TabIndex = 2;
            this.btnGenerateTopics.Text = "✨ Generate Topics";
            this.btnGenerateTopics.UseVisualStyleBackColor = false;
            // 
            // comboCramSubject
            // 
            this.comboCramSubject.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboCramSubject.FormattingEnabled = true;
            this.comboCramSubject.Location = new System.Drawing.Point(20, 50);
            this.comboCramSubject.Name = "comboCramSubject";
            this.comboCramSubject.Size = new System.Drawing.Size(360, 25);
            this.comboCramSubject.TabIndex = 1;
            this.comboCramSubject.Text = "Choose a document...";
            // 
            // lblCramSelect
            // 
            this.lblCramSelect.AutoSize = true;
            this.lblCramSelect.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCramSelect.Location = new System.Drawing.Point(18, 20);
            this.lblCramSelect.Name = "lblCramSelect";
            this.lblCramSelect.Size = new System.Drawing.Size(142, 19);
            this.lblCramSelect.TabIndex = 0;
            this.lblCramSelect.Text = "Select Study Material";
            // 
            // panelFocus
            // 
            this.panelFocus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(253)))));
            this.panelFocus.Controls.Add(this.panelFocusTimer);
            this.panelFocus.Controls.Add(this.lblFocusTitle);
            this.panelFocus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFocus.Location = new System.Drawing.Point(0, 0);
            this.panelFocus.Name = "panelFocus";
            this.panelFocus.Size = new System.Drawing.Size(965, 862);
            this.panelFocus.TabIndex = 2;
            this.panelFocus.Visible = false;
            // 
            // panelFocusTimer
            // 
            this.panelFocusTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(237)))));
            this.panelFocusTimer.Controls.Add(this.lblFocusTimer);
            this.panelFocusTimer.Controls.Add(this.lblFocusStatus);
            this.panelFocusTimer.Controls.Add(this.btnFocusReset);
            this.panelFocusTimer.Controls.Add(this.btnFocusStart);
            this.panelFocusTimer.Location = new System.Drawing.Point(50, 100);
            this.panelFocusTimer.Name = "panelFocusTimer";
            this.panelFocusTimer.Size = new System.Drawing.Size(350, 300);
            this.panelFocusTimer.TabIndex = 1;
            // 
            // lblFocusTimer
            // 
            this.lblFocusTimer.AutoSize = true;
            this.lblFocusTimer.Font = new System.Drawing.Font("Segoe UI", 60F, System.Drawing.FontStyle.Bold);
            this.lblFocusTimer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.lblFocusTimer.Location = new System.Drawing.Point(35, 50);
            this.lblFocusTimer.Name = "lblFocusTimer";
            this.lblFocusTimer.Size = new System.Drawing.Size(274, 106);
            this.lblFocusTimer.TabIndex = 2;
            this.lblFocusTimer.Text = "25:00";
            // 
            // lblFocusStatus
            // 
            this.lblFocusStatus.AutoSize = true;
            this.lblFocusStatus.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblFocusStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblFocusStatus.Location = new System.Drawing.Point(120, 160);
            this.lblFocusStatus.Name = "lblFocusStatus";
            this.lblFocusStatus.Size = new System.Drawing.Size(106, 21);
            this.lblFocusStatus.TabIndex = 3;
            this.lblFocusStatus.Text = "💪 Focus Time";
            // 
            // btnFocusReset
            // 
            this.btnFocusReset.BackColor = System.Drawing.Color.White;
            this.btnFocusReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFocusReset.Location = new System.Drawing.Point(180, 220);
            this.btnFocusReset.Name = "btnFocusReset";
            this.btnFocusReset.Size = new System.Drawing.Size(100, 40);
            this.btnFocusReset.TabIndex = 1;
            this.btnFocusReset.Text = "Reset";
            this.btnFocusReset.UseVisualStyleBackColor = false;
            // 
            // btnFocusStart
            // 
            this.btnFocusStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnFocusStart.FlatAppearance.BorderSize = 0;
            this.btnFocusStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFocusStart.ForeColor = System.Drawing.Color.White;
            this.btnFocusStart.Location = new System.Drawing.Point(60, 220);
            this.btnFocusStart.Name = "btnFocusStart";
            this.btnFocusStart.Size = new System.Drawing.Size(100, 40);
            this.btnFocusStart.TabIndex = 0;
            this.btnFocusStart.Text = "▶ Start";
            this.btnFocusStart.UseVisualStyleBackColor = false;
            // 
            // lblFocusTitle
            // 
            this.lblFocusTitle.AutoSize = true;
            this.lblFocusTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblFocusTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.lblFocusTitle.Location = new System.Drawing.Point(40, 25);
            this.lblFocusTitle.Name = "lblFocusTitle";
            this.lblFocusTitle.Size = new System.Drawing.Size(200, 37);
            this.lblFocusTitle.TabIndex = 0;
            this.lblFocusTitle.Text = "⏱ Focus Mode";
            // 
            // panelExam
            // 
            this.panelExam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(253)))));
            this.panelExam.Controls.Add(this.panelExamContent);
            this.panelExam.Controls.Add(this.lblExamTitle);
            this.panelExam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExam.Location = new System.Drawing.Point(0, 0);
            this.panelExam.Name = "panelExam";
            this.panelExam.Size = new System.Drawing.Size(965, 862);
            this.panelExam.TabIndex = 3;
            this.panelExam.Visible = false;
            // 
            // panelExamContent
            // 
            this.panelExamContent.BackColor = System.Drawing.Color.White;
            this.panelExamContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelExamContent.Controls.Add(this.lblExamQuestion);
            this.panelExamContent.Controls.Add(this.radioOption4);
            this.panelExamContent.Controls.Add(this.radioOption3);
            this.panelExamContent.Controls.Add(this.radioOption2);
            this.panelExamContent.Controls.Add(this.radioOption1);
            this.panelExamContent.Controls.Add(this.btnExamSubmit);
            this.panelExamContent.Controls.Add(this.btnExamNext);
            this.panelExamContent.Controls.Add(this.btnExamPrev);
            this.panelExamContent.Location = new System.Drawing.Point(50, 100);
            this.panelExamContent.Name = "panelExamContent";
            this.panelExamContent.Size = new System.Drawing.Size(600, 400);
            this.panelExamContent.TabIndex = 1;
            // 
            // lblExamQuestion
            // 
            this.lblExamQuestion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblExamQuestion.Location = new System.Drawing.Point(20, 20);
            this.lblExamQuestion.Name = "lblExamQuestion";
            this.lblExamQuestion.Size = new System.Drawing.Size(550, 60);
            this.lblExamQuestion.TabIndex = 7;
            this.lblExamQuestion.Text = "Question 1: What is the powerhouse of the cell?";
            // 
            // radioOption4
            // 
            this.radioOption4.AutoSize = true;
            this.radioOption4.Location = new System.Drawing.Point(30, 200);
            this.radioOption4.Name = "radioOption4";
            this.radioOption4.Size = new System.Drawing.Size(85, 24);
            this.radioOption4.TabIndex = 6;
            this.radioOption4.TabStop = true;
            this.radioOption4.Text = "Option D";
            this.radioOption4.UseVisualStyleBackColor = true;
            // 
            // radioOption3
            // 
            this.radioOption3.AutoSize = true;
            this.radioOption3.Location = new System.Drawing.Point(30, 160);
            this.radioOption3.Name = "radioOption3";
            this.radioOption3.Size = new System.Drawing.Size(84, 24);
            this.radioOption3.TabIndex = 5;
            this.radioOption3.TabStop = true;
            this.radioOption3.Text = "Option C";
            this.radioOption3.UseVisualStyleBackColor = true;
            // 
            // radioOption2
            // 
            this.radioOption2.AutoSize = true;
            this.radioOption2.Location = new System.Drawing.Point(30, 120);
            this.radioOption2.Name = "radioOption2";
            this.radioOption2.Size = new System.Drawing.Size(84, 24);
            this.radioOption2.TabIndex = 4;
            this.radioOption2.TabStop = true;
            this.radioOption2.Text = "Option B";
            this.radioOption2.UseVisualStyleBackColor = true;
            // 
            // radioOption1
            // 
            this.radioOption1.AutoSize = true;
            this.radioOption1.Location = new System.Drawing.Point(30, 80);
            this.radioOption1.Name = "radioOption1";
            this.radioOption1.Size = new System.Drawing.Size(85, 24);
            this.radioOption1.TabIndex = 3;
            this.radioOption1.TabStop = true;
            this.radioOption1.Text = "Option A";
            this.radioOption1.UseVisualStyleBackColor = true;
            // 
            // btnExamSubmit
            // 
            this.btnExamSubmit.BackColor = System.Drawing.Color.Green;
            this.btnExamSubmit.ForeColor = System.Drawing.Color.White;
            this.btnExamSubmit.Location = new System.Drawing.Point(480, 340);
            this.btnExamSubmit.Name = "btnExamSubmit";
            this.btnExamSubmit.Size = new System.Drawing.Size(100, 40);
            this.btnExamSubmit.TabIndex = 2;
            this.btnExamSubmit.Text = "Submit";
            this.btnExamSubmit.UseVisualStyleBackColor = false;
            // 
            // btnExamNext
            // 
            this.btnExamNext.Location = new System.Drawing.Point(120, 340);
            this.btnExamNext.Name = "btnExamNext";
            this.btnExamNext.Size = new System.Drawing.Size(80, 40);
            this.btnExamNext.TabIndex = 1;
            this.btnExamNext.Text = "Next";
            this.btnExamNext.UseVisualStyleBackColor = true;
            // 
            // btnExamPrev
            // 
            this.btnExamPrev.Location = new System.Drawing.Point(20, 340);
            this.btnExamPrev.Name = "btnExamPrev";
            this.btnExamPrev.Size = new System.Drawing.Size(80, 40);
            this.btnExamPrev.TabIndex = 0;
            this.btnExamPrev.Text = "Prev";
            this.btnExamPrev.UseVisualStyleBackColor = true;
            // 
            // lblExamTitle
            // 
            this.lblExamTitle.AutoSize = true;
            this.lblExamTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblExamTitle.ForeColor = System.Drawing.Color.Green;
            this.lblExamTitle.Location = new System.Drawing.Point(40, 25);
            this.lblExamTitle.Name = "lblExamTitle";
            this.lblExamTitle.Size = new System.Drawing.Size(206, 37);
            this.lblExamTitle.TabIndex = 0;
            this.lblExamTitle.Text = "📝 Mock Exam";
            // 
            // panelQuiz
            // 
            this.panelQuiz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(253)))));
            this.panelQuiz.Controls.Add(this.lblQuizTitle);
            this.panelQuiz.Controls.Add(this.panelQuizCard);
            this.panelQuiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelQuiz.Location = new System.Drawing.Point(0, 0);
            this.panelQuiz.Name = "panelQuiz";
            this.panelQuiz.Size = new System.Drawing.Size(965, 862);
            this.panelQuiz.TabIndex = 4;
            this.panelQuiz.Visible = false;
            // 
            // lblQuizTitle
            // 
            this.lblQuizTitle.AutoSize = true;
            this.lblQuizTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblQuizTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lblQuizTitle.Location = new System.Drawing.Point(40, 25);
            this.lblQuizTitle.Name = "lblQuizTitle";
            this.lblQuizTitle.Size = new System.Drawing.Size(184, 37);
            this.lblQuizTitle.TabIndex = 1;
            this.lblQuizTitle.Text = "❓ Quiz Mode";
            // 
            // panelQuizCard
            // 
            this.panelQuizCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.panelQuizCard.Controls.Add(this.btnQuizStart);
            this.panelQuizCard.Controls.Add(this.comboQuizDoc);
            this.panelQuizCard.Controls.Add(this.lblQuizSelect);
            this.panelQuizCard.Location = new System.Drawing.Point(50, 100);
            this.panelQuizCard.Name = "panelQuizCard";
            this.panelQuizCard.Size = new System.Drawing.Size(400, 200);
            this.panelQuizCard.TabIndex = 0;
            // 
            // btnQuizStart
            // 
            this.btnQuizStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnQuizStart.FlatAppearance.BorderSize = 0;
            this.btnQuizStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuizStart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnQuizStart.ForeColor = System.Drawing.Color.White;
            this.btnQuizStart.Location = new System.Drawing.Point(20, 100);
            this.btnQuizStart.Name = "btnQuizStart";
            this.btnQuizStart.Size = new System.Drawing.Size(360, 40);
            this.btnQuizStart.TabIndex = 2;
            this.btnQuizStart.Text = "▶ Start Quiz";
            this.btnQuizStart.UseVisualStyleBackColor = false;
            // 
            // comboQuizDoc
            // 
            this.comboQuizDoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboQuizDoc.FormattingEnabled = true;
            this.comboQuizDoc.Location = new System.Drawing.Point(20, 50);
            this.comboQuizDoc.Name = "comboQuizDoc";
            this.comboQuizDoc.Size = new System.Drawing.Size(360, 25);
            this.comboQuizDoc.TabIndex = 1;
            this.comboQuizDoc.Text = "Select Material...";
            // 
            // lblQuizSelect
            // 
            this.lblQuizSelect.AutoSize = true;
            this.lblQuizSelect.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblQuizSelect.Location = new System.Drawing.Point(18, 20);
            this.lblQuizSelect.Name = "lblQuizSelect";
            this.lblQuizSelect.Size = new System.Drawing.Size(97, 19);
            this.lblQuizSelect.TabIndex = 0;
            this.lblQuizSelect.Text = "Start a Quiz";
            // 
            // panelSettings
            // 
            this.panelSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(253)))));
            this.panelSettings.Controls.Add(this.lblSettingsTitle);
            this.panelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSettings.Location = new System.Drawing.Point(0, 0);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(965, 862);
            this.panelSettings.TabIndex = 5;
            this.panelSettings.Visible = false;
            // 
            // lblSettingsTitle
            // 
            this.lblSettingsTitle.AutoSize = true;
            this.lblSettingsTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblSettingsTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSettingsTitle.Location = new System.Drawing.Point(40, 25);
            this.lblSettingsTitle.Name = "lblSettingsTitle";
            this.lblSettingsTitle.Size = new System.Drawing.Size(152, 37);
            this.lblSettingsTitle.TabIndex = 0;
            this.lblSettingsTitle.Text = "⚙ Settings";

            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1225, 862);
            this.Controls.Add(this.mainContentPanel);
            this.Controls.Add(this.sidebar);
            this.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.DoubleBuffered = true;
            this.Name = "Dashboard";
            this.Text = "AI Study Buddy";
            this.sidebar.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.mainContentPanel.ResumeLayout(false);
            this.panelChat.ResumeLayout(false);
            this.panelChatMessages.ResumeLayout(false);
            this.panelChatHeader.ResumeLayout(false);
            this.panelChatHeader.PerformLayout();
            this.panelChatInput.ResumeLayout(false);
            this.tableLayoutPanelInput.ResumeLayout(false);
            this.tableLayoutPanelInput.PerformLayout();
            this.panelInputContainer.ResumeLayout(false);
            this.panelCram.ResumeLayout(false);
            this.panelCram.PerformLayout();
            this.panelCramCard.ResumeLayout(false);
            this.panelCramCard.PerformLayout();
            this.panelFocus.ResumeLayout(false);
            this.panelFocus.PerformLayout();
            this.panelFocusTimer.ResumeLayout(false);
            this.panelFocusTimer.PerformLayout();
            this.panelExam.ResumeLayout(false);
            this.panelExam.PerformLayout();
            this.panelExamContent.ResumeLayout(false);
            this.panelExamContent.PerformLayout();
            this.panelQuiz.ResumeLayout(false);
            this.panelQuiz.PerformLayout();
            this.panelQuizCard.ResumeLayout(false);
            this.panelQuizCard.PerformLayout();
            this.panelSettings.ResumeLayout(false);
            this.panelSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox chatHistoryBox;
        private System.Windows.Forms.RichTextBox textBox1;
        private System.Windows.Forms.Button buttonSend; // Renamed from button1
        private System.Windows.Forms.Button buttonUpload; // Renamed from buttonLoadPdf
        private System.Windows.Forms.Panel sidebar;
        private System.Windows.Forms.Panel mainContentPanel;
        private System.Windows.Forms.Panel panelChat;
        private System.Windows.Forms.Panel panelChatHeader;
        private System.Windows.Forms.Label lblChatTitle;
        private System.Windows.Forms.Label lblChatSubtitle;
        private System.Windows.Forms.Button buttonHighlighter;
        private System.Windows.Forms.Panel panelChatMessages;
        private System.Windows.Forms.Panel panelChatInput;
        private System.Windows.Forms.Panel panelInputContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInput;
        private System.Windows.Forms.Panel panelSendButtonContainer;
        private System.Windows.Forms.Panel panelCram;
        private System.Windows.Forms.Label lblCramTitle;
        private System.Windows.Forms.Label lblCramSub;
        private System.Windows.Forms.Panel panelCramCard;
        private System.Windows.Forms.Label lblCramSelect;
        private System.Windows.Forms.ComboBox comboCramSubject;
        private System.Windows.Forms.Button btnGenerateTopics;
        private System.Windows.Forms.Panel panelFocus;
        private System.Windows.Forms.Label lblFocusTitle;
        private System.Windows.Forms.Panel panelFocusTimer;
        private System.Windows.Forms.Label lblFocusTimer;
        private System.Windows.Forms.Label lblFocusStatus;
        private System.Windows.Forms.Button btnFocusStart;
        private System.Windows.Forms.Button btnFocusReset;
        private System.Windows.Forms.Panel panelExam;
        private System.Windows.Forms.Label lblExamTitle;
        private System.Windows.Forms.Panel panelExamContent;
        private System.Windows.Forms.Label lblExamQuestion;
        private System.Windows.Forms.RadioButton radioOption1;
        private System.Windows.Forms.RadioButton radioOption2;
        private System.Windows.Forms.RadioButton radioOption3;
        private System.Windows.Forms.RadioButton radioOption4;
        private System.Windows.Forms.Button btnExamNext;
        private System.Windows.Forms.Button btnExamPrev;
        private System.Windows.Forms.Button btnExamSubmit;
        private System.Windows.Forms.Panel panelQuiz;
        private System.Windows.Forms.Label lblQuizTitle;
        private System.Windows.Forms.Panel panelQuizCard;
        private System.Windows.Forms.Label lblQuizSelect;
        private System.Windows.Forms.ComboBox comboQuizDoc;
        private System.Windows.Forms.Button btnQuizStart;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.Label lblSettingsTitle;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button buttonQuiz;
        private System.Windows.Forms.Button buttonExam;
        private System.Windows.Forms.Button buttonFocus;
        private System.Windows.Forms.Button buttonCram;
        private System.Windows.Forms.Button buttonChat;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblAppTitle;

        // These fields are for compatibility with existing code in Dashboard.cs
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonLoadPdf;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Panel bottomPanel;
    }
}