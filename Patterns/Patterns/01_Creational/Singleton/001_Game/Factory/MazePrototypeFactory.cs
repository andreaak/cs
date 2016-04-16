namespace Patterns._01_Creational.Singleton._001_Game.Factory
{
    class MazePrototypeFactory : MazeFactory
    {
        // Поля.
        Maze prototypeMaze = null;
        Room.Room prototypeRoom1 = null;
        Room.Room prototypeRoom2 = null;
        Wall.Wall prototypeWall = null;
        Door.Door prototypeDoor = null;

        // Конструктор.
        public MazePrototypeFactory(Maze maze, Wall.Wall wall, Room.Room room1, Room.Room room2, Door.Door door)
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

        public override Room.Room MakeRoom(int number)
        {
            if (number == 1)
                return prototypeRoom1.Clone();
            else
                return prototypeRoom2.Clone();
        }

        public override Wall.Wall MakeWall()
        {
            // Клонирование.
            return prototypeWall.Clone();
        }

        public override Door.Door MakeDoor(Room.Room room1, Room.Room room2)
        {
            Door.Door door = prototypeDoor.Clone();
            door.Initialize(room1, room2);

            return door;
        }
    }
}
