using System;


namespace Creational.Prototype._001_Game
{
    // Класс волшебной комнаты.
    class EnchantedRoom : Room
    {
        // Поля.
        private Spell spell;

        // Конструкторы.
        public EnchantedRoom(int roomNo)
            : base(roomNo)
        {
        }
        public EnchantedRoom()
            :base()
        {
            spell = null;
        }

        public EnchantedRoom(int roomNo, Spell spell)
        {
            this.Initialize(roomNo, spell);
        }

        // Копирующий конструктор.
        protected EnchantedRoom(EnchantedRoom other)
            : base(other)
        {
            this.spell = other.spell;
        }

        // Методы.
        public bool HasSpell()
        {
            if (this.spell != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Переопределение базовой операции клонирования.
        public override Room Clone()
        {
            return new EnchantedRoom(this);
        }
        // Переопределение базовой операции инициализации.
        public void Initialize(int roomNo, Spell spell)
        {
            base.Initialize(roomNo);
            this.spell = spell;
        }
    }
}
