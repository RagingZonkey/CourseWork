using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
   public class Product
    {

        
        public int Id { get; set; }
        public string Title { get; set; }

        public decimal Cost { get; set; }

        public string Description { get; set; }
        public string MainImagePath { get; set; }        
    }
}
