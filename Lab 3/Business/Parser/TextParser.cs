using Business.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Business.Parser
{
    public class TextParser
    {

        private static readonly Regex TokenRegex = new Regex(
            @"(?<Word>[\p{L}\p{N}]+(?:[-'][\p{L}\p{N}]+)*)|(?<Punct>\.{2,}|[?!]{2,}|[^\s\p{L}\p{N}])",
            RegexOptions.Compiled | RegexOptions.Singleline);


        public Text ParseFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл с исходным текстом не найден: {filePath}");

            string content = File.ReadAllText(filePath, Encoding.UTF8);
            return ParseText(content);
        }


        public Text ParseText(string rawText)
        {
            var text = new Text();
            if (string.IsNullOrWhiteSpace(rawText))
                return text;

            var currentSentence = new Sentence();
            MatchCollection matches = TokenRegex.Matches(rawText);

            foreach (Match match in matches)
            {
                if (match.Groups["Word"].Success)
                {
                    currentSentence.AddToken(new Word(match.Groups["Word"].Value));
                }
                else if (match.Groups["Punct"].Success)
                {
                    var punct = new Punctuation(match.Groups["Punct"].Value);
                    currentSentence.AddToken(punct);


                    if (punct.IsSentenceEnding)
                    {
                        text.AddSentence(currentSentence);
                        currentSentence = new Sentence();
                    }
                }
            }


            if (currentSentence.Tokens.Count > 0)
            {
                text.AddSentence(currentSentence);
            }

            return text;
        }


        public static HashSet<string> LoadStopWordsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            string content = File.ReadAllText(filePath, Encoding.UTF8);
            var words = content.Split(new[] { '\r', '\n', ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            return new HashSet<string>(words, StringComparer.OrdinalIgnoreCase);
        }
    }
}
