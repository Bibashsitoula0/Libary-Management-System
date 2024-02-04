using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    public class CartBookViewModel
    {
        public string userid { get; set; }
        public int CartId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SchoolCode { get; set; }
        public string BookName { get; set; }
        public string BookImage { get; set; }
        public int AuthorId { get; set; }
        public string Faculty { get; set; }
        public string Semister { get; set; }
        public int Year { get; set; }
        public string Author_Name { get; set; }
        public bool IsApproved { get; set; }
        public  string filename { get; set; }
        public bool istaken { get; set; }
    }
}
