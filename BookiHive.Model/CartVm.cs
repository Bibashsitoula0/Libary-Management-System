using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    public class CartVm
    {
        public int id { get; set; }
        public string userid { get; set; }
        public int bookid { get; set; }
        public string bookname { get; set; }
        public DateTime time { get; set; }
        public int count { get; set; }
        public int Request { get; set; }
        public List<CartList> cartlist { get; set; }
    }
}
