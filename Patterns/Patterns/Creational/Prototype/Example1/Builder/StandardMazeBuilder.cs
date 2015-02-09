using System;


namespace Creational.Prototype.Example1
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
                Room room = new Room(roomNo);
                currentMaze.AddRoom(room);

                room.SetSide(Direction.North, new Wall());
                room.SetSide(Direction.South, new Wall());
                room.SetSide(Direction.East, new Wall());
                room.SetSide(Direction.West, new Wall());
            }
        }

        // Чтобы построить дверь между двумя комнатами, требуется найти обе комнаты в лабиринте и их общую стену.
        public override void BuildDoor(int roomFrom, int roomTo)
        {
            Room room1 = currentMaze.RoomNo(roomFrom);
            Room room2 = currentMaze.RoomNo(roomTo);
            Door door = new Door(room1, room2);

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
        private Direction CommonWall(Room room1, Room room2)
        {
            if (room1.GetSide(Direction.North) is Wall &&
                room1.GetSide(Direction.South) is Wall &&
                room1.GetSide(Direction.East) is Wall &&
                room1.GetSide(Direction.West) is Wall &&
                room1.GetSide(Direction.North) is Wall &&
                room1.GetSide(Direction.South) is Wall &&
                room1.GetSide(Direction.East) is Wall &&
                room1.GetSide(Direction.West) is Wall)
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
