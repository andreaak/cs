using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._02_Structural.Facade._003_AssemblerCompiler2
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test2()
        {
            string program = "var1 db 1000; var2 db 500;" +
                             "xor eax,eax;" +
                             "mov eax, var1; mov eax, 1000; mov var1, eax;" +
                             "nop ;" +
                             "add eax, var1; add eax, 1000; add var1, eax;" +
                             "sub eax, var2; sub eax, 1000; sub var2, eax;" +
                             "call HexMessage; call ExitProcess";

            //string program = "var1 db 1000;xor eax,eax; mov eax, var1; call HexMessage; call ExitProcess";

            string errorMessage;

            Stream stream = Compiler.Compiler.Instance.Compile(program, out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                Debug.WriteLine(string.Format("Compilation failed.\r\nError: {0}", errorMessage));
                return;
            }

            SaveStreamToFile("result.exe", stream);
            Debug.WriteLine("Compilation succeeded.");
        }

        public static void SaveStreamToFile(string filePath, Stream stream)
        {
            if (stream == null || stream.Length == 0)
            {
                return;
            }

            using (FileStream fileStream = File.Create(filePath, (int)stream.Length))
            {
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }
    }
}
