using System;


namespace Creational.Prototype.Example1
{
    // Класс взорванной стены.
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

        // Переопределение базовой операции клонирования.
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
