using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WpfApp1.Model;

namespace WpfApp1.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<OrderedService> OrderedServices {get; set;}
        public DbSet<OrderedProduct> OrderedProducts { get; set; }
   
        public  DataBaseContext() :
            base("DBConnection")
        { }
    }
    
}
