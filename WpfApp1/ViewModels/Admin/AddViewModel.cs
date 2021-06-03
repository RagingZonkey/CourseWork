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

        private int time_box;

        public int Time_Box
        {
            get { return time_box; }
            set
            {
                time_box = value;
                OnPropertyChanged("Time_Box");
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
            if (ImagePath == null || Title_Box == null || Cost_Box.ToString() ==null || Time_Box.ToString() == null)
            {

                MessageBox.Show("Выберите изображение или заполните пустые поля!");


            }
        
            else 
            {
                try
                {

                    App.db.Services.Add(new Service { Title = Title_Box, Cost = Cost_Box, DurationInMinutes = Time_Box, MainImagePath = ImagePath});
                    App.db.SaveChanges();

                }
                catch (Exception ex)
                {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                finally
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
