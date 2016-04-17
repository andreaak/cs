namespace Patterns._03_Behavioral.Memento._002_Robot
{
    class Man
    {
        public string Ñlothes { get; set; }

        public void Dress(Backpack backpack)
        {
            Ñlothes = backpack.Ñontents;
        }

        public Backpack Undress()
        {
            return new Backpack(Ñlothes);
        }
    }
}
