using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    public class UserDetails
    {
        public string userid { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }
        public string codeno { get; set; }
        public string Qrcode { get; set; }
        public string faculty { get; set; }
        public string semister { get; set; }
        public string years { get; set; }
        public int fiscalyear { get; set; }
        public string phonenumber { get; set; }
        public string aboutus { get; set; }
        public string address { get; set; }
        public string image { get; set; }

        public DateTime Collegebooktakendate { get; set; }
        public DateTime Collegebookgivendate { get; set; }
        public int bookcount { get; set; }
        public string bookname1 { get; set; }
        public string bookname2 { get; set; }
        public string bookname3 { get; set; }
        public string bookname4 { get; set; }
        public string bookname5 { get; set; }
        public string bookname6 { get; set; }
        public string bookname7 { get; set; }
        public string bookname8 { get; set; }
        public string payment { get; set; }
    }
}
