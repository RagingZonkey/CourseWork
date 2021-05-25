using DbConnect;
using System;
using System.Collections.Generic;
using System.Data;
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
using WpfApp1.Model;
using Microsoft.Win32;
using WpfApp1.ViewModels;

namespace WpfApp1.view.Admin.Buttons
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public string imagepath;
        public Add()
        {
            InitializeComponent();
            DataContext = new AddViewModel();

        }

    }
}
