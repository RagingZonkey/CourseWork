using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{   
    public class Service
    {
        
        public int Id { get; set; }


        public string Title { get; set; }

        public decimal Cost { get; set; }


        public int DurationInMinutes { get; set; }

        public string MainImagePath { get; set; }
 
    }
}
