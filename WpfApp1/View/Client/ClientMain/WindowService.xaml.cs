using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfApp1.Model;
using WpfApp1.view;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using WpfApp1.view.Client.Buy;
using System.Diagnostics;
namespace WpfApp1
{
   
    public partial class WindowService : Window
    {
        

        public WindowService(string login)
        {
            
            InitializeComponent();

        }


    }
}
