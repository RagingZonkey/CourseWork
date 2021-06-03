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
using WpfApp1.view.Admin.Buttons;
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels
{
    public class WindowProductsViewModel : BaseViewModel
    {
        public ICommand Edit { get; private set; }
        public ICommand Delete { get; private set; }
        public ICommand Add { get; private set; }
        public ICommand Exit { get; private set; }
        public ICommand Change { get; private set; }
        public ICommand Main { get; private set; }
        Product p;
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

        public WindowProductsViewModel()
        {
            Edit = new RelayCommand(go_edit);
            Delete = new RelayCommand(go_delete);
            Add = new RelayCommand(go_add);
            Exit = new RelayCommand(go_exit);
            Change = new RelayCommand(go_change);
            Main = new RelayCommand(go_main);


            Products = new ObservableCollection<Product>(App.db.Products);


        }

        private void go_edit(object sender)
        {
            if (SelectedProduct != null) // Магия / не трогать
            {
                EditProducts ed = new EditProducts(SelectedProduct);
                ed.Show();
                foreach (Window win in Application.Current.Windows)
                {
                    if (win is WindowProducts)
                    {
                        win.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите услугу!", "Error");
            }
        }

        private void go_delete(object sender)
        {
            p = SelectedProduct;
            var orderedProductToDelete = App.db.OrderedProducts.Where(y => y.ProductId == SelectedProduct.Id);
            if (p != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данный товар?", "Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    WindowAdmin adm = new WindowAdmin();
                    foreach (Window win in Application.Current.Windows)
                    {
                        if (win is WindowProducts)
                        {
                            win.Close();
                        }
                    }


                    adm.Show();

                    App.db.OrderedProducts.RemoveRange(orderedProductToDelete);
                    App.db.Products.Remove(p);
                    App.db.SaveChanges();


                    if (p == null)
                    {
                        MessageBox.Show("Товар успешно удален!", "Ok");

                    }



                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("В базе данных нет ни одного товара,\n для начала выполните добавление!");
            }
            
            
            
        }

        private void go_add(object sender)
        {
            AddProducts add = new AddProducts();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowProducts)
                {
                    win.Close();
                }
            }
            add.Show();


        }
       

        private void go_main(object sender)
        {
            WindowAdmin rec = new WindowAdmin();
            rec.Show();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowProducts)
                {
                    win.Close();
                }
            }
        }
        private void go_exit(object sender)
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowProducts)
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
                if (win is WindowProducts)
                {
                    win.Close();
                }
            }
        }

    }
}
