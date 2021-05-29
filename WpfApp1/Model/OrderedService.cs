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
        public string DurationInMinutes { get; set; }
        public string DayReserv { get; set; }
        public string MainImagePath { get; set; }
        public string Login { get; set; } 
    }
}
