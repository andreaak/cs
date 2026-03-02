using System;
using System.Collections.Generic;

namespace CSTest._21_Database._02_EntityFramework._01_DatabaseFirst.Models;

public partial class Product
{
    public string MfrId { get; set; }

    public string ProductId { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int QtyOnHand { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
