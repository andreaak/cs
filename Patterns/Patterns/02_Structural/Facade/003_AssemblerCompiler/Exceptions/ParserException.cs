using System;

namespace Patterns._02_Structural.Facade._003_AssemblerCompiler.Exceptions
{
    class ParserException : Exception
    {
        public ParserException(string message)
            : base(message)
        {
        }
    }
}
