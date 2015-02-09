using System;


namespace Patterns.Creational.Singleton.Example1
{
    class MazePrototypeFactory : MazeFactory
    {
        // Поля.
        Maze prototypeMaze = null;
        Room prototypeRoom1 = null;
        Room prototypeRoom2 = null;
        Wall prototypeWall = null;
        Door prototypeDoor = null;

        // Конструктор.
        public MazePrototypeFactory(Maze maze, Wall wall, Room room1, Room room2, Door door)
        {
            this.prototypeMaze = maze;
            this.prototypeWall = wall;
            this.prototypeRoom1 = room1;
            this.prototypeRoom2 = room2;
            this.prototypeDoor = door;
        }

        // Методы.

        public override Maze MakeMaze()
        {
            return prototypeMaze.Clone();
        }

        public override Room MakeRoom(int number)
        {
            if (number == 1)
                return prototypeRoom1.Clone();
            else
                return prototypeRoom2.Clone();
        }

        public override Wall MakeWall()
        {
            // Клонирование.
            return prototypeWall.Clone();
        }

        public override Door MakeDoor(Room room1, Room room2)
        {
            Door door = prototypeDoor.Clone();
            door.Initialize(room1, room2);

            return door;
        }
    }
}
