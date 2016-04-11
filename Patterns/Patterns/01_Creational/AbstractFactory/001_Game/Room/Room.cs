using System.Collections.Generic;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory._001_Game
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

        public MapSite GetSide(Direction direction)
        {
            return sides[direction];
        }

        public void SetSide(Direction direction, MapSite mapSide)
        {
            this.sides.Add(direction, mapSide);
        }

        // Номер комнаты. 
        public int RoomNumber
        {
            get { return roomNumber; }
            set { roomNumber = value; }
        }
    }
}
