using System;


namespace Creational.Prototype._001_Game
{
    // Фабрика для создания комнат с бомбой.
    class BombedMazeFactory : MazeFactory
    {
        // Метод создает взорваные стены.
        public override Wall MakeWall()
        {
            return new BombedWall();
        }

        // Метод создает комнату с бомбой.
        public override Room MakeRoom(int number)
        {
            return new RoomWithBomb(number);
        }
    }
}
