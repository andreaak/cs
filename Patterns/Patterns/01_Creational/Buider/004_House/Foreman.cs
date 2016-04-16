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
            // ��������� ���� ������� ��������� � ���������� �������.
            // ������� ������� ������, ����� ���� � � ��������� ������� �����.
            builder.BuildBasement();
            builder.BuildStorey();
            builder.BuildRoof();
        }
    }
}
