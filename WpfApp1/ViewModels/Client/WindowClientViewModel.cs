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
        public ICommand CleanUp { get; private set; }

        OrderedProduct p;
        public decimal resultingPrice;
        public decimal ResultingPrice
        {
            get { return resultingPrice; }
            set
            {
                resultingPrice = value;
                OnPropertyChanged("ResultingPrice");
            }

        }

        public decimal productsResultingPrice;
        public decimal ProductsResultingPrice
        {
            get { return productsResultingPrice; }
            set
            {
                productsResultingPrice = value;
                OnPropertyChanged("ProductsResultingPrice");
            }

        }


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
        private OrderedProduct selectedProduct;

        public OrderedProduct SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
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
            var entity_services = App.db.OrderedServices.Where(x => x.Login == logins);
            var entity_products = App.db.OrderedProducts.Where(y => y.Login == logins);


            OrderedProducts = new ObservableCollection<OrderedProduct>(entity_products);
            OrderedServices = new ObservableCollection<OrderedService>(entity_services);
            foreach(var service in OrderedServices)
            {
                //service.Cost = App.db.Services.FirstOrDefault(n => n.Id == service.ServiceId).Cost;
                service.DurationInMinutes = App.db.Services.FirstOrDefault(n => n.Id == service.ServiceId).DurationInMinutes;
                service.Cost = App.db.Services.FirstOrDefault(n => n.Id == service.ServiceId).Cost;
                service.MainImagePath = App.db.Services.FirstOrDefault(n => n.Id == service.ServiceId).MainImagePath;
                service.Title = App.db.Services.FirstOrDefault(n => n.Id == service.ServiceId).Title;
                ResultingPrice += service.Cost;
            }
            //OrderedProducts = new ObservableCollection<OrderedProduct>();

            foreach (var product in OrderedProducts)
            {
                product.Cost = App.db.Products.FirstOrDefault(n => n.Id == product.ProductId).Cost;
                product.Description = App.db.Products.FirstOrDefault(n => n.Id == product.ProductId).Description;
                product.Title = App.db.Products.FirstOrDefault(n => n.Id == product.ProductId).Title;
                product.MainImagePath = App.db.Products.FirstOrDefault(n => n.Id == product.ProductId).MainImagePath;
                ProductsResultingPrice += product.TotalPrice;
            }

            

            Tovari = new RelayCommand(Click_Tovari);
            Service = new RelayCommand(Click_Service);
            Change = new RelayCommand(Click_change);
            Close = new RelayCommand(go_close);
            CleanUp = new RelayCommand(go_cleanup);

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

        private void go_cleanup(object sender)
        {
            WindowService adm = new WindowService(logins);
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowClient)
                {
                    win.Close();
                }
            }


            
            p = SelectedProduct;
            if (p != null)
            {
                var orderedProductToDelete = App.db.OrderedProducts.Where(y => y.Id == p.Id);
                App.db.OrderedProducts.RemoveRange(orderedProductToDelete);
                App.db.SaveChanges();
            }
            adm.Show();


        }
    }
}
