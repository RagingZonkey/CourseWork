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
    public class WindowClientViewModel : BaseViewModel
    {
        public ICommand Tovari { get; private set; }
        public ICommand Service { get; private set; }
        public ICommand Change { get; private set; }
        public ICommand Close { get; private set; }



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

        private ObservableCollection<OrderedService> orderedServices;

        public ObservableCollection<OrderedService> OrderedServices
        {
            get { return orderedServices; }
            set
            {
                orderedServices = value;
                OnPropertyChanged("OrderedServices");
            }
        }

        private ObservableCollection<OrderedProduct> orderedProducts;

        public ObservableCollection<OrderedProduct> OrderedProducts
        {
            get { return orderedProducts; }
            set
            {
                orderedProducts = value;
                OnPropertyChanged("Services");
            }
        }

        public WindowClientViewModel(string login)
        {
            logins = login;
            var entity_services = App.db.OrderedServices.Where(x => x.Login == logins).SingleOrDefault();
            var entity_products = App.db.OrderedProducts.Where(y => y.Login == logins).SingleOrDefault();
            //App.db.SaveChangesAsync().GetAwaiter();

            OrderedProducts = new ObservableCollection<OrderedProduct>();

            OrderedProducts.Add(new OrderedProduct
            {
                Title = entity_products.Title.ToString(),
                Cost = "Стоимость товара" +entity_products.Cost.ToString() + " BYN",
                TotalPrice = "Итого: "+ (decimal.Parse(entity_products.Cost) * entity_products.Quantity).ToString() + " BYN",
                Description = "Описание товара: " + entity_products.Description,
                Quantity = entity_products.Quantity,
                MainImagePath = entity_products.MainImagePath.ToString()
            });

            OrderedServices = new ObservableCollection<OrderedService>();

            OrderedServices.Add(new OrderedService 
            {
                Title = entity_services.Title.ToString(),
                Cost = entity_services.Cost.ToString() + " BYN",
                DurationInMinutes = "Продолжительность в минутах: " + entity_services.DurationInMinutes + " мин",
                DayReserv = "Дата резервации: " + entity_services.DayReserv.ToString(),
                MainImagePath = entity_services.MainImagePath.ToString()
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
