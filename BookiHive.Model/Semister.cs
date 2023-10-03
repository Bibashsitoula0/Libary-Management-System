using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    [Table("semister")]
    public class Semister
    {
        public int id { get; set; }
        public string semister { get; set; }
    }
}
