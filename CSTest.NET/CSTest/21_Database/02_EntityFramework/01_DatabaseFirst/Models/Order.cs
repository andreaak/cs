using System;
using System.Collections.Generic;

namespace CSTest._21_Database._02_EntityFramework._01_DatabaseFirst.Models;

public partial class Order
{
    public int OrderNum { get; set; }

    public DateTime OrderDate { get; set; }

    public int Cust { get; set; }

    public int? Rep { get; set; }

    public string Mfr { get; set; }

    public string Product { get; set; }

    public int Qty { get; set; }

    public decimal Amount { get; set; }

    public virtual Customer CustNavigation { get; set; }

    public virtual Product ProductNavigation { get; set; }

    public virtual Salesrep RepNavigation { get; set; }
}
