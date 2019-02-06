using System;

namespace TextConverter
{
    public class WordData
    {
        public string Path { get; set; }
        public string Region { get; set; }
        public bool DownloadTranscription { get; set; }
        public bool DownloadTranslation { get; set; }
        public bool Separate { get; set; }
        public Action<string> InfoAction { get; set; }
        public Action<string> ErrorAction { get; set; }
        public IWordConverter Converter { get; set; }
    }
}
