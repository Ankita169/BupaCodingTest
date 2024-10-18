using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookOwner.Models
{
    public class book
    {
        public string Name { get; set; }
        public string Type { get; set; }

        //public string OwnerAdult { get; set; }

        //public string OwnerChild { get; set; }


    }


    public class Owner
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<book> OwnerAdult { get; set; }

        public List<book> OwnerChild { get; set; }

        public List<book> Books { get; set; }
    }

}