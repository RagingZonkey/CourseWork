using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Context;
using WpfApp1.Model;
using WpfApp1.view;
using WpfApp1.view.Admin;
using WpfApp1.view.Admin.Buttons;
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels
{
    public class EditProductsViewModel : BaseViewModel 
    {

        public ICommand Save_Service { get; private set; }
        public ICommand GoBack_EditView { get; private set; }
        public ICommand Select_Image { get; private set; }

        Product Product;
        public EditProductsViewModel(Product init)
        {
            this.Product = init;
            ID = init.Id;
            Title_Box = init.Title;
            Description_Box = init.Description;
            Cost_Box = init.Cost;
            Amount_Box = init.Amount;
            Save_Service = new RelayCommand(go_save);
            GoBack_EditView = new RelayCommand(go_back);
            Select_Image = new RelayCommand(select_image);
        }

        #region Fields and Properties
        private string desc_box;


        public string Description_Box
        {
            get { return desc_box; }
            set
            {
                desc_box = value;
                OnPropertyChanged("Description_Box");
            }
        }

        private int amount_box;

        public int Amount_Box
        {
            get { return amount_box; }
            set
            {
                amount_box = value;
                OnPropertyChanged("Amount_Box");
            }
        }


        private string title_box;

        public string Title_Box
        {
            get { return title_box; }
            set
            {
                title_box = value;
                OnPropertyChanged("Title_Box");
            }
        }

        private decimal cost_box;

        public decimal Cost_Box
        {
            get { return cost_box; }
            set
            {
                cost_box = value;
                OnPropertyChanged("Cost_Box");
            }
        }





        private string imagepath;

        public string ImagePath
        {
            get { return imagepath; }
            set { imagepath = value; }
        }

        public int ID { get; private set; }

        #endregion

        private void go_save(object obj)
        {
            if (ImagePath == null || Title_Box == null || Cost_Box.ToString() == null || Description_Box == null || Amount_Box.ToString() == null)
            {

                MessageBox.Show("Выберите изображение и заполните пустые поля!");
                

            }
            else
            {
                if (Cost_Box == 0 || Amount_Box == 0)
                {
                    MessageBox.Show("Проверьте правильность введенных данных о и количестве цене товара!");
                }
                else
                {

                    try
                    {
                        var entity = App.db.Products.Where(x => x.Id == ID).SingleOrDefault();
                        entity.Title = Title_Box;
                        entity.Description = Description_Box;
                        entity.Cost = Cost_Box;
                        entity.MainImagePath = ImagePath;
                        entity.Amount = Amount_Box;
                        foreach (OrderedProduct op in App.db.OrderedProducts.Where(x => x.ProductId == ID))
                        {
                            op.TotalPrice = entity.Cost * op.Quantity;
                        }
                        App.db.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        WindowAdmin winadm = new WindowAdmin();
                        MessageBox.Show("Продукт успешно отредактирован!");
                        foreach (Window win in Application.Current.Windows)
                        {
                            if (win is EditProducts)
                            {
                                win.Close();
                            }
                        }
                        winadm.Show();
                    }

                }
            }
            
        }

        private void select_image(object sender)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    imagepath = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                Console.WriteLine("path" + imagepath);
            }

        }




        private void go_back(object sender)
        {
            try
            {
                WindowAdminService winadm = new WindowAdminService();
                foreach (Window win in Application.Current.Windows)
                {
                    if (win is EditProducts)
                    {
                        win.Close();
                    }
                }
                winadm.Show();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

    }
    
}
