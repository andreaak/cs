﻿namespace Patterns._01_Creational.Singleton._001_Game.Wall
{
    class BombedWall : Wall
    {
        // Поля.

        bool bomb;

        // Конструкторы.

        public BombedWall()
        {
        }

        public BombedWall(BombedWall other)
        {
            this.bomb = other.bomb;
        }

        // Замещение базовой операции клонирования.
        public override Wall Clone()
        {
            return new BombedWall(this);
        }

        public bool HasBomb()
        {
            return this.bomb;
        }
    }
}
