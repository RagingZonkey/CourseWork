using System;
using System.Collections.ObjectModel;
using System.Linq;
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





        public ICommand Tovari { get; private set; }
        public ICommand Service { get; private set; }
        public ICommand Change { get; private set; }
        public ICommand Close { get; private set; }
        public ICommand CleanUp { get; private set; }
        public ICommand CleanUp_OS { get; private set; }

        OrderedProduct op;
        Product p;
        OrderedService s;

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
        private OrderedService selectedService;

        public OrderedService SelectedService
        {
            get { return selectedService; }
            set
            {
                selectedService = value;
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
            CleanUp_OS = new RelayCommand(go_cleanup_os);

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
            //WindowService adm = new WindowService(logins);
            //foreach (Window win in Application.Current.Windows)
            //{
            //    if (win is WindowClient)
            //    {
            //        win.Close();
            //    }
            //}


            
            op = SelectedProduct;
            if (op != null)
            {
                var orderedProductToDelete = App.db.OrderedProducts.FirstOrDefault(y => y.Id == op.Id);
                var productAmountRenewal = App.db.Products.FirstOrDefault(x => x.Id == orderedProductToDelete.ProductId);
                productAmountRenewal.Amount += op.Quantity;
                App.db.OrderedProducts.Remove(orderedProductToDelete);
                OrderedProducts.Remove(orderedProductToDelete);
                App.db.SaveChanges();
            }
            //adm.Show();


        }

        private void go_cleanup_os(object sender)
        {
            try {
                //WindowService adm = new WindowService(logins);
                //foreach (Window win in Application.Current.Windows)
                //{
                //    if (win is WindowClient)
                //    {
                //        win.Close();
                //    }
                //}
                s = SelectedService;
                if (s != null)
                {
                    var orderedServiceToDelete = App.db.OrderedServices.FirstOrDefault(y => y.Id == s.Id);
                    App.db.OrderedServices.Remove(orderedServiceToDelete);
                    OrderedServices.Remove(orderedServiceToDelete);
                    App.db.SaveChanges();
                }
                //adm.Show();
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            



            


        }
    }
}
