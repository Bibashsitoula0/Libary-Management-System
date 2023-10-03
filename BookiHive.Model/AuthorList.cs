using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    [Table("author")]
    public class AuthorList
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
