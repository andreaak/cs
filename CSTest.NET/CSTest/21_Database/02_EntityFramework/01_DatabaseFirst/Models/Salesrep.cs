using System;
using System.Collections.Generic;

namespace CSTest._21_Database._02_EntityFramework._01_DatabaseFirst.Models;

public partial class Salesrep
{
    public int EmplNum { get; set; }

    public string Name { get; set; }

    public int? Age { get; set; }

    public int? RepOffice { get; set; }

    public string Title { get; set; }

    public DateTime HireDate { get; set; }

    public int? Manager { get; set; }

    public decimal? Quota { get; set; }

    public decimal Sales { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Salesrep> InverseManagerNavigation { get; set; } = new List<Salesrep>();

    public virtual Salesrep ManagerNavigation { get; set; }

    public virtual ICollection<Office> Offices { get; set; } = new List<Office>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Office RepOfficeNavigation { get; set; }
}
