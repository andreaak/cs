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
    
    public partial class Customers2
    {
        public Customers2()
        {
            this.Orders2 = new HashSet<Orders2>();
        }
    
        public int cnum { get; set; }
        public string cname { get; set; }
        public string city { get; set; }
        public Nullable<int> rating { get; set; }
        public Nullable<int> snum { get; set; }
    
        public virtual Salespeople Salespeople { get; set; }
        public virtual ICollection<Orders2> Orders2 { get; set; }
    }
}