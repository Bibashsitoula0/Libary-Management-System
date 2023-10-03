using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    [Table("faculty")]
    public class Faculty
    {
        public int id { get; set; }
        public string faculty { get; set; }
    }
}
