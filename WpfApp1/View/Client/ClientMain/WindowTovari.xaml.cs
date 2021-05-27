using System.Windows;
using WpfApp1.ViewModels.Client;

namespace WpfApp1.view
{
    
    public partial class WindowTovari : Window
    {
        
        public WindowTovari(string login)
        {
            InitializeComponent();
            DataContext = new WindowTovariViewModel(login);
            
        }

        
    }
}
