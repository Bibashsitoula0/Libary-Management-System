using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    public class BookVm
    {
        public int Id { get; set; }
        public bool isdeleted { get; set; }
        public string bookname { get; set; }
        public int year { get; set; }
        public int semister { get; set; }
        public int faculty { get; set; }
        public int authorid { get; set; }
        public string createdby { get; set; }
        public string updatedby { get; set; }
        public int totalbook { get; set; }
        public IFormFile bookimage { get; set; }
        public double price { get; set; }
        public string publisher { get; set; }
    }
}
