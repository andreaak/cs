namespace CS_TDD._005_xUnit._02_Asserting.Setup
{
    public class EnemyFactory
    {
        public object Create(bool isBoss)
        {
            if (isBoss)
            {
                return new BossEnemy();
            }

            return new NormalEnemy();
        }
    }
}
