using Patterns._01_Creational.FactoryMethod._001_Game.Enum;

namespace Patterns._01_Creational.FactoryMethod._001_Game.Builder
{
    // Подкласс StandardMazeBuilder - содержит реализацию построения простых лабиринтов.
    class StandardMazeBuilder : MazeBuilder
    {
        Maze currentMaze = null;

        // Конструктор.
        public StandardMazeBuilder()
        {
            this.currentMaze = null;
        }

        // Инстанцирует экземпляр класса Maze, который будет собираться другими операциями.
        public override void BuildMaze()
        {
            this.currentMaze = new Maze();
        }

        // Создает комнату и строит вокруг нее стены.
        public override void BuildRoom(int roomNo)
        {
            //if (currentMaze.RoomNo(roomNo) == null)
            {
                Room.Room room = new Room.Room(roomNo);
                currentMaze.AddRoom(room);

                room.SetSide(Direction.North, new Wall.Wall());
                room.SetSide(Direction.South, new Wall.Wall());
                room.SetSide(Direction.East, new Wall.Wall());
                room.SetSide(Direction.West, new Wall.Wall());
            }
        }

        // Чтобы построить дверь между двумя комнатами, требуется найти обе комнаты в лабиринте и их общую стену.
        public override void BuildDoor(int roomFrom, int roomTo)
        {
            Room.Room room1 = currentMaze.RoomNo(roomFrom);
            Room.Room room2 = currentMaze.RoomNo(roomTo);
            Door.Door door = new Door.Door(room1, room2);

            room1.SetSide(CommonWall(room1, room2), door);
            room2.SetSide(CommonWall(room2, room1), door);
        }

        // Возвращает клиенту собранный продукт т.е., лабиринт.
        public override Maze GetMaze()
        {
            return this.currentMaze;
        }

        // CommonWall - Общая стена.
        // Это вспомогательная операция, которая определяет направление общей для двух комнат стены. 
        private Direction CommonWall(Room.Room room1, Room.Room room2)
        {
            if (room1.GetSide(Direction.North) is Wall.Wall &&
                room1.GetSide(Direction.South) is Wall.Wall &&
                room1.GetSide(Direction.East) is Wall.Wall &&
                room1.GetSide(Direction.West) is Wall.Wall &&
                room1.GetSide(Direction.North) is Wall.Wall &&
                room1.GetSide(Direction.South) is Wall.Wall &&
                room1.GetSide(Direction.East) is Wall.Wall &&
                room1.GetSide(Direction.West) is Wall.Wall)
            {
                return Direction.East;
            }
            else
            {
                return Direction.West;
            }
        }
    }
}
