using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace WsdlGeneration
{
    internal class WsdlExeRunner
    {
        // Fields
        private bool forceGenerate;
        private bool generateFields = false;
        private string inputFilePath = string.Empty;
        private string language = "CS";
        private InfoMessages messages = new InfoMessages();
        private string namespaceName = string.Empty;
        private string outputFilePath = string.Empty;
        private bool shareTypes = false;
        private StringBuilder standardError = new StringBuilder();
        private StringBuilder standardOutput = new StringBuilder();
        private string toolStandardError = string.Empty;
        private string wsdlExePath = string.Empty;

        // Methods
        private string CreateParameters()
        {
            return ("\"" + this.GetUriPath(this.InputFilePath) + "\" /out:\"" + this.OutputFilePath + "\" /n:\"" + this.Namespace + "\" /nologo");
        }

        private string GetUriPath(string inputPath)
        {
            if (File.Exists(inputPath))
            {
                return new Uri("file://" + inputPath).ToString();
            }
            return inputPath;
        }

        public bool Run()
        {
            try
            {
                if (!(this.ForceGenerate || (File.GetLastWriteTime(this.InputFilePath) >= File.GetLastWriteTime(this.OutputFilePath))))
                {
                    this.messages.AddInfoMessage(InfoLevel.Canceled, string.Empty, "File \n" + this.InputFilePath + "\nis older than \n" + this.OutputFilePath + "\nReference generation canceled.");
                    return false;
                }
                if (this.wsdlExePath.Length == 0)
                {
                    this.wsdlExePath = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\8.0", "InstallDir", string.Empty).ToString();
                    this.wsdlExePath = Path.Combine(this.wsdlExePath, @"..\..\SDK\v2.0\Bin\wsdl.exe");
                }
                if (File.Exists(this.OutputFilePath))
                {
                    File.SetAttributes(this.OutputFilePath, FileAttributes.Normal);
                }
                Process process = new Process();
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.ErrorDialog = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.ErrorDataReceived += new DataReceivedEventHandler(this.wsdlGenerator_ErrorDataReceived);
                process.OutputDataReceived += new DataReceivedEventHandler(this.wsdlGenerator_OutputDataReceived);
                if (!File.Exists(this.wsdlExePath))
                {
                    this.messages.AddInfoMessage(InfoLevel.Error, "File not found", "Can't find wsdl.exe generator. Expected file path is: " + this.wsdlExePath);
                    return false;
                }
                process.StartInfo.FileName = this.wsdlExePath;
                process.StartInfo.Arguments = this.CreateParameters();
                this.standardError.Remove(0, this.standardError.Length);
                this.standardOutput.Remove(0, this.standardOutput.Length);
                process.Start();
                process.BeginErrorReadLine();
                process.BeginOutputReadLine();
                process.WaitForExit();
                process.ErrorDataReceived -= new DataReceivedEventHandler(this.wsdlGenerator_ErrorDataReceived);
                process.OutputDataReceived -= new DataReceivedEventHandler(this.wsdlGenerator_OutputDataReceived);
                if (this.standardError.Length > 0)
                {
                    this.messages.AddInfoMessage(InfoLevel.Error, "Error", this.InputFilePath + Environment.NewLine + this.standardError.ToString());
                }
                if (this.standardOutput.Length > 0)
                {
                    this.messages.AddInfoMessage(InfoLevel.Info, "Info", this.standardOutput.ToString());
                }
                process.Close();
            }
            catch (System.Exception ex)
            {

            }
 
            return (this.standardError.Length == 0);
        }

        private void wsdlGenerator_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            this.standardError.Append(e.Data);
        }

        private void wsdlGenerator_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            this.standardOutput.Append(e.Data);
        }

        // Properties
        public bool ForceGenerate
        {
            get
            {
                return this.forceGenerate;
            }
            set
            {
                this.forceGenerate = value;
            }
        }

        public bool GenerateFields
        {
            get
            {
                return this.generateFields;
            }
            set
            {
                this.generateFields = value;
            }
        }

        public string InputFilePath
        {
            get
            {
                return this.inputFilePath;
            }
            set
            {
                this.inputFilePath = value;
            }
        }

        public string Language
        {
            get
            {
                return this.language;
            }
            set
            {
                string str = value;
                if ((str == null) || ((!(str == "CS") && !(str == "VB")) && ((!(str == "JS") && !(str == "VJS")) && !(str == "CPP"))))
                {
                    throw new ArgumentException("Wrong language to set!");
                }
                this.language = value;
            }
        }

        public InfoMessages Messages
        {
            get
            {
                return this.messages;
            }
        }

        public string Namespace
        {
            get
            {
                return this.namespaceName;
            }
            set
            {
                if (value == null)
                {
                    this.namespaceName = string.Empty;
                }
                else
                {
                    this.namespaceName = value;
                }
            }
        }

        public string OutputFilePath
        {
            get
            {
                return this.outputFilePath;
            }
            set
            {
                this.outputFilePath = value;
            }
        }

        public bool ShareTypes
        {
            get
            {
                return this.shareTypes;
            }
            set
            {
                this.shareTypes = value;
            }
        }
    }

}
