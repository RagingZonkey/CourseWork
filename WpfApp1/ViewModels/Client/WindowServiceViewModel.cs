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


        public WindowServiceViewModel()
        {
            var entity = App.db.Services.SingleOrDefault();
            Services.Add(new Service
            {
                ID = int.Parse(entity.ID.ToString()),
                Title = entity.Title.ToString(),
                Cost = float.Parse(entity.Cost.ToString()).ToString() + " рублей за " /*+ int.Parse(entity.DurationInSeconds.ToInt32())/60) + " мин"*/,
                Costedit = float.Parse(entity.Costedit.ToString()).ToString(),
                DurationInSeconds = int.Parse(entity.DurationInSeconds.ToString()).ToString(),
                Discount = float.Parse(entity.Discount.ToString()).ToString() + "% скидка",
                DiscountEdit = float.Parse(entity.DiscountEdit.ToString()).ToString(),
                MainImagePath = entity.MainImagePath.ToString()
            });
            Tovari = new RelayCommand(Click_Tovari);
            Order = new RelayCommand(go_order);
            Change = new RelayCommand(Click_change);
            WinClient = new RelayCommand()
            Exit

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
