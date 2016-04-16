using System;

namespace Patterns._02_Structural.Facade._003_AssemblerCompiler.Exceptions
{
    class ProgramNodeBuilderExeption : Exception
    {
        public ProgramNodeBuilderExeption(string message)
            : base(message)
        {
        }
    }
}
