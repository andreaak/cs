using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._02_Structural.Facade._003_AssemblerCompiler
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            string sourceCode = "var1 dd 5;" +
                                "var2 dd 2;" +
                                "xor eax, eax;" +
                                "mov eax, var1;" +
                                "add eax, var2;" +
                                //"sub eax, var1;" +
                                //"mov eax, 1;" +
                                //"nop;" +

                                "call HexMessage;" +
                                "call ExitProcess;";

            Action<string> message = msg => Debug.WriteLine(msg);

            Compiler.Compiler compiler = new Compiler.Compiler();
            compiler.Compile(sourceCode, "result.exe", message);

            // Delay.
            //Console.ReadKey();
        }
    }
}
