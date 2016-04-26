namespace CS_TDD._004_StubsAndMocks._019_RhynoMocks._003_RhynoMocks
{
    public class Warehouse : IWarehouse
    {
        private DataAccess db;

        public Warehouse()
        {
            db = new DataAccess();
        }

        public virtual bool HasInventory(string productName)
        {
            return db.HasInventory(productName);
        }

        public virtual void Remove(string productName)
        {
            db.Remove(productName);
        }
    }
}
