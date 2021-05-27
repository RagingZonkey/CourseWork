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
using WpfApp1.view.Client.Buy;
using WpfApp1.ViewModels.Base;


namespace WpfApp1.ViewModels.Client
{
    public class OrderViewModel : BaseViewModel
    {
        public ICommand OrderService { get; private set; }
        public ICommand Back { get; private set; }

        Service Service;
        public string logins;

        public OrderViewModel(Service init, string login)
        {

            logins = login;
            this.Service = init;
            title_box = init.Title;
            time_box = init.DurationInSeconds;
            cost_box = init.Costedit;
            skidka_box = init.DiscountEdit.ToString() + "%";

            if (double.Parse(init.DiscountEdit) != 0)
            {
                double percent = double.Parse(init.DiscountEdit);
                double number = double.Parse(cost_box);
                int result = int.Parse(cost_box) - (int)Math.Round(number * (percent / 100));
                resultingCost_box = result.ToString();
            }
            else if (double.Parse(init.DiscountEdit) == 0)
            {
                skidka_box = "Отсутствует";
                resultingCost_box = init.Costedit;
            }


            OrderService = new RelayCommand(go_order);
            Back = new RelayCommand(go_back);
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

        private DateTime date_box;

        public DateTime Date_Box
        {
            get { return date_box; }
            set { date_box = value; }
        }

        public int Id { get; private set; }

        private void go_order(object sender)
        {
            DateTime curDate = DateTime.Now;
            //DateTime? selectedDate = Date_Box.SelectedDate;
            if(date_box > DateTime.Now)
            {
                try
                {
                    var entity = App.db.OrderedServices.FirstOrDefault(x => x.Id == Id);
                    entity.Title = Title_Box;
                    entity.Cost = Cost_Box;
                    entity.TotalPrice = ResultingCost_Box;
                    entity.Description = Description_Box;
                    entity.Discount = Service.DiscountEdit;
                    entity.OrderDate = curDate.ToShortDateString();
                    entity.DayReserv = date_box.Date.ToShortDateString();
                    entity.MainImagePath = Service.MainImagePath;
                    entity.Login = logins;
                    App.db.SaveChangesAsync().GetAwaiter();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                finally
                {
                    WindowClient winadm = new WindowClient(logins);
                    MessageBox.Show("Товар успешно зарезервирована!");
                    foreach (Window win in Application.Current.Windows)
                    {
                        if (win is Order)
                        {
                            win.Close();
                        }
                    }
                    winadm.Show();
                }
            }
            else
            {
                MessageBox.Show("Выберите правильную дату!", "Error");
            }



        }




        private void go_back(object sender)
        {
            WindowClient winadm = new WindowClient(logins);
            foreach (Window win in Application.Current.Windows)
            {
                if (win is Order)
                {
                    win.Close();
                }
            }
            winadm.Show();
        }


    }
}
