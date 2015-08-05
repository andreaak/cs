using System;


namespace Creational.Prototype.Example1
{
    // CountingMazeBuilder - вообще не создает никакого лабиринта, 
    // он лишь подсчитывает число компонентов разного вида, которые могли бы быть созданы.
    class CountingMazeBuilder : MazeBuilder
    {
        // Поля.
        // Счетчики дверей и комнат.
        int doors, rooms;
                
        // Конструктор.
        public CountingMazeBuilder()
        {
            this.doors = this.rooms = 0;
        }

        // Методы.

        public override void BuildMaze()
        {
        }

        public override void BuildRoom(int roomNo)
        {
            this.rooms++;
        }

        public override void BuildDoor(int roomFrom, int roomTo)
        {
            this.doors++;
        }

        public void AddWall(int i, Direction direction)
        {
        }

        // Свойства.  -  GetCounts

        // Счетчик комнат.
        public int CountRooms
        {
            get { return rooms; }
        }

        // Счетчик дверей.
        public int CountDoors
        {
            get { return doors; }
        }
    }
}
