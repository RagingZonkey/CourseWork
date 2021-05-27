using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class OrderedService
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cost { get; set; }
        public string TotalPrice { get; set; }
        public string Description { get; set; }
        public string Discount { get; set; }
        public string DurationInSeconds { get; set; }
        public string OrderDate { get; set; }
        public string DayReserv { get; set; }
        public string MainImagePath { get; set; }
        public string Login { get; set; } 
    }
}
