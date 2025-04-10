namespace CSTest._04_Class._05_Properties
{
    class _01_Problem
    {
        private string field = null;

        public void SetField(string value) // Метод-мутатор - mutator  (setter)
        {
            field = value;
        }

        public string GetField()           // Метод-аксессор -  accessor  (getter)
        {
            return field;
        }
    }
}
