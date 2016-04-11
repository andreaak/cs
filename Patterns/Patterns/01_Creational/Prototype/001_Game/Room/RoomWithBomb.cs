using System;


namespace Creational.Prototype._001_Game
{
    // Класс комнаты с бомбой.
    class RoomWithBomb : Room
    {
        // Конструкторы.
        public RoomWithBomb()
        {
        }

        public RoomWithBomb(int roomNo)
            : base(roomNo)
        {
        }

        // Копирующий конструктор.
        protected RoomWithBomb(RoomWithBomb other)
            : base(other)
        {
        }

        // Методы.
        // Переопределение базовой операции клонирования.
        public override Room Clone()
        {
            return new RoomWithBomb(this);
        }
    }
}
