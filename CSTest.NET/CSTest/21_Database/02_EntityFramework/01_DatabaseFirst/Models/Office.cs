using System;
using System.Collections.Generic;

namespace CSTest._21_Database._02_EntityFramework._01_DatabaseFirst.Models;

public partial class Office
{
    public int Office1 { get; set; }

    public string City { get; set; }

    public string Region { get; set; }

    public int? Mgr { get; set; }

    public decimal? Target { get; set; }

    public decimal Sales { get; set; }

    public virtual Salesrep MgrNavigation { get; set; }

    public virtual ICollection<Salesrep> Salesreps { get; set; } = new List<Salesrep>();
}
