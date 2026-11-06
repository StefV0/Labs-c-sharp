using System.Collections.Generic;

namespace Business.Tokens
{
    public class FreqLine
    {
        public string Word { get; set; }
        public int Frequency { get; set; }
        public List<int> SentenceIndices { get; set; }

        public FreqLine(string word, int frequency, List<int> sentenceIndices)
        {
            Word = word;
            Frequency = frequency;
            SentenceIndices = sentenceIndices;
        }

        public override string ToString()
        {
            string sentences = string.Join(", ", SentenceIndices);
            return $"{Word,-20} | Частота: {Frequency,2} | Предложения: [{sentences}]";
        }
    }
}
