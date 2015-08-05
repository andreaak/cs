using System;


namespace Creational.Prototype.Example1
{
    class MazeGame
    {
        // Фабрика компонентов лабиринта.
        MazeFactory factory = null;

        // Методы CreateMaze - создает лабиринт из двух комнат с одной дверью между комнатами.    

        // Использует Фабрику. 
        public Maze CreateMaze(MazeFactory factory)
        {
            this.factory = factory;

            // Лабиринт.
            Maze aMaze = this.factory.MakeMaze();

            Room r1 = this.factory.MakeRoom(1);
            Room r2 = this.factory.MakeRoom(2);

            // Дверь - данный экземпляр содержит ссылки на две комнаты. (Является общим для двух комнат.)
            Door aDoor = this.factory.MakeDoor(r1, r2);

            aMaze.AddRoom(r1);
            aMaze.AddRoom(r2);

            r1.SetSide(Direction.North, this.factory.MakeWall());
            r1.SetSide(Direction.East, aDoor);
            r1.SetSide(Direction.South, this.factory.MakeWall());
            r1.SetSide(Direction.West, this.factory.MakeWall());

            r2.SetSide(Direction.North, this.factory.MakeWall());
            r2.SetSide(Direction.East, this.factory.MakeWall());
            r2.SetSide(Direction.South, this.factory.MakeWall());
            r2.SetSide(Direction.West, aDoor);

            return aMaze;
        }

        // Использует Строителя.   
        public Maze CreateMaze(MazeBuilder builder)
        {
            builder.BuildMaze();
            builder.BuildRoom(1);
            builder.BuildRoom(2);
            builder.BuildDoor(1, 2);

            // Возвращает готовый продукт (Лабиринт)
            return builder.GetMaze();
        }

        public Maze CreateComplexMaze(MazeBuilder builder)
        {
            // Построение 1001-й комнаты.
            for (int i = 0; i < 1001; i++)
            {
                builder.BuildRoom(i + 1);
            }

            return builder.GetMaze();
        }
    }
}
