using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    public  class CartList
    {
        public int totalcount_distinct { get; set; }
        public int bookid { get; set; }
        public string bookname { get; set; }
        public int minutes { get; set; }
        public string  userid { get; set; }
        public string authorname { get; set; }
        public string faculty { get; set; }
        public string semister { get; set; }
        public string year { get; set; }
        public double  price { get; set; }
        public int cartid { get; set; }
        public int request { get; set; }
    }
}
