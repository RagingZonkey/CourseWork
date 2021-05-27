using System;
using System.Collections.Generic;
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
        public OrderProductsViewModel(Product init, string login)
        {
            
            logins = login;
            this.product = init;
            title_box = init.Title;
            desc_box = init.DescriptionEdit;
            cost_box = init.CostEdit;
            resultingCost_box = init.CostEdit;
            Order = new RelayCommand(go_order);
            Back = new RelayCommand(go_back);
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

        private string resultingCost_box;

        public string ResultingCost_Box
        {
            get { return resultingCost_box; }
            set
            {
                resultingCost_box = value;
                OnPropertyChanged("ResultingCost_Box");
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

        public int Id { get; private set; }

        private void go_order(object sender)
        {
            try
            {
                var entity = App.db.OrderedProducts.FirstOrDefault(x => x.Id == Id);
                entity.Title = Title_Box;
                entity.Cost = Cost_Box;
                entity.TotalPrice = ResultingCost_Box;
                entity.Description = Description_Box;
                entity.Login = logins;
                entity.MainImagePath = product.MainImagePath;
                App.db.SaveChangesAsync().GetAwaiter();
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
