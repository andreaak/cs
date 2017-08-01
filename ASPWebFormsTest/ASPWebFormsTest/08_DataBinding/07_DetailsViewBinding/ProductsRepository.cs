using System.Collections.Generic;

namespace ASPWebFormsTest._08_DataBinding._07_DetailsViewBinding
{
    /// <summary>
    /// Класс представляющий источник данных для элементов управления на страницах
    /// </summary>
    public class ProductsRepository
    {
        private static List<Product> _products;

        public static List<Product> Products
        {
            get 
            {
                if (_products == null) 
                {
                    _products = new List<Product>();
                    _products.Add(new Product() { Id = 1, Name = "Mobile phone", Price = 220 });
                    _products.Add(new Product() { Id = 2, Name = "Laptop", Price = 999 });
                }
                return _products; 
            }
            set { _products = value; }
        }

        // Получение всех элементов из источника данных
        public List<Product> SelectAllProducts()
        {
            return Products;
        }

        // Добавление нового элемента в источник данных.
        public void AddProduct(Product p)
        {
            Products.Add(p);
        }

        // Удаление элемента из источника данных.
        public void RemoveProduct(Product p)
        {
            Products.Remove(p);
        }

        // Обновление элемента в источнике данных.
        public void UpdateProduct(Product p)
        {
            int index = Products.IndexOf(p);
            Products.Remove(p);
            Products.Insert(index, p);
        }
    }
}