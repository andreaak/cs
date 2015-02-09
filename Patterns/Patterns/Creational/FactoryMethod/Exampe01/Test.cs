using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Creational.FactoryMethod.Example1
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            // Создаем генератор лабиринта.
            MazeGame game = new MazeGame();

            // Создаем лабиринт из двух комнат используя фабричный метод - CreateMaze().
            Maze maze = game.CreateMaze();

            // Генератор псевдослучайных последовательностей.
            Random random = new Random();

            // Попадаем в лабиринт, выбирая комнату случайным образом.
            Room room = maze.RoomNo(random.Next(1, 3));

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
                Console.Write("Я в комнате {0}. Делаю шаг. - ", room.RoomNumber);

                // Делаем шаг в выбранную сторону. (Визуальное отображение стороны на экране)
                site.Enter();

                if (site is Door) // Если дверь, то перейти в другую комнату.
                {
                    Door door = (Door)site;
                    // Переход в другую комнату (Получение ссылки на новую комнату).
                    room = door.OtherSideFrom(room);
                }

                // Иначе стена. Тогда опять ищем другое направление в той-же комнате.

                // Задержка между шагами.
                Thread.Sleep(800);
            }
        }

    }
}
