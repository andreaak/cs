using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_ASPMVCTest.Areas.E_30_Examples.Models
{
    [Table("Order")]
    public class E_03_EF_Order
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public E_03_EF_Product Product { get; set; }
        public E_03_EF_Customer Customer { get; set; }
    }
}