using System;
using System.IO;
using CS_TDD._004_StubsAndMocks._004_Mocks;

namespace CS_TDD._004_StubsAndMocks._013_RhynoMocksPropertyConstraints
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
                if (Path.GetExtension(fileName) != ".txt")
                    logService.LogError("FileExtension error: " + fileName);
            }
            catch (Exception ex)
            {
                mailService.SendMail(new MailMessage("TechSupport@mail.com", ex.GetType().Name, ex.Message));
            }
           
        }
    }
}
