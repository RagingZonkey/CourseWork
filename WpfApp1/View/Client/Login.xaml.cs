using System.Windows;
using WpfApp1.ViewModels.Client;

namespace WpfApp1
{
    public partial class Login : Window
    {
        LoginViewModel vm;
        public Login()
        {
            InitializeComponent();
            vm = new LoginViewModel();
            DataContext = vm;
        }

        private void password_box_PasswordChanged(object sender, RoutedEventArgs e)
        {
            vm.Password_Box = password_box.Password;
        }
    }
}
