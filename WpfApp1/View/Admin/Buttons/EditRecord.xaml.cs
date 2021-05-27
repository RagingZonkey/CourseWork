using System.Windows;
using WpfApp1.Model;
using WpfApp1.ViewModels;

namespace WpfApp1.view.Admin.Buttons
{
   
    public partial class EditRecord : Window
    {
        public EditRecord(Service init)
        {
            InitializeComponent();
            DataContext = new EditRecordViewModel(init);
        }

        
    }
}
