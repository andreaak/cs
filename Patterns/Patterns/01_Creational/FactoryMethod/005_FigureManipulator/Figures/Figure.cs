
using Patterns._01_Creational.FactoryMethod._005_FigureManipulator.Manipulators;

namespace Patterns._01_Creational.FactoryMethod._005_FigureManipulator.Figures
{
    // Абстрактный класс Figure предоставляет фабричный метод CreateManipulator(), 
    // позволяющий создать манипулятор соответствующий определенной фигуре. 
    public abstract class Figure
    {
        public Manipulator manipulator { get; set; }
        public abstract Manipulator CreateManipulator();
    }
}
