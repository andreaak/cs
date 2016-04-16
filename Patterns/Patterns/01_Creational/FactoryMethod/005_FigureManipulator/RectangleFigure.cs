using System.Drawing;
using Patterns._01_Creational.FactoryMethod._005_FigureManipulator.Figures;
using Patterns._01_Creational.FactoryMethod._005_FigureManipulator.Manipulators;

namespace Patterns._01_Creational.FactoryMethod._005_FigureManipulator
{
    class RectangleFigure : Figure
    {
        private Point endPoint;
        private Point startPoint;

        public RectangleFigure(Point startPoint, Point endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            manipulator = CreateManipulator();
        }

        public override Manipulator CreateManipulator()
        {
            return new RectangleManipulator(startPoint, endPoint);
        }
    }
}
