using System.Diagnostics;

namespace Patterns._03_Behavioral.Visitor._003_Visitor
{
    class ConcreteVisitor : Visitor
    {
        public override void VisitElementA(ElementA elementA)
        {
            // Код который мог быть размещен в классе ElementA,
            // расширяет собой класс ElementA.
            elementA.SomeState = "State A";
            Debug.WriteLine(elementA.SomeState);

            // Работа с разнородным интерфейсом.
            elementA.OperationA();
        }

        public override void VisitElementB(ElementB elementB)
        {
            // Код который мог быть размещен в классе ElementB,
            // расширяет собой класс ElementB.
            elementB.SomeState = "State B";
            Debug.WriteLine(elementB.SomeState);

            // Работа с разнородным интерфейсом.
            elementB.OperationB();
        }
    }
}
