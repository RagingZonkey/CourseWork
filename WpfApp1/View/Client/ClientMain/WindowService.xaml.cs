using System.Windows;
using WpfApp1.ViewModels.Client;

namespace WpfApp1
{
   
    public partial class WindowService : Window
    {
        

        public WindowService(string login)
        {
            
            InitializeComponent();
            DataContext = new WindowServiceViewModel(login);

        }


    }
}
