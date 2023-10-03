using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    public class BookList
    {
        public int bookid { get; set; }
        public string bookname { get; set; }
        public string year { get; set; }
        public string semistertype { get; set; }
        public string facultytype { get; set; }
        public string authorname { get; set; }
        public int totalbook { get; set; }
        public double price { get; set; }
        public string publisher { get; set; }
        public string bookimage { get; set; }
        public int totalbooktaken { get; set; }
        public int availablebooks { get; set; }
    }
}
