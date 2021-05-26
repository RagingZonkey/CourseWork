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

        private string cost_box;

        public string Cost_Box
        {
            get { return cost_box; }
            set
            {
                cost_box = value;
                OnPropertyChanged("Cost_Box");
            }
        }

        private string time_box;

        public string Time_Box
        {
            get { return time_box; }
            set
            {
                time_box = value;
                OnPropertyChanged("Time_Box");
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
                WindowAdminService winadm = new WindowAdminService();
                foreach (Window win in Application.Current.Windows)
                {
                    if (win is Add)
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
            if (imagepath != null)
            {
                try
                {
                    App.db.Products.Add(new Product { Title = Title_Box, Cost = Cost_Box, Description = Description_Box, MainImagePath = ImagePath });
                    App.db.SaveChangesAsync().GetAwaiter();

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (imagepath.ToString() != "")
                    {
                        if (ProductsCheckTextBoxes())
                        {
                            MessageBox.Show("Продукт успешно добавлен!");
                            WindowAdminService winadm = new WindowAdminService();
                            foreach (Window win in Application.Current.Windows)
                            {
                                if (win is Add)
                                {
                                    win.Close();
                                }
                            }
                            winadm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Заполните пустые поля");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите изображение");
                    }

                }
            }
        }
        private Boolean ProductsCheckTextBoxes()
        {
            String Title = title_box;
            String Cost = cost_box;
            String DurationInSeconds = time_box;



            if (Title == String.Empty || Cost == String.Empty || DurationInSeconds == String.Empty)
            {
                return false;
            }
            return true;
        }
    }


}
