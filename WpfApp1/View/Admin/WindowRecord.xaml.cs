using System;
using System.Collections.Generic;
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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using WpfApp1.Model;
using WpfApp1.view.Admin.Buttons;
using WpfApp1.view.Admin;
using WpfApp1.ViewModels;

namespace WpfApp1.view.Admin
{
    
    public partial class WindowRecord : Window
    {
        public WindowRecord()
        {
            InitializeComponent();
            DataContext = new WindowRecordViewModel();
        }
    }
}
