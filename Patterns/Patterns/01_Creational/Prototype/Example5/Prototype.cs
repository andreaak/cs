using System;

// Прототипно-ориентированный подход в программировании.
// ПОП является подмножеством ООП.

namespace Creational.Prototype.Example5
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
