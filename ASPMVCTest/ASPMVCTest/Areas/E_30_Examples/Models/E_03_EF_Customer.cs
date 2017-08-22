using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_ASPMVCTest.Areas.E_30_Examples.Models
{
    [Table("Customer")]
    public class E_03_EF_Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<E_03_EF_Order> Orders { get; set; }
    }
}