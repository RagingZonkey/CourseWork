using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private string selectedTime;
        //DateTime origin;

        public string SelectedTime
        {
            get { return selectedTime; }
            set 
            {
                selectedTime = value;
                OnPropertyChanged("SelectedTime");
            }
        }

        //Service Service;
        public string logins;
        //OrderedService orderedService;

        public OrderViewModel(/*Service init,*/ string login)
        {

            logins = login;
            //this.Service = init;
            //title_box = init.Title;
            Services = new ObservableCollection<Service>(App.db.Services);

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

        private void go_order(object sender)
        {
            DateTime oldDate = Date_Box;
            DateTime endTime = new DateTime(1, 1, 1, 20, 0, 0);
            DateTime startTime = new DateTime(1, 1, 1, 8, 0, 0);


            Regex timeValidation = new Regex("^(0[0-9]|1[0-9]|2[0-3]|[0-9]):[0-5][0-9]$");
            if (!timeValidation.IsMatch(SelectedTime))
            {
                System.Windows.Forms.MessageBox.Show("Дурак, цифры научись вводить");
                Date_Box = oldDate;
                return;
            }
            DateTime curDate = DateTime.Now;
            bool flag = true;
            string[] date = SelectedTime.Split(':');
            Date_Box = Date_Box.AddHours(int.Parse(date[0]));
            Date_Box = Date_Box.AddMinutes(int.Parse(date[1]));
            DateTime dateTime = Date_Box;
            DateTime dateTimeFromService;
            int duration = 0;
            if (SelectedService != null)
            {
                duration = SelectedService.DurationInMinutes;
            }
            else flag = false;
            //dateTime = Date_Box.AddMinutes(duration);
            dateTime = dateTime.AddMinutes(duration);
            endTime = endTime.AddYears(Date_Box.Year - 1);
            endTime = endTime.AddMonths(Date_Box.Month -1);
            endTime = endTime.AddDays(Date_Box.Day - 1);
            startTime = startTime.AddYears(Date_Box.Year - 1);
            startTime = startTime.AddMonths(Date_Box.Month - 1);
            startTime = startTime.AddDays(Date_Box.Day - 1);
            if ((Date_Box - startTime).TotalMinutes >= 0 && (dateTime - endTime).TotalMinutes <= 0)
            {
                foreach (var service in App.db.OrderedServices)
                {

                    dateTimeFromService = service.DayReserv;
                    dateTimeFromService = dateTimeFromService.AddMinutes(SelectedService.DurationInMinutes);
                    if ((Date_Box > service.DayReserv && Date_Box >= dateTimeFromService) ||
                        (dateTime <= service.DayReserv && dateTime < dateTimeFromService))
                    {
                    }
                    else
                    {
                        flag = false;
                        Date_Box = oldDate;

                        break;
                    }
                }
                //DateTime? selectedDate = Date_Box.SelectedDate;
            }
            else
            {
                MessageBox.Show("В выбранное вами время парикмахерская не работает!\nВыберите другое время!");
                Date_Box = oldDate;

                return;
            }
            if (Date_Box > DateTime.Now && flag)
            {
                try
                {
                    OrderedService orderedService = new OrderedService
                    {
                        DayReserv = Date_Box,
                        Login = logins,
                        ServiceId = SelectedService.Id

                    
                    };
                    App.db.OrderedServices.Add(orderedService);
                    App.db.SaveChanges();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                finally
                {
                    Date_Box = oldDate;

                    WindowClient winadm = new WindowClient(logins);
                    MessageBox.Show("Запись проведена успешно!!");
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
                MessageBox.Show("На это время уже забронировано место!\nВыберите другой вариант!");
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
