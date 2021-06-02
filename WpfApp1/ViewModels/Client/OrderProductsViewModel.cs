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
    public class OrderProductsViewModel : BaseViewModel
    {

        public ICommand Order { get; private set; }
        public ICommand Back { get; private set; }
        Product product;
        public string logins;
        
        public OrderProductsViewModel(/*Product init,*/ string login)
        {
            
            logins = login;
            //this.product = init;
            //title_box = init.Title;
            Products = new ObservableCollection<Product>(App.db.Products);
            Order = new RelayCommand(go_order);
            Back = new RelayCommand(go_back);
        }

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                products = value;
                OnPropertyChanged("Products");
            }
        }

        

        private Product selectedProduct;

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        private int quantity_box;

        public int Quantity_Box
        {
            get { return quantity_box; }
            set
            {
                quantity_box = value;
                OnPropertyChanged("Quantity_Box");
            }
        }

        private string title_box;

        public string Title_Box
        {
            get { return title_box; }
            set { title_box = value; }
        }








        

        private void go_order(object sender)
        {
            try
            {
                
                OrderedProduct orderedProduct = new OrderedProduct
                {
                    Title = SelectedProduct.Title,
                    Cost = SelectedProduct.Cost,
                    Quantity = Quantity_Box,
                    TotalPrice = (Quantity_Box * int.Parse(SelectedProduct.Cost)).ToString() ,
                    MainImagePath = SelectedProduct.MainImagePath,
                    Login = logins

                };
                App.db.OrderedProducts.Add(orderedProduct);
                App.db.SaveChanges();
            }
            catch(Exception ex) 
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally 
            {
                WindowClient winadm = new WindowClient(logins);
                MessageBox.Show("Товар успешно зарезервирован!");
                foreach (Window win in Application.Current.Windows)
                {
                    if (win is OrderProducts)
                    {
                        win.Close();
                    }
                }
                winadm.Show();
            }
          


            
        }




        private void go_back(object sender)
        {
            WindowClient winadm = new WindowClient(logins);
            foreach (Window win in Application.Current.Windows)
            {
                if (win is OrderProducts)
                {
                    win.Close();
                }
            }
            winadm.Show();
        }


    }
}
