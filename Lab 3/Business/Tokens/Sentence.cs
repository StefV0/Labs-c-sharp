using Business.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Business.Tokens
{
    [XmlType("sentence")]
    public class Sentence : IToken
    {

        [XmlIgnore]
        public List<IToken> Tokens { get; set; } = new List<IToken>();


        [XmlElement("word")]
        public List<Word> SerializableWords
        {
            get => Tokens.OfType<Word>().ToList();
            set
            {
                if (value != null)
                {
                    Tokens = value.Cast<IToken>().ToList();
                }
            }
        }

        public Sentence() { }

        public Sentence(IEnumerable<IToken> tokens)
        {
            Tokens.AddRange(tokens);
        }


        public void AddToken(IToken token)
        {
            if (token != null)
            {
                Tokens.Add(token);
            }
        }


        public IEnumerable<Word> GetWords() => Tokens.OfType<Word>();

        
        [XmlIgnore]
        public int WordCount => Tokens.OfType<Word>().Count();

        
        [XmlIgnore]
        public int Length => Value.Length;

       
        [XmlIgnore]
        public bool IsInterrogative
        {
            get
            {
                var lastPunct = Tokens.OfType<Punctuation>().LastOrDefault();
                return lastPunct != null && lastPunct.IsQuestion;
            }
        }

        
        [XmlIgnore]
        public string Value => BuildSentenceString();

        private string BuildSentenceString()
        {
            if (Tokens.Count == 0)
                return string.Empty;

            var sb = new StringBuilder();
            string openBrackets = "([«„“";

            for (int i = 0; i < Tokens.Count; i++)
            {
                IToken current = Tokens[i];
                sb.Append(current.Value);

                if (i < Tokens.Count - 1)
                {
                    IToken next = Tokens[i + 1];

                    bool currentIsOpenPunct = current is Punctuation && openBrackets.Contains(current.Value);
                    bool nextIsPunct = next is Punctuation;
                    bool nextIsOpenOrDash = nextIsPunct && (openBrackets.Contains(next.Value) || next.Value == "—" || next.Value == "-");

                    
                    if (!currentIsOpenPunct && (!nextIsPunct || nextIsOpenOrDash))
                    {
                        sb.Append(' ');
                    }
                }
            }

            return sb.ToString();
        }

        public int RemoveStopWords(IEnumerable<string> stopWords)
        {
            if (stopWords == null)
                return 0;

            var stopWordsSet = new HashSet<string>(
                stopWords.Where(w => !string.IsNullOrWhiteSpace(w)).Select(w => w.Trim()),
                StringComparer.OrdinalIgnoreCase);

            int removedCount = 0;
            
                removedCount += this.Tokens.RemoveAll(token =>
                    token is Word word && stopWordsSet.Contains(word.Value));
           
            return removedCount;
        }

        public override string ToString() => Value;
    }
}
