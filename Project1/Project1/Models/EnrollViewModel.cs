using Project1.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class EnrollViewModel
    {
        public List<ItemList> Classes { get; set; }
        public int SelectedClass { get; set; }
    }
}
