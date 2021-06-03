using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class OrderedProduct
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public string Login { get; set; }
        public int ProductId { get; set; }

        [NotMapped]
        public decimal Cost { get; set; }
        [NotMapped]
        public string Description { get; set; }
        [NotMapped] 
        public string MainImagePath { get; set; }
        [NotMapped]
        public string Title { get; set; }
    
    }
}
