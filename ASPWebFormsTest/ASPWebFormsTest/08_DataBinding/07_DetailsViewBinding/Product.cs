namespace ASPWebFormsTest._08_DataBinding._07_DetailsViewBinding
{
    /// <summary>
    /// Класс представляющий одну запись в источнике данных.
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

        // При переопределении метода Equals метод GetHashCode желательно переопределить.
        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}