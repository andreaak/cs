using System;
using System.IO;
using CS_TDD._004_StubsAndMocks._004_Mocks;

namespace CS_TDD._004_StubsAndMocks._005_MockAndStub
{

    class FileManager
    {
        ILogService logService;
        IMailService mailService;

        public FileManager(ILogService logService, IMailService mailService)
        {
            this.logService = logService;
            this.mailService = mailService;
        }

        public void Analize(string fileName)
        {
            try
            {
                if (fileName.Length < 8)
                    logService.LogError("Filename too short: " + fileName);

                if (Path.GetExtension(fileName) != ".txt")
                    logService.LogError("FileExtension error: " + fileName);
            }
            catch (Exception ex)
            {
                mailService.SendMail("some@mail.mail", ex.Message);
            }
           
        }
    }
}
