using System;


namespace Creational.Prototype.Example1
{
    class Door : MapSite
    {
        // Поля.
        Room room1 = null;
        Room room2 = null;
        bool isOpen;

        // Конструкторы.

        public Door()
        {
        }

        public Door(Door other)
        {
            this.room1 = other.room1;
            this.room2 = other.room2;
        }

        public Door(Room room1, Room room2)
        {
            this.room1 = room1;
            this.room2 = room2;
        }

        // Методы.

        // Отображает дверь.
        public override void Enter()
        {
            Console.WriteLine("Door");
        }

        // Метод возвращает ссылку на другую комнату.
        public Room OtherSideFrom(Room room)
        {
            if (room == room1)  // Если идем из r1 в r2, то возвращаем ссылку на r2.
                return room2;
            else          // Иначе: Если идем из r2 в r1, то возвращаем ссылку на r1.
                return room1;
        }

        // Клонирование.
        public virtual Door Clone()
        {
            Door door = new Door(this.room1, this.room2);
            // При обращении к закрытым членам экземпляра, в пределах того-же класса - инкапсуляция не работает.
            door.isOpen = this.isOpen;

            return door;
        }

        // Метод инициализации...
        public virtual void Initialize(Room room1, Room room2)
        {
            this.room1 = room1;
            this.room2 = room2;
        }
    }

}
