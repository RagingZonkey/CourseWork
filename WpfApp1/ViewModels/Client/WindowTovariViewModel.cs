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
using WpfApp1.view.Client.Buttons;
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels.Client
{
    public class WindowTovariViewModel : BaseViewModel
    {
        public ICommand Service { get; private set; }
        public ICommand Exit { get; private set; }
        public ICommand Main { get; private set; }
        public ICommand Change { get; private set; }
        public ICommand Order { get; private set; }


        Product p;
        public string logins;

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                products = value;
                OnPropertyChanged("Services");
            }
        }

        private Product selectedProduct;

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedService");
            }
        }


        public WindowTovariViewModel(string login)
        {
            logins = login;
            var entity = App.db.Products.SingleOrDefault();

            products.Add(new Product
            {
                ID = int.Parse(entity.Id.ToString()),
                Title = entity.Title.ToString(),
                Cost = "Цена - " + float.Parse(entity.Cost.ToString()).ToString() + " рублей",
                CostEdit = float.Parse(entity.CostEdit.ToString()).ToString(),
                DescriptionEdit = entity.DescriptionEdit.ToString(),
                Description = "Описание - " + entity.Description.ToString(),
                MainImagePath = entity.MainImagePath.ToString(),
            });

            Service = new RelayCommand(go_service);
            Order = new RelayCommand(go_order);
            Change = new RelayCommand(go_change);
            Main = new RelayCommand(go_main);
            Exit = new RelayCommand(go_exit);

        }

        private void go_service(object sender)
        {
            WindowService main = new WindowService(logins);
            main.Show();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowTovari)
                {
                    win.Close();
                }
            }
        }


        private void go_exit(object sender)
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowTovari)
                {
                    win.Close();
                }
            }
        }



        private void go_main(object sender)
        {
            WindowClient windowClient = new WindowClient(logins);
            windowClient.Show();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowTovari)
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
                if (win is WindowTovari)
                {
                    win.Close();
                }
            }
        }

        private void go_order(object sender)
        {
            p = SelectedProduct;
            if (SelectedProduct != null)
            {
                OrderProducts order = new OrderProducts(SelectedProduct, logins);
                foreach (Window win in Application.Current.Windows)
                {
                    if (win is WindowTovari)
                    {
                        win.Hide();
                    }
                }
                order.Show();
            }
            else
            {
                MessageBox.Show("Выберите продукт!", "Error");
            }
        }
    }
}
