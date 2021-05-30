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
using WpfApp1.view.Client.Buy;
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels.Client
{
    public class WindowServiceViewModel : BaseViewModel
    {
        public ICommand Tovari { get; private set; }
        public ICommand Order { get; private set; }
        public ICommand Change { get; private set; }
        public ICommand Exit { get; private set; }
        public ICommand WinClient { get; private set; }
        Service s;

        public string logins;
       

        public string Logins
        {
            get { return logins; }
            set
            {
                logins = value;
                OnPropertyChanged("Logins");
            }

        }

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

        private Service selectedService;

        public Service SelectedService
        {
            get { return selectedService; }
            set
            {
                selectedService = value;
                OnPropertyChanged("SelectedService");
            }
        }


        public WindowServiceViewModel(string login)
        {
            logins = login;
            Console.WriteLine(logins);
            var entity_services = App.db.Services.SingleOrDefault();
            Services = new ObservableCollection<Service>();

            Services.Add(new Service
            {
                Title = entity_services.Title.ToString(),
                Cost = entity_services.Cost.ToString() + " BYN",
                DurationInMinutes = "Продолжительность в минутах: " + entity_services.DurationInMinutes + " мин",
                ReservDay = "Дата резервации: " + entity_services.ReservDay.ToString(),
                MainImagePath = entity_services.MainImagePath.ToString()
            });
            Tovari = new RelayCommand(Click_Tovari);
            Order = new RelayCommand(go_order);
            Change = new RelayCommand(Click_change);
            WinClient = new RelayCommand(Click_Akk);
            Exit = new RelayCommand(Click_Exit);

        }

        private void Click_Tovari(object sender)
        {
            WindowTovari windowTovari = new WindowTovari(logins);
            windowTovari.Show();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowService)
                {
                    win.Close();
                }
            }
        }

        private void go_order(object sender)
        {
            s = SelectedService;
            if (SelectedService != null)
            {
                Order order = new Order(SelectedService, logins);
                foreach (Window win in Application.Current.Windows)
                {
                    if (win is WindowService)
                    {
                        win.Hide();
                    }
                }
                order.Show();
            }
            else
            {
                MessageBox.Show("Выберите услугу!", "Error");
            }
        }

        private void Click_Exit(object sender)
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowService)
                {
                    win.Close();
                }
            }
        }

        private void Click_Akk(object sender)
        {
            WindowClient windowClient = new WindowClient(logins);
            windowClient.Show();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowService)
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
                if (win is WindowService)
                {
                    win.Close();
                }
            }
        }
    }
}
