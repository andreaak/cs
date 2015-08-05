using System;


namespace Patterns.Creational.Singleton.Example1
{
    class BombedMazeGame : MazeGame
    {
        // Конструктор.
        public BombedMazeGame()
        {
        }

        // Методы.

        public override Wall MakeWall()
        {
            return new BombedWall();
        }

        public override Room MakeRoom(int number)
        {
            return new RoomWithBomb(number);
        }
    }
}
