using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class OrderedService
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public DateTime DayReserv { get; set; }
        public string Login { get; set; }

        [NotMapped]
        public decimal Cost { get; set; }
        [NotMapped]
        public int DurationInMinutes { get; set; }
        [NotMapped]
        public string MainImagePath { get; set; }
        [NotMapped]
        public string Title { get; set; }
    }
}
