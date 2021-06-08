using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Context;
using WpfApp1.Model;
using WpfApp1.view.Admin;
using WpfApp1.view.Admin.Buttons;
using WpfApp1.ViewModels.Base;
using Microsoft.Win32;
using WpfApp1.Commands;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using WpfApp1.ViewModels.Client;

namespace WpfApp1.ViewModels
{
    public class EditRecordViewModel : BaseViewModel
    {

        public ICommand Save_Service { get; private set; }
        public ICommand GoBack_EditView { get; private set; }

        OrderedService OrderedService;
        public EditRecordViewModel(OrderedService init)
        {
            this.OrderedService = init;
            ID = init.Id;
            Login = init.Login;
            Title = init.Title;
            Services = new ObservableCollection<Service>(App.db.Services);

            Save_Service = new RelayCommand(record_save);
            GoBack_EditView = new RelayCommand(go_back);
            
        }



        #region Fields and Properties


        private ObservableCollection<Service> services;

        public ObservableCollection<Service> Services
        {
            get { return services; }
            set
            {
                services = value;
                OnPropertyChanged("Services");
            }
        }

        private DateTime date_box;

        public DateTime Date_Box
        {
            get { return date_box; }
            set { date_box = value; }
        }


        private string login;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }


        private string selectedTime;
       

        public string SelectedTime
        {
            get { return selectedTime; }
            set
            {
                selectedTime = value;
                OnPropertyChanged("SelectedTime");
            }
        }

        //private ObservableCollection<Service> services;

        //public ObservableCollection<Service> Services
        //{
        //    get { return services; }
        //    set
        //    {
        //        services = value;
        //        OnPropertyChanged("Services");
        //    }
        //}




        public int ID { get; private set; }



        #endregion


        private void record_save(object obj)
        {
            
                if (SelectedTime != null)
                {
                    DateTime oldDate = Date_Box;
                    DateTime endTime = new DateTime(1, 1, 1, 20, 0, 0);
                    DateTime startTime = new DateTime(1, 1, 1, 8, 0, 0);


                    Regex timeValidation = new Regex("^(0[0-9]|1[0-9]|2[0-3]|[0-9]):[0-5][0-9]$");
                    if (!timeValidation.IsMatch(SelectedTime))
                    {
                        System.Windows.Forms.MessageBox.Show("Вы вышли за пределы формата времени!");
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
                    if (OrderedService != null)
                    {
                        duration = OrderedService.DurationInMinutes;
                    }
                    else flag = false;
                    //dateTime = Date_Box.AddMinutes(duration);
                    dateTime = dateTime.AddMinutes(duration);
                    endTime = endTime.AddYears(Date_Box.Year - 1);
                    endTime = endTime.AddMonths(Date_Box.Month - 1);
                    endTime = endTime.AddDays(Date_Box.Day - 1);
                    startTime = startTime.AddYears(Date_Box.Year - 1);
                    startTime = startTime.AddMonths(Date_Box.Month - 1);
                    startTime = startTime.AddDays(Date_Box.Day - 1);
                    if ((Date_Box - startTime).TotalMinutes >= 0 && (dateTime - endTime).TotalMinutes <= 0)
                    {
                        foreach (var service in App.db.OrderedServices)
                        {

                            dateTimeFromService = service.DayReserv;

                            int js = 0;
                            foreach (var sngj in Services)
                            {
                                if (sngj.Id == service.ServiceId)
                                {
                                js = sngj.DurationInMinutes;
                                break;
                                }
                            }

                            dateTimeFromService = dateTimeFromService.AddMinutes(js);
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
                            var entity = App.db.OrderedServices.FirstOrDefault(x => x.Id == ID);
                            entity.DayReserv = Date_Box;
                            App.db.SaveChanges();
                        }
                        catch { MessageBox.Show("На это время уже забронировано место!\nВыберите другой вариант!"); }
                        finally
                        {
                            WindowRecord winadm = new WindowRecord();
                            MessageBox.Show("Запись успешно отредактирована!");
                            foreach (Window win in Application.Current.Windows)
                            {
                                if (win is EditRecord)
                                {
                                    win.Close();
                                }
                            }
                            winadm.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данное время недоступно для записи! Выберите другой вариант! ");
                    }

                }
                else
                {
                    MessageBox.Show("Укажите дату и время, на которые хотите сделать заказ!");
                }
            

    
        }


            private void go_back(object obj)
        {
            WindowRecord winadm = new WindowRecord();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is EditRecord)
                {
                    win.Close();
                }
            }
            winadm.Show();
        }

    }
}
