using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1.view
{
    

    public partial class WindowAdmin : Window
    {
        public WindowAdmin()
        {
            InitializeComponent();
            DataContext = new WindowAdminViewModel();
        }


    }
}
