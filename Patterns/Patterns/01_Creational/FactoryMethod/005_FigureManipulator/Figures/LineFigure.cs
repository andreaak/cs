using System.Drawing;
using Patterns._01_Creational.FactoryMethod._005_FigureManipulator.Manipulators;

namespace Patterns._01_Creational.FactoryMethod._005_FigureManipulator.Figures
{
    class LineFigure : Figure
    {
        // Положение фигуры на форме.
        Point endPoint;
        Point startPoint;

        public LineFigure(Point startPoint, Point endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            manipulator = CreateManipulator();
        }

        // Фабричный метод возвращающий манипулятор соответствующий данной фигуре.
        public override Manipulator CreateManipulator()
        {
            return new LineManipulator(startPoint, endPoint);
        }
    }
}
