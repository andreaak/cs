using System.Collections.Generic;

namespace Patterns._02_Structural.Facade._003_AssemblerCompiler.SubSystems.Nodes
{
    // Токен хранит в себе набор лексем (частей составляющих инструкцию ассемблера).
    public class Token
    {
        public List<string> Lexemes { get; set;}

        public Token()
        {
        }

        public Token(string [] lexemes)
        {
            this.Lexemes = new List<string>();
            this.Lexemes.AddRange(lexemes);
        }
    }
}
