using System.Drawing;
using Patterns._01_Creational.FactoryMethod._005_FigureManipulator.Manipulators;

namespace Patterns._01_Creational.FactoryMethod._005_FigureManipulator.Figures
{
    class TextFigure : Figure
    {
        // Положение фигуры на форме.
        Point startPoint;

        public TextFigure(Point startPoint)
        {
            this.startPoint = startPoint;
            manipulator = CreateManipulator();
        }

        // Фабричный метод возвращающий манипулятор соответствующий данной фигуре.
        public override Manipulator CreateManipulator()
        {
            return new TextManipulator(startPoint);
        }
    }
}
