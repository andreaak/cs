namespace Patterns._01_Creational.Prototype._001_Game.Factory
{
    class MazePrototypeFactory : MazeFactory
    {
        // Поля.
        Maze prototypeMaze = null;
        Room.Room prototypeRoom = null;
        Wall.Wall prototypeWall = null;
        Door.Door prototypeDoor = null;

        // Конструктор.
        public MazePrototypeFactory(Maze maze, Wall.Wall wall, Room.Room room, Door.Door door)
        {
            this.prototypeMaze = maze;
            this.prototypeWall = wall;
            this.prototypeRoom = room;
            this.prototypeDoor = door;
        }

        // Методы.

        public override Maze MakeMaze()
        {
            return prototypeMaze.Clone();
        }

        public override Room.Room MakeRoom(int number)
        {
            Room.Room room = prototypeRoom.Clone();
            room.Initialize(number);

            return room;
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
