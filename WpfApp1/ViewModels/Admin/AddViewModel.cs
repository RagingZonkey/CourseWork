using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Model;
using WpfApp1.view.Admin;
using WpfApp1.view.Admin.Buttons;
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels
{
    public class AddViewModel : BaseViewModel
    {
        public ICommand Add_Service { get; private set; }
        public ICommand GoBack_AddView { get; private set; }
        public ICommand Select_Image { get; private set; }

        #region Fields and Properties
        private string title_box;

        public string Title_Box
        {
            get { return title_box; }
            set { title_box = value;
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

        private string skidka_box;
        

        public string Skidka_Box
        {
            get { return skidka_box; }
            set
            {
                skidka_box = value;
                OnPropertyChanged("Skidka_Box");
            }
        }

        private string imagepath;

        public string ImagePath
        {
            get { return imagepath; }
            set { imagepath = value; }
        }

        #endregion

        public AddViewModel()
        {
            Add_Service = new RelayCommand(go_add);
            GoBack_AddView = new RelayCommand(go_back);
            Select_Image = new RelayCommand(select_image);

        }

        private void go_add(object obj)
        {
            try
            {
                App.db.Services.Add(new Service { Title = Title_Box, Cost = Cost_Box, DurationInMinutes = Time_Box, Discount = Skidka_Box, MainImagePath = ImagePath });
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
                    if (CheckTextBoxes())
                    {
                        MessageBox.Show("Услуга успешно добавлена!");
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

        public Boolean CheckTextBoxes()
        {
            String Title = title_box;
            String Cost = cost_box;
            String DurationInMinutes = time_box;
            


            if (Title == String.Empty || Cost == String.Empty || DurationInMinutes == String.Empty)
            {
                return false;
            }
            else
            {
                return true;
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
    }
}
