using System.Text;

using Business;
using Business.Tokens;
using Business.Parser;

namespace TextTokenizationProject
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string sampleText = @"
Программирование — это искусство создания надежных программ.
Что такое токенизация текста и зачем она нужна разработчику?
Mary had a little lamb!
Как выбрать правильный алгоритм для парсинга больших файлов и баз данных?
Tokens are small blocks of text, such as words and punctuation.
Почему так важно разделять ответственности между классами в проекте?
Короткий пример.";

            

            TextParser parser = new TextParser();
            Text parsedText = parser.ParseText(sampleText);

            Console.WriteLine("--- ИСХОДНЫЙ РАСПАРСЕННЫЙ ТЕКСТ ---");
            PrintSentencesWithIndex(parsedText.Sentences);

            
            Console.WriteLine("\n--- 1. ПРЕДЛОЖЕНИЯ ПО ВОЗРАСТАНИЮ КОЛИЧЕСТВА СЛОВ ---");
            foreach (var s in parsedText.GetSentencesOrderedByWordCount())
            {
                Console.WriteLine($"[Слов: {s.WordCount,2}] {s}");
            }

            
            Console.WriteLine("\n--- 2. ПРЕДЛОЖЕНИЯ ПО ВОЗРАСТАНИЮ ДЛИНЫ (СИМВОЛОВ) ---");
            foreach (var s in parsedText.GetSentencesOrderedByLength())
            {
                Console.WriteLine($"[Длина: {s.Length,2}] {s}");
            }

            
            int targetQuestionWordLength = 5;
            Console.WriteLine($"\n--- 3. СЛОВА ДЛИНЫ {targetQuestionWordLength} В ВОПРОСИТЕЛЬНЫХ ПРЕДЛОЖЕНИЯХ (БЕЗ ПОВТОРОВ) ---");
            var uniqueQuestionWords = parsedText.FindUniqueWordsInInterrogativeSentences(targetQuestionWordLength);
            Console.WriteLine(uniqueQuestionWords.Any()
                ? string.Join(", ", uniqueQuestionWords)
                : "Слова указанной длины не найдены.");

            
            int lengthToRemove = 6;
            Text textForTask4 = parsedText.Clone();
            int removedConsonantWords = textForTask4.RemoveWordsByLengthStartingWithConsonant(lengthToRemove);
            Console.WriteLine($"\n--- 4. УДАЛЕНИЕ СЛОВ ДЛИНЫ {lengthToRemove}, НАЧИНАЮЩИХСЯ С СОГЛАСНОЙ (Удалено: {removedConsonantWords}) ---");
            Console.WriteLine(textForTask4);

            
            int sentenceIndexToModify = 2; 
            int wordLengthToReplace = 4;   
            string replacementSubstring = "ПЕПЫ";

            Text textForTask5 = parsedText.Clone();
            Console.WriteLine($"\n--- 5. ЗАМЕНА СЛОВ ДЛИНЫ {wordLengthToReplace} В ПРЕДЛОЖЕНИИ #{sentenceIndexToModify + 1} НА \"{replacementSubstring}\" ---");
            Console.WriteLine($"До:    {textForTask5.Sentences[sentenceIndexToModify]}");
            textForTask5.ReplaceWordsInSentence(sentenceIndexToModify, wordLengthToReplace, replacementSubstring);
            Console.WriteLine($"После: {textForTask5.Sentences[sentenceIndexToModify]}");

            
            Text textForTask6 = parsedText.Clone();
            var allStopWords = TextParser.LoadStopWordsFromFile(@"D:\C#\Labs\Lab 3\Lab 3\Stop words\stopwords_en.txt").Union(TextParser.LoadStopWordsFromFile(@"D:\C#\Labs\Lab 3\Lab 3\Stop words\stopwords_ru.txt")).ToList();
            Console.WriteLine(allStopWords.ToString());
            int removedStopWordsCount = textForTask6.RemoveStopWords(allStopWords);
            textForTask6.ExportToXml(@"D:\C#\Labs\Lab 3\Lab 3\Parse text\Parsed_twithout_sw.xml");
            Console.WriteLine($"\n--- 6. ТЕКСТ ПОСЛЕ УДАЛЕНИЯ СТОП-СЛОВ (Удалено стоп-слов: {removedStopWordsCount}) ---");
            Console.WriteLine(textForTask6);

            
            Console.WriteLine("\n--- 7. ЭКСПОРТ ОБЪЕКТА TEXT В XML---");
            string xmlOutput = parsedText.ExportToXml(filePath: @"D:\C#\Labs\Lab 3\Lab 3\Parse text\Parsed.xml");
            Console.WriteLine(xmlOutput);
        }

        private static void PrintSentencesWithIndex(IList<Sentence> sentences)
        {
            for (int i = 0; i < sentences.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sentences[i]} (Слов: {sentences[i].WordCount}, Длина: {sentences[i].Length})");
            }
        }
    }


  
}