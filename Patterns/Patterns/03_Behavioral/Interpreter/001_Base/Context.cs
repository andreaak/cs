namespace Patterns._03_Behavioral.Interpreter._001_Base
{
    class Context
    {
        // Контекст распознающей грамматики языка:
        // V = {a}; L = V*; 
        // Правильные цепочки: a, aa, aaa, ...
        // Неправильные цепочки: b, ab, aba, ...

        public string Source { get; set; }
        public char Vocabulary { get; set; }
        public bool Result { get; set; }
        public int Position { get; set; }
    }
}
