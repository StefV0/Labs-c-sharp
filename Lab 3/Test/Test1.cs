using Business.Parser;
using Business.Tokens;

namespace Test
{
    [TestClass]
    public sealed class Test1
    {
        private static TextParser p = new TextParser();
        
        private static string t = "В 17:08 Влад, Новик(ов), закончил читать литературное произведение, \"мтоыоыомовыт\". Влад. Конец. Продолжение следует...";
        Text text = p.ParseText(t);
        [TestMethod]
        public void StopWordsLoadTestLength()
        {
            var words = TextParser.LoadStopWordsFromFile(@"D:\C#\Labs\Lab 3\Lab 3\Stop words\stopwords_ru.txt").ToList();
            Assert.HasCount(421, words);
        }

        [TestMethod]
        public void TokensLength()
        {
            int l = text.Sentences.Count();
            Assert.AreEqual(4, l);
        }

        [TestMethod]
        public void ReplaceTest()
        {
            string repl_text = text.ReplaceWordsInSentence(0, 4, "[НЕИЗВЕСТНЫЙ]").ToString();
            Assert.DoesNotContain("Влад", repl_text);
        }

        [TestMethod]
        public void RemoveStopWords()
        {
            string[] stopw = { "В", "чиТать" };
            text[0].RemoveStopWords(stopw);
            Assert.DoesNotContain("читать", text[0].ToString());
        }

        [TestMethod]
        public void TestConcodance()
        {
            var biggest = text.GetConcordance().OrderBy(item => item.Frequency).LastOrDefault();
            Assert.AreEqual("влад", biggest.Word);
        }
    }
}
