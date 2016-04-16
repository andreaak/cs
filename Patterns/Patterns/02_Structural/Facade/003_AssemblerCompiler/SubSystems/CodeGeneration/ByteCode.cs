
namespace Patterns._02_Structural.Facade._003_AssemblerCompiler.SubSystems.CodeGeneration
{
    class ByteCode
    {
        byte[] code = null;

        public byte[] Code
        {
            get { return code; }
        }

        public ByteCode(params byte[] code)
        {
            this.code = code;
        }
    }
}
