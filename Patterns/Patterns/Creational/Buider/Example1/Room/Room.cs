using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Creational.Builder.Example1
{
    class Room : MapSite
    {
        // Поля.
        int roomNumber = 0;   // Номер комнаты.        
              
        Dictionary<Direction, MapSite> sides = new Dictionary<Direction, MapSite>(4);  // Стороны комнаты. (Wall and Door)

        // Конструктор.       
        public Room(int roomNo)
        {
            this.roomNumber = roomNo;
        }
        
        // Методы.

        public override void Enter()
        {
            Debug.WriteLine("Room");
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
    }
}
