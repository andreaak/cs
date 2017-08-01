namespace ASPWebFormsTest._08_DataBinding._07_DetailsViewBinding
{
    /// <summary>
    /// ����� �������������� ���� ������ � ��������� ������.
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Product()
        {
        }

        public override bool Equals(object obj)
        {
            Product temp = obj as Product;
            if (temp == null)
            {
                return false;
            }
            return temp.Id == this.Id;
        }

        // ��� ��������������� ������ Equals ����� GetHashCode ���������� ��������������.
        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}