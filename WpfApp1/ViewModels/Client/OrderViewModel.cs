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

            OrderService = new RelayCommand(go_order);
            Back = new RelayCommand(go_back);
        }


        private ObservableCollection<Service> services;

        public ObservableCollection<Service> Services
        {
            get { return services; }
            set
            {
                services = value;
                OnPropertyChanged("Products");
            }
        }

        private Service selectedService;

        public Service SelectedService
        {
            get { return selectedService; }
            set
            {
                selectedService = value;
                OnPropertyChanged("SelectedProduct");
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
            if(Date_Box > DateTime.Now)
            {
                try
                {
                    Services = new ObservableCollection<Service>(App.db.Services);
                    OrderedService orderedService = new OrderedService
                    {
                        Title = SelectedService.Title,
                        Cost = SelectedService.Cost,
                        DayReserv = SelectedService.ReservDay,
                        MainImagePath = SelectedService.MainImagePath,
                        Login = logins
                       
                    };
                    var entity = App.db.OrderedServices.FirstOrDefault(x => x.Id == Id);
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
