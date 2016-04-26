namespace CS_TDD._004_StubsAndMocks._019_RhynoMocks._003_RhynoMocks
{
    public interface IWarehouse
    {
        bool HasInventory(string productName);
        void Remove(string productName);
    }
}
