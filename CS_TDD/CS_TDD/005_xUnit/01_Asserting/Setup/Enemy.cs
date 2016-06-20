namespace CS_TDD._005_xUnit._01_Asserting.Setup
{
    public abstract class Enemy
    {
        private string _name = "Default Name";

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}