using System;


namespace Creational.FactoryMethod._001_Game
{
    class EnchantedMazeGame : MazeGame
    {
        // Конструктор.
        public EnchantedMazeGame()
        {
        }

        // Методы.

        public override Room MakeRoom(int number)
        {
            return new EnchantedRoom(number, this.CastSpell());
        }

        public override Door MakeDoor(Room r1, Room r2)
        {
            return new DoorNeedingSpell(r1, r2);
        }

        // Метод создания заклинания.
        protected Spell CastSpell()
        {
            return new Spell();
        }
    }
}
