using System.Threading.Tasks;

namespace TextConverter
{
    public interface IWordConverter
    {
        string InExtension { get; }
        string OutExtension { get; }
        Task ConvertFileAsync(string filePath, string outputFilePath, WordData data);
    }
}
