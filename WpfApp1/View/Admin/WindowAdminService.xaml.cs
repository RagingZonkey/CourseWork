using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1.view.Admin
{
    public partial class WindowAdminService : Window
    {
        public WindowAdminService()
        {
            InitializeComponent();
            DataContext = new WindowAdminServiceViewModel();
        }
    }
}
