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
                OnPropertyChanged("Services");
            }
        }

        public WindowProductsViewModel()
        {
            Edit = new RelayCommand(go_edit);
            Delete = new RelayCommand(go_delete);
            Add = new RelayCommand(go_add);
            Exit = new RelayCommand(go_exit);
            Change = new RelayCommand(go_change);
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
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данный товар?", "Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                //DB db = new DB();
                //SqlCommand command = new SqlCommand("DELETE FROM Product WHERE ID = @id", db.getConnection());
                //command.Parameters.AddWithValue("@id", s.ID);
                //db.openConnection();

                try 
                {
                
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show("Продукт успешно удален!", "Ok");
                    WindowProducts adm = new WindowProducts();
                    foreach (Window win in Application.Current.Windows)
                    {
                        if (win is WindowProducts)
                        {
                            win.Close();
                        }
                    }
                    adm.Show();
                }
                
            }
            else
            {

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
        private void productlist_SelectionChanged(object sender)
        {
            Service p = (Service)productlist.SelectedItem;
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
