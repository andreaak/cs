using System.IO;

namespace Patterns._02_Structural.Facade._003_AssemblerCompiler.SubSystems.CodeGeneration
{
    class ByteCodeStream
    {
        public void SaveStreamToFile(ByteCode programByteCode, string exeFileLocation)
        {
            using (FileStream fileStream = File.Create(exeFileLocation, programByteCode.Code.Length))
            {
                fileStream.Write(programByteCode.Code, 0, programByteCode.Code.Length);
            }
        }
    }
}
