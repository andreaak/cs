using System;
using System.Collections.Generic;

namespace CSTest._21_Database._02_EntityFramework._01_DatabaseFirst.Models;

public partial class Customer
{
    public int CustNum { get; set; }

    public string Company { get; set; }

    public int? CustRep { get; set; }

    public decimal? CreditLimit { get; set; }

    public virtual Salesrep CustRepNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
