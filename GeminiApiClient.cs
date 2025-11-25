using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ai_Study_Buddy___Gemini
{
    public class GeminiApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _modelUrl;

        public GeminiApiClient(HttpClient client, string apiKey)
        {
            _httpClient = client;
            _apiKey = apiKey;
            _modelUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";
        }

        public async Task<string> GenerateTextAsync(string prompt)
        {
            var body = new
            {
                contents = new[]
                {
                    new {
                        role = "user",
                        parts = new[] { new { text = prompt } }
                    }
                }
            };

            string json = JsonSerializer.Serialize(body);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PostAsync(_modelUrl, content);
            var raw = await resp.Content.ReadAsStringAsync();
            if (!resp.IsSuccessStatusCode) return $"API Error: {resp.StatusCode}\n{raw}";
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
                        var part0 = partsEl[0];
                        if (part0.TryGetProperty("text", out var textEl))
                            return textEl.GetString() ?? raw;
                    }
                }
            }
            catch { }
            return raw;
        }

        public async Task<List<Question>> GenerateQuizQuestionsAsync(string sourceText, int count = 5)
        {
            var prompt = $"Generate {count} multiple-choice questions (A-D) based ONLY on this content:\n{sourceText}\n" +
                         "Strictly follow this format for each question:\n" +
                         "Q: [Question Text]\n" +
                         "A) [Option A]\n" +
                         "B) [Option B]\n" +
                         "C) [Option C]\n" +
                         "D) [Option D]\n" +
                         "Answer: [Correct Option Letter]\n" +
                         "---\n" +
                         "Ensure there is a '---' separator between questions.";
            string text = await GenerateTextAsync(prompt);
            return ParseQuestions(text);
        }

        public async Task<List<Question>> GenerateMockExamAsync(string sourceText, int count = 20, string difficulty = "mixed")
        {
            var prompt = $"Create a mock exam of {count} {difficulty} difficulty multiple-choice questions based on:\n{sourceText}\n" +
                         "Strictly follow this format for each question:\n" +
                         "Q: [Question Text]\n" +
                         "A) [Option A]\n" +
                         "B) [Option B]\n" +
                         "C) [Option C]\n" +
                         "D) [Option D]\n" +
                         "Answer: [Correct Option Letter]\n" +
                         "---\n" +
                         "Ensure there is a '---' separator between questions.";
            string text = await GenerateTextAsync(prompt);
            return ParseQuestions(text);
        }

        public async Task<List<string>> SummarizeTopicsAsync(string sourceText, int maxTopics = 10)
        {
            var prompt = $"Summarize key topics (max {maxTopics}) from this study material. Provide bullet list only:\n{sourceText}";
            string text = await GenerateTextAsync(prompt);
            var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var list = new List<string>();
            foreach (var l in lines)
            {
                var trim = l.TrimStart('-', '*', ' ');
                if (!string.IsNullOrWhiteSpace(trim))
                    list.Add(trim.Trim());
            }
            return list;
        }

        private List<Question> ParseQuestions(string raw)
        {
            var questions = new List<Question>();
            // Normalize line endings and split by separator
            var normalized = raw.Replace("\r\n", "\n").Replace("\r", "\n");
            var blocks = normalized.Split(new[] { "---" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var b in blocks)
            {
                var lines = b.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var q = new Question();
                foreach (var line in lines)
                {
                    var t = line.Trim();
                    if (string.IsNullOrWhiteSpace(t)) continue;

                    if (t.StartsWith("Q:", StringComparison.OrdinalIgnoreCase))
                        q.Text = t.Substring(2).Trim();
                    else if (t.StartsWith("A)", StringComparison.OrdinalIgnoreCase) || t.StartsWith("A.", StringComparison.OrdinalIgnoreCase))
                        q.Options.Add(new AnswerOption { Text = t.Substring(2).Trim() });
                    else if (t.StartsWith("B)", StringComparison.OrdinalIgnoreCase) || t.StartsWith("B.", StringComparison.OrdinalIgnoreCase))
                        q.Options.Add(new AnswerOption { Text = t.Substring(2).Trim() });
                    else if (t.StartsWith("C)", StringComparison.OrdinalIgnoreCase) || t.StartsWith("C.", StringComparison.OrdinalIgnoreCase))
                        q.Options.Add(new AnswerOption { Text = t.Substring(2).Trim() });
                    else if (t.StartsWith("D)", StringComparison.OrdinalIgnoreCase) || t.StartsWith("D.", StringComparison.OrdinalIgnoreCase))
                        q.Options.Add(new AnswerOption { Text = t.Substring(2).Trim() });
                    else if (t.StartsWith("Answer:", StringComparison.OrdinalIgnoreCase))
                    {
                        var parts = t.Split(new[] { ':' }, 2);
                        if (parts.Length > 1)
                        {
                            var letter = parts[1].Trim().ToUpperInvariant();
                            // Handle cases like "Answer: A) Option Text" or just "Answer: A"
                            if (letter.Length > 1 && (letter[1] == ')' || letter[1] == '.'))
                                letter = letter.Substring(0, 1);
                            else if (letter.Length > 0)
                                letter = letter.Substring(0, 1);

                            q.CorrectIndex = letter switch
                            {
                                "A" => 0,
                                "B" => 1,
                                "C" => 2,
                                "D" => 3,
                                _ => 0
                            };
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(q.Text) && q.Options.Count >= 2)
                    questions.Add(q);
            }
            return questions;
        }
    }
}