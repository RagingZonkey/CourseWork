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
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels.Client
{
    public class WindowClientViewModel : BaseViewModel
    {
        public ICommand Tovari { get; private set; }
        public ICommand Service { get; private set; }
        public ICommand Change { get; private set; }
        public ICommand Close { get; private set; }


        public string logins;

        private ObservableCollection<Service> services;

        public ObservableCollection<Service> Services
        {
            get { return services; }
            set
            {
                services = value;
                OnPropertyChanged("Services");
            }
        }

        public WindowClientViewModel(string login)
        {
            logins = login;
            var entity = App.db.OrderedServices.Where(x => x.Login == logins).SingleOrDefault();
            entity.Login = logins;
            App.db.SaveChangesAsync().GetAwaiter();

            Services.Add(new Service
            {
                ID = int.Parse(entity.Id.ToString()),
                Title = entity.Title.ToString(),
                DurationInSecondsEdit = "Время работы мастера " + int.Parse(entity.DurationInSeconds)/60 + " мин",
                OrderDate = "Дата записи: " + entity.OrderDate.ToString(),
                Discount = "Цена с учетом скидок: " + float.Parse(entity.Discount.ToString()).ToString() + " руб",
                MainImagePath = entity.ImagePath.ToString()
            });

            Tovari = new RelayCommand(Click_Tovari);
            Service = new RelayCommand(Click_Service);
            Change = new RelayCommand(Click_change);
            Close = new RelayCommand(go_close);
        }

        private void Click_Tovari(object sender)
        {
            WindowTovari windowTovari = new WindowTovari(logins);
            windowTovari.Show();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowClient)
                {
                    win.Close();
                }
            }
        }

        private void Click_Service(object sender)
        {
            WpfApp1.WindowService main = new WpfApp1.WindowService(logins);
            main.Show();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowClient)
                {
                    win.Close();
                }
            }
        }

        private void Click_change(object sender)
        {
            Login login = new Login();
            login.Show();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowClient)
                {
                    win.Close();
                }
            }
        }

        private void go_close(object sender)
        {
            
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowClient)
                {
                    win.Close();
                }
            }
           
        }


    }
}
