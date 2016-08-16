using System;
using System.Diagnostics;
using NUnit.Framework;
using Patterns._01_Creational.Singleton._001_Game.Enum;
using Patterns._01_Creational.Singleton._001_Game.Factory;

namespace Patterns._01_Creational.Singleton._001_Game
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            // Создаем генератор лабиринта.
            MazeGame.MazeGame game = new MazeGame.MazeGame();

            // MazeFactory - SINGLETON.
            MazeFactory mazeFactory = MazeFactory.Instance();

            // Создаем лабиринт из двух комнат используя фабричный метод - CreateMaze().
            Maze maze = game.CreateMaze(mazeFactory);

            // Генератор псевдослучайных последовательностей.
            Random random = new Random();

            // Попадаем в лабиринт, выбирая комнату случайным образом.
            Room.Room room = maze.RoomNo(random.Next(1, 3));

            // Выбранная сторона.
            MapSite site = null;


            // ИГРОВАЯ ПЕТЛЯ.

            // Начинаем идти по лабиринту. 
            while (true)
            {

                // Случайным образом выбираем новую сторону.
                switch (random.Next(0, 5))
                {
                    // Выбираем направление куда будем идти (получаем ссылку на сторону). 
                    case 0:
                        site = room.GetSide(Direction.North);
                        break;
                    case 1:
                        site = room.GetSide(Direction.South);
                        break;
                    case 2:
                        site = room.GetSide(Direction.East);
                        break;
                    case 3:
                        site = room.GetSide(Direction.West);
                        break;
                }

                // Отображаем номер комнаты в которой сейчас находимся.
                Debug.WriteLine("Я в комнате {0}. Делаю шаг. - ", room.RoomNumber);

                // Делаем шаг в выбранную сторону. (Визуальное отображение стороны на экране)
                site.Enter();

                if (site is Door.Door) // Если дверь, то перейти в другую комнату.
                {
                    Door.Door door = (Door.Door)site;
                    // Переход в другую комнату (Получение ссылки на новую комнату).
                    room = door.OtherSideFrom(room);
                }

                // Иначе стена. Тогда опять ищем другое направление в той-же комнате.

                // Задержка между шагами.
                //Thread.Sleep(800);
                break;
            }
            
        }
    }
}
