using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    [Table("bookissue")]
    public class Bookissue
    {
        public int id { get; set; }
        public string issuer_id { get; set; }
        public string issue_name { get; set; }
        public int book_id { get; set; }
        public int bookcount { get; set; }
        public DateTime bookborrow { get; set; }
        public DateTime booklastreminddate  { get; set; }
        public string status { get; set; }
    }
}
