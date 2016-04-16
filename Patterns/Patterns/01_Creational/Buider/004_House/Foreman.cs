namespace Patterns._01_Creational.Buider._004_House
{
    class Foreman
    {
        Builder.Builder builder;

        public Foreman(Builder.Builder builder)
        {
            this.builder = builder;
        }

        public void Construct()
        {
            // Требуется дать команды строителю в правильном порядке.
            // Сначала строить подвал, потом этаж и в последнюю очередь крышу.
            builder.BuildBasement();
            builder.BuildStorey();
            builder.BuildRoof();
        }
    }
}
