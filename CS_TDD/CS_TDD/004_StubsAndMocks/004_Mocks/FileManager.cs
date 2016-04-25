using System.IO;

namespace CS_TDD._004_StubsAndMocks._004_Mocks
{

    class FileManager
    {
        ILogService service;

        public FileManager(ILogService service)
        {
            this.service = service;
        }

        public void Analize(string fileName)
        {
            if (fileName.Length < 8)
                service.LogError("Filename too short:" + fileName);

            if (Path.GetExtension(fileName) != ".txt")
                service.LogError("FileExtension error: " + fileName);
        }
    }
}
