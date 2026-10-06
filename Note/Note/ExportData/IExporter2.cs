using System.Collections.Generic;

namespace Note.ExportData
{
    public interface IExporter2
    {
        void Convert(string outputFile, IList<(string, string)> rtfs);
        string[] GetExtensions();
    }
}