using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    public class CartViewModel
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public bool IsApproved { get; set; }
        public bool Request { get; set; }
        public bool IsTaken { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Semister { get; set; }
        public int Faculty { get; set; }
        public decimal Price { get; set; }
        public string semister_name { get; set; }
        public string yearname { get; set; }
        public string faculty_name { get; set; }
    }
}
