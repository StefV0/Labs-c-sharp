using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Business.Tokens
{
    [XmlRoot("text")]
    public class Text
    {
        
        [XmlElement("sentence")]
        public List<Sentence> Sentences { get; set; } = new List<Sentence>();

        public Text() { }

        public Text(IEnumerable<Sentence> sentences)
        {
            Sentences.AddRange(sentences);
        }

        public void AddSentence(Sentence sentence)
        {
            if (sentence != null && sentence.Tokens.Count > 0)
            {
                Sentences.Add(sentence);
            }
        }

        
        public IEnumerable<Sentence> GetSentencesOrderedByWordCount()
        {
            return Sentences.OrderBy(s => s.WordCount).ToList();
        }

        
        public IEnumerable<Sentence> GetSentencesOrderedByLength()
        {
            return Sentences.OrderBy(s => s.Length).ToList();
        }

        
        public IEnumerable<string> FindUniqueWordsInInterrogativeSentences(int length)
        {
            if (length <= 0)
                return Enumerable.Empty<string>();

            
            return Sentences
                .Where(s => s.IsInterrogative)
                .SelectMany(s => s.GetWords())
                .Where(w => w.Length == length)
                .Select(w => w.Value)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        
        public int RemoveWordsByLengthStartingWithConsonant(int length)
        {
            int removedCount = 0;
            foreach (var sentence in Sentences)
            {
                removedCount += sentence.Tokens.RemoveAll(token =>
                    token is Word word &&
                    word.Length == length &&
                    word.StartsWithConsonant());
            }
            return removedCount;
        }

        
        public bool ReplaceWordsInSentence(int sentenceIndex, int wordLength, string replacement)
        {
            if (sentenceIndex < 0 || sentenceIndex >= Sentences.Count || replacement == null)
                return false;

            var sentence = Sentences[sentenceIndex];
            bool modified = false;

            for (int i = 0; i < sentence.Tokens.Count; i++)
            {
                if (sentence.Tokens[i] is Word word && word.Length == wordLength)
                {
                    sentence.Tokens[i] = new Word(replacement);
                    modified = true;
                }
            }

            return modified;
        }

        
        public int RemoveStopWords(IEnumerable<string> stopWords)
        {
            if (stopWords == null)
                return 0;

            var stopWordsSet = new HashSet<string>(
                stopWords.Where(w => !string.IsNullOrWhiteSpace(w)).Select(w => w.Trim()),
                StringComparer.OrdinalIgnoreCase);

            int removedCount = 0;
            foreach (var sentence in Sentences)
            {
                removedCount += sentence.Tokens.RemoveAll(token =>
                    token is Word word && stopWordsSet.Contains(word.Value));
            }
            return removedCount;
        }

        
        public string ExportToXml(string? filePath = null)
        {
            var serializer = new XmlSerializer(typeof(Text));

            
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "    ",
                Encoding = new UTF8Encoding(false), 
                OmitXmlDeclaration = true 
            };

            using var memoryStream = new MemoryStream();
            using (var writer = XmlWriter.Create(memoryStream, settings))
            {
                serializer.Serialize(writer, this, namespaces);
            }

            string xmlBody = Encoding.UTF8.GetString(memoryStream.ToArray());
            string header = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";
            string fullXml = header + Environment.NewLine + xmlBody;

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                File.WriteAllText(filePath, fullXml, new UTF8Encoding(false));
            }

            return fullXml;
        }

        
        public Text Clone()
        {
            var clonedText = new Text();
            foreach (var sentence in Sentences)
            {
                var clonedSentence = new Sentence();
                foreach (var token in sentence.Tokens)
                {
                    if (token is Word w)
                        clonedSentence.AddToken(new Word(w.Value));
                    else if (token is Punctuation p)
                        clonedSentence.AddToken(new Punctuation(p.Value));
                }
                clonedText.AddSentence(clonedSentence);
            }
            return clonedText;
        }

        public Sentence this[int i]
        {
            get { return Sentences[i]; }
        }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, Sentences.Select(s => s.ToString()));
        }

    }
}
