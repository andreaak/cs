using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WsdlGeneration
{
    internal class ProcessGeneratedFile
    {
        // Fields
        private string fileToProcess;
        private InfoMessages filterInfo;

        // Methods
        public ProcessGeneratedFile(string filePath)
        {
            this.fileToProcess = filePath;
            this.filterInfo = new InfoMessages();
        }

        public void RunProcessor()
        {
            if (!File.Exists(this.fileToProcess))
            {
                this.filterInfo.AddInfoMessage(InfoLevel.Error, "File not found.", "Can't process file " + this.fileToProcess);
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                using (TextReader reader = new StreamReader(this.fileToProcess))
                {
                    string str = null;
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.fileToProcess);
                    while ((str = reader.ReadLine()) != null)
                    {
                        str = str.Replace("System.Web.Services.Protocols.SoapHttpClientProtocol", "GcmWebReferences.Protocols.GcmWebServicesClientProtocol").Replace("public " + fileNameWithoutExtension + "()", "private " + fileNameWithoutExtension + "()");
                        builder.Append(str).Append("\n");
                    }
                }
                File.SetAttributes(this.fileToProcess, FileAttributes.Normal);
                File.Delete(this.fileToProcess);
                TextWriter writer = new StreamWriter(this.fileToProcess);
                writer.Write(builder.ToString());
                writer.Close();
                this.filterInfo.AddInfoMessage(InfoLevel.Info, string.Empty, "File " + this.fileToProcess + " processed without errors.");
            }
        }

        // Properties
        public InfoMessages ProcessInfo
        {
            get
            {
                return this.filterInfo;
            }
        }
    }
}
