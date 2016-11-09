using System.Diagnostics;

namespace CSTest._04_Class._01_Fields
{
    // Константа (Constant) - это область памяти, которая хранит в себе некоторое значение, которое нельзя изменить.
    class _02_Constants
    {
        public const int field = 5;

        //public const int field2;//Constant initializer is missing
        //static _02_Constants()
        //{
        //    field2 = 7;
        //}

        //Constant initializer must be compile-time constant 
        //_01_Constants.z' must be constant
        //Ошибка уровня компиляции
        //public const int z = Init();
        //public const TestClass w = new TestClass();

        private static int Init()
        {
            return field;
        }

        public void Show()
        {
            const int y = 9;
            // Попытка присвоения константе нового значения, приводит к ошибке уровня компиляции!
            //y = 25555;
            Debug.WriteLine(y);
        }

        //Decompiled
        /*
        private static int Init()
        {
            return 5;
        }

        public void Show()
        {
            Debug.WriteLine((object) 9);
        }
        */
    }
}
