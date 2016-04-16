
using Patterns._01_Creational.FactoryMethod._003_Application.Product;
using Patterns._01_Creational.FactoryMethod._003_Application.Product.ConcreteProduct;

namespace Patterns._01_Creational.FactoryMethod._003_Application.Creator.ConcreteCreator
{
    class PngApplication : Application
    {
        BaseForm baseForm;
        int counter;
        Action action;

        public PngApplication(BaseForm baseForm, int counter, Action action)
        {
            this.baseForm = baseForm;
            this.counter = counter;
            this.action = action;
        }

        public override Document CreateDocument()
        {
            return new PngDocument(baseForm, counter, action);
        }

        public override void OpenDocument()
        {
            CreateDocument().Open();
        }
    }
}
