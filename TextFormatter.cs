using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ai_Study_Buddy___Gemini
{
    public static class TextFormatter
    {
        // Appends formatted text to a RichTextBox. Supports:
        // - Bullet lines starting with '* ', '- ', or '• '
        // - Bold segments wrapped in **double asterisks**
        // - Headers using '### ' (renders as bold larger text)
        public static void AppendFormatted(RichTextBox rtb, string text)
        {
            if (rtb == null) throw new ArgumentNullException(nameof(rtb));
            if (string.IsNullOrEmpty(text)) return;

            // Normalize line endings
            text = text.Replace("\r\n", "\n").Replace("\r", "\n");
            var lines = text.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrEmpty(line) && i < lines.Length - 1)
                {
                    rtb.AppendText("\n");
                    continue;
                }

                // Headers: ### Header
                if (line.TrimStart().StartsWith("### "))
                {
                    string header = line.TrimStart().Substring(4).Trim();
                    var headerFont = new Font(rtb.Font.FontFamily, rtb.Font.Size + 1.5f, FontStyle.Bold);
                    rtb.SelectionFont = headerFont;
                    rtb.AppendText(header);
                    rtb.SelectionFont = rtb.Font;
                    if (i < lines.Length - 1) rtb.AppendText("\n");
                    continue;
                }

                bool isBullet = false;
                string content = line;
                string trimmed = line.TrimStart();
                if (trimmed.StartsWith("* ") || trimmed.StartsWith("- ") || trimmed.StartsWith("• "))
                {
                    isBullet = true;
                    int bulletIndex = line.IndexOfAny(new[] { '*', '-', '•' });
                    if (bulletIndex != -1)
                        content = line.Substring(bulletIndex + 1).TrimStart();
                }

                rtb.SelectionBullet = isBullet;
                rtb.BulletIndent = isBullet ? 20 : 0;

                // Handle bold markers **
                var parts = content.Split(new[] { "**" }, StringSplitOptions.None);
                for (int p = 0; p < parts.Length; p++)
                {
                    if (p % 2 == 1)
                    {
                        rtb.SelectionFont = new Font(rtb.Font, FontStyle.Bold);
                        rtb.AppendText(parts[p]);
                        rtb.SelectionFont = rtb.Font;
                    }
                    else
                    {
                        rtb.AppendText(parts[p]);
                    }
                }

                if (i < lines.Length - 1)
                    rtb.AppendText("\n");
            }

            rtb.SelectionBullet = false;
            rtb.BulletIndent = 0;
            rtb.ScrollToCaret();
        }
    }
}
