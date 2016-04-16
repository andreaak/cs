
using Patterns._01_Creational.FactoryMethod._003_Application.Product;
using Patterns._01_Creational.FactoryMethod._003_Application.Product.ConcreteProduct;

namespace Patterns._01_Creational.FactoryMethod._003_Application.Creator.ConcreteCreator
{
    // ConcreteCreatorA
    class TextApplication : Application
    {
        BaseForm baseForm;
        int counter;
        Action action;

        public TextApplication(BaseForm baseForm, int counter, Action action)
        {
            this.baseForm = baseForm;
            this.counter = counter;
            this.action = action;
        }

        public override Document CreateDocument()
        {
            return new TextDocument(baseForm, counter, action);
        }

        public override void OpenDocument()
        {
            CreateDocument().Open();
        }
    }
}
