using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    [Table("cart")]
    public class Cart
    {
        public int id { get; set; }
        public string userid { get; set; }
        public int bookid { get; set; }
        public string bookname { get; set; }
        public DateTime time { get; set; }
    }
}
