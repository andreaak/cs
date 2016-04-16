namespace Patterns._02_Structural.Facade._003_AssemblerCompiler.SubSystems.CodeGeneration
{
    internal enum Command
    {
        MOV,
        ADD,
        SUB,
        XOR,
        CALL,
        NOP
    }

    internal static class Constants
    {
        public static readonly char[] programDelimiter = new char[] { ';' };
        public static readonly char[] lexemesDelimiter = new char[] { ' ' };
        public static readonly char[] parameterDelimiter = new char[] { ',' };
        public static readonly int dataSegmentStartPoint = 0x00403000;
        public static readonly int programSegmentStartPoint = 0x00401000;
        public static readonly string eax = "EAX";
    }
}
