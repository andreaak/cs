//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSTest._21_Database._02_EntityFramework._01_DatabaseFirst
{
    using System;
    using System.Collections.Generic;
    
    public partial class Salespeople
    {
        public Salespeople()
        {
            this.Customers2 = new HashSet<Customers2>();
            this.Orders2 = new HashSet<Orders2>();
        }
    
        public int snum { get; set; }
        public string sname { get; set; }
        public string city { get; set; }
        public decimal comm { get; set; }
    
        public virtual ICollection<Customers2> Customers2 { get; set; }
        public virtual ICollection<Orders2> Orders2 { get; set; }
    }
}