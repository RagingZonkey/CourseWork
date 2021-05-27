using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using WpfApp1.Model;
using WpfApp1.Context;
using WpfApp1.ViewModels;

namespace WpfApp1.view.Admin.Buttons
{
    
    public partial class Edit : Window
    {

        
        public Edit(Service init)
        {
            //this.Service = init;
            //title_box = init.Title;
            //cost_box = init.Costedit;
            //time_box = init.DurationInSeconds;
            //skidka_box = init.DiscountEdit;
            InitializeComponent();
            DataContext = new EditViewModel(init);

        }

       
    }
}
