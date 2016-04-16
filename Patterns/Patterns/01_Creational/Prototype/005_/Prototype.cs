// Прототипно-ориентированный подход в программировании.
// ПОП является подмножеством ООП.

namespace Patterns._01_Creational.Prototype._005_
{
    class Prototype
    {
        public string Class { get; set; }
        public string State { get; set; }

        public Prototype Clone()
        {
            return this.MemberwiseClone() as Prototype;
        }
    }
}
