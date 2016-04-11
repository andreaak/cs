using System;


namespace Patterns.Creational.AbstractFactory._001_Game
{
    class MazeGame
    {
        // Фабрика компонентов лабиринта.
        MazeFactory factory = null;

        // Метод CreateMaze - создает лабиринт из двух комнат с одной дверью между комнатами. 
        // В нем нет недостатков прошлой версии метода.
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
    }
}
