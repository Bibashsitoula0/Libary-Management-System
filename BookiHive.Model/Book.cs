using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    [Table("books")]
    public class Book
    {
        public int id { get; set; }
        public string bookname { get; set; }
        public int year { get; set; }
        public int semister { get; set; }
        public int faculty { get; set; }
        public int authorid { get; set; }
        public string createdby { get; set; }
        public string updatedby { get; set; }
        public int totalbook { get; set; }
        public string bookimage { get; set; }
        public double price { get; set; }
        public string publisher { get; set; }
        public bool isdeleted { get; set; }
    }
}
