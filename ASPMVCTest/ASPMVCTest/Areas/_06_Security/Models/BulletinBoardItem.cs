using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _01_ASPMVCTest.Areas._06_Security.Models
{

    public static class BulletinBoardRepository
    {
        public static List<BulletinBoardItem> Items = new List<BulletinBoardItem>();
    }
   

    public class BulletinBoardItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}