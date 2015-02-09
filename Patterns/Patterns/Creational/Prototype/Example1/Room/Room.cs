using System;
using System.Collections.Generic;


namespace Creational.Prototype.Example1
{
    class Room : MapSite
    {
        // Поля.
        protected int roomNumber;           
              
        Dictionary<Direction, MapSite> sides = new Dictionary<Direction, MapSite>(4);  // Стороны комнаты. (Wall and Door)

        // Конструкторы
        public Room()
        {
            roomNumber = 0; // Номер комнаты.
        }

        public Room(int roomNo)
        {
            this.Initialize(roomNo);
        }

        // Копирующий конструктор.
        protected Room(Room other)
        {
            this.roomNumber = other.roomNumber;
        }

        // Методы.
        public override void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Room");
        }

        // Получение экземпляра стороны комнаты.
        public MapSite GetSide(Direction direction)
        {
            return sides[direction];
        }

        // Добавление стороны.
        public void SetSide(Direction direction, MapSite mapSide)
        {
            // Если Дверь, то вставить ее на место стены. (логика билдера - значит стены уже построены)
            if (mapSide is Door)
            { 
                this.sides[direction] = mapSide; 
            }
            else
            {
                // Если Стена то просто добавлять, значит идет построение комнаты, 
                // а комната изначально строится только из стен только потом идет замена стены на дверь.
                this.sides.Add(direction, mapSide);
            }                
        }

        // Номер комнаты. 
        public int RoomNumber
        {
            get { return roomNumber; }
            set { roomNumber = value; }
        }

        // Клонироавние.
        public virtual Room Clone()
        {
            return new Room(this);
        }
        // Метод инициализации.
        public virtual void Initialize(int roomNo)
        {
            this.roomNumber = roomNo;
        }
    }
}
