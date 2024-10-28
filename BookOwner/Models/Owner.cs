using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookOwner.Models
{
    public class Owner
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<book> Books { get; set; }

    }
}
