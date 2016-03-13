using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _01_ASPMVCTest.Areas._003_Model.Models
{
    public class MyModel 
    {
        public int Prop1 { get; set; }
        public string Prop2 { get; set; }
        public bool Prop3 { get; set; }
    }

    public class MyModel2 : MyModel
    {
    }
}