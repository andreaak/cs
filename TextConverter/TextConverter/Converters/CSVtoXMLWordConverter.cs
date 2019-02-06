using System.Threading.Tasks;

namespace TextConverter
{
    public class CSVtoXMLWordConverter : WordConverter, IWordConverter
    {
        public string InExtension => ".csv";
        public string OutExtension => ".xml";

        public async Task ConvertFileAsync(string filePath, string outputFilePath, WordData data)
        {
            var words = CSVHelper.ReadWords(filePath);
            await ProcessWordsAsync(words, data);
            XMLHelper.WriteWords(outputFilePath, words);
            CSVHelper.WriteWords(outputFilePath + ".csv", words);
        }
    }
}
