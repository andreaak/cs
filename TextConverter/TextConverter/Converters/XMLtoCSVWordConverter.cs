using System.Threading.Tasks;

namespace TextConverter
{
    public class XMLtoCSVWordConverter : WordConverter, IWordConverter
    {
        public string InExtension => ".xml";
        public string OutExtension => ".csv";

        public async Task ConvertFileAsync(string filePath, string outputFilePath, WordData data)
        {
            var words = XMLHelper.ReadWords(filePath);
            await ProcessWordsAsync(words, data);
            CSVHelper.WriteWords(outputFilePath, words);
            XMLHelper.WriteWords(outputFilePath + InExtension, words);
        }
    }
}
