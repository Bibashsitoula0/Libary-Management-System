using BookHive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiHive.Model
{
    public class StudentData
    {
        public List<CartViewModel> cartViewModels {  get; set; }
        public List<RegisterList> userdata { get; set; }
    }
}
