using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Model;
using WpfApp1.view;
using WpfApp1.view.Admin;
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels
{
    public class WindowAdminViewModel : BaseViewModel
    {
        public ICommand Record { get; private set; }
        public ICommand Service { get; private set; }
        public ICommand Products { get; private set; }
        public ICommand Change { get; private set; }
        public ICommand Exit { get; private set; }

        public WindowAdminViewModel()
        {
            Record = new RelayCommand(go_record);
            Service = new RelayCommand(go_service);
            Products = new RelayCommand(go_products);
            Exit = new RelayCommand(go_exit);
            Change = new RelayCommand(go_change);
        }

       
        private void go_record(object sender)
        {
            WindowRecord record = new WindowRecord();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowAdmin)
                {
                    win.Close();
                }
            }
            record.Show();
        }
        private void go_service(object sender)
        {
            WindowAdminService service = new WindowAdminService();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowAdmin)
                {
                    win.Close();
                }
            }
            service.Show();
        }
        private void go_products(object sender)
        {
            WindowProducts product = new WindowProducts();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowAdmin)
                {
                    win.Close();
                }
            }
            product.Show();
        }
        private void go_exit(object sender)
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowAdmin)
                {
                    win.Close();
                }
            }
        }

        private void go_change(object sender)
        {
            Login login = new Login();
            login.Show();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowAdmin)
                {
                    win.Close();
                }
            }
        }

        






    }
}
