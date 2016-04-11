using System;


namespace Creational.FactoryMethod._001_Game
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
