using System.Collections.Generic;

namespace Ai_Study_Buddy___Gemini
{
    public enum StudyModeType
    {
        Quiz,
        MockExam,
        Cram,
        Focus
    }

    public class Question
    {
        public string Text { get; set; } = string.Empty;
        public List<AnswerOption> Options { get; set; } = new();
        public int CorrectIndex { get; set; }
        public bool IsFlagged { get; set; }
        public int? UserChoice { get; set; }
    }

    public class AnswerOption
    {
        public string Text { get; set; } = string.Empty;
    }

    public class AppSettings
    {
        public string AiName { get; set; } = "Bud";
        public string Theme { get; set; } = "Light";
        public int FontSize { get; set; } = 11;
        public string ResponseStyle { get; set; } = "Detailed";
        public bool AutoSave { get; set; } = true;
        public bool NotificationsEnabled { get; set; }
        public string DefaultMode { get; set; } = "Chat";

        public AppSettings Clone()
        {
            return new AppSettings
            {
                AiName = AiName,
                Theme = Theme,
                FontSize = FontSize,
                ResponseStyle = ResponseStyle,
                AutoSave = AutoSave,
                NotificationsEnabled = NotificationsEnabled,
                DefaultMode = DefaultMode
            };
        }
    }
}