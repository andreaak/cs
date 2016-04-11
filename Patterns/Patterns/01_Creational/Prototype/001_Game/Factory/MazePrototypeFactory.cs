using System;


namespace Creational.Prototype._001_Game
{
    class MazePrototypeFactory : MazeFactory
    {
        // Поля.
        Maze prototypeMaze = null;
        Room prototypeRoom = null;
        Wall prototypeWall = null;
        Door prototypeDoor = null;

        // Конструктор.
        public MazePrototypeFactory(Maze maze, Wall wall, Room room, Door door)
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

        public override Room MakeRoom(int number)
        {
            Room room = prototypeRoom.Clone();
            room.Initialize(number);

            return room;
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
