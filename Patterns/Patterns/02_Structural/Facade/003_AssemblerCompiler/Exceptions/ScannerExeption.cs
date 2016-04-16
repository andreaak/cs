using System;

namespace Patterns._02_Structural.Facade._003_AssemblerCompiler.Exceptions
{
    class ScannerExeption : Exception
    {
        public ScannerExeption(string message)
            : base(message)
        {
        }
    }
}
