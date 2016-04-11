using System;


namespace Creational.Prototype._001_Game
{
    // Класс двери, для которой требуется заклинание.
    class DoorNeedingSpell : Door
    {
        // Поля.
        bool isOpen;

        // Конструкторы.       
        public DoorNeedingSpell(Room room1, Room room2)
            : base(room1, room2)
        {
        }
        public DoorNeedingSpell()
            : base()
        {
            isOpen = default(bool);
        }

        // Копирующий конструктор.
        protected DoorNeedingSpell(DoorNeedingSpell other)
            : base(other)
        {
            this.isOpen = other.isOpen;
        }

        // Методы.
        public bool IsOpen()
        {
            return this.isOpen;
        }

        // Переопределение базовой операции клонирования.
        public override Door Clone()
        {
            return new DoorNeedingSpell(this);
        }
        // Переопределение базовой операции инициализации.
        public override void Initialize(Room room1, Room room2)
        {
            base.Initialize(room1, room2);
            this.isOpen = default(bool);
        }
    }
}
