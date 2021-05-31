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
using WpfApp1.view;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using WpfApp1.ViewModels.Client;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        RegisterViewModel vm;
        public Register()
        {
            InitializeComponent();
            vm = new RegisterViewModel();
            DataContext = vm;
        }

        private void passwordbox_one_PasswordChanged(object sender, RoutedEventArgs e)
        {
            vm.FirstPasswordBox = passwordbox_one.Password;
        }

        private void passwordbox_two_PasswordChanged(object sender, RoutedEventArgs e)
        {
            vm.SecondPasswordBox = passwordbox_two.Password;
        }
    }
}


