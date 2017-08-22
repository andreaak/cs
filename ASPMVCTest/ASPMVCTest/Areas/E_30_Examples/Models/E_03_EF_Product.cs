using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_ASPMVCTest.Areas.E_30_Examples.Models
{
    [Table("Product")]
    public class E_03_EF_Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
