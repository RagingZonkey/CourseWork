using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.view.Admin;
using WpfApp1.view.Admin.Buttons;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.ViewModels.Base;
using System.Windows;
using WpfApp1.Model;

namespace WpfApp1.ViewModels
{
    public class AddProductsViewModel : BaseViewModel
    {
        public ICommand Add_Service { get; private set; }
        public ICommand GoBack_AddProductView { get; private set; }
        public ICommand Select_Image { get; private set; }


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

        private string imagepath;

        public string ImagePath
        {
            get { return imagepath; }
            set
            {
                imagepath = value;
                
            }
        }

        public AddProductsViewModel()
        {
            Add_Service = new RelayCommand(go_add);
            GoBack_AddProductView = new RelayCommand(go_back);
            Select_Image = new RelayCommand(select_image);

        }

        private void select_image(object obj)
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

        private void go_back(object obj)
        {
            try
            {
                WindowProducts winadm = new WindowProducts();
                foreach (Window win in Application.Current.Windows)
                {
                    if (win is AddProducts)
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

        private void go_add(object obj)
        {
            if (ImagePath == null || Title_Box == null || Cost_Box.ToString() == null || Description_Box == null || Amount_Box.ToString() == null)
            {

                MessageBox.Show("Выберите изображение и заполните пустые поля!");
                

            }
            else

            {
                if (Cost_Box == 0 || Amount_Box == 0)
                {
                    MessageBox.Show("Проверьте правильность введенных данных о цене и количестве товара!");
                }
                else
                {
                    try
                    {
                        App.db.Products.Add(new Product { Title = Title_Box, Cost = Cost_Box, Description = Description_Box, MainImagePath = ImagePath, Amount = Amount_Box });
                        App.db.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                    finally
                    {


                        MessageBox.Show("Продукт успешно добавлен!");
                        WindowProducts winadm = new WindowProducts();
                        foreach (Window win in Application.Current.Windows)
                        {
                            if (win is AddProducts)
                            {
                                win.Close();
                            }
                        }
                        winadm.Show();



                    }
                }
            }
        }
        
    }


}
