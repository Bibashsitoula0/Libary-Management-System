using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    [Table("years")]
    public class Years
    {
        public int id { get; set; }
        public string year { get; set; }
    }
}
