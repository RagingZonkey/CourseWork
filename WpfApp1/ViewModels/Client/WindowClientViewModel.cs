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

            //Services.Add(new Service
            //{
            //    ID = int.Parse(reader[0].ToString()),
            //    Title = reader[1].ToString(),
            //    DurationInSecondsEdit = "Время работы мастера " + float.Parse(reader[4].ToString()).ToString() + " мин",
            //    OrderDate = "Дата записи: " + reader[7].ToString(),
            //    Discount = "Цена с учетом скидок: " + float.Parse(reader[3].ToString()).ToString() + " руб",
            //    MainImagePath = reader[9].ToString()
            //});

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
