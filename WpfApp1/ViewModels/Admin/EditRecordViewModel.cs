﻿using System;
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
            
            Save_Service = new RelayCommand(record_save);
            GoBack_EditView = new RelayCommand(go_back);
            
        }


        #region Fields and Properties

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





        public int ID { get; private set; }



        #endregion


        private void record_save(object obj)
        {
            Regex timeValidation = new Regex("^(0[0-9]|1[0-9]|2[0-3]|[0-9]):[0-5][0-9]$");
            if (!timeValidation.IsMatch(SelectedTime))
            {
                System.Windows.Forms.MessageBox.Show("Дурак, цифры научись вводить");
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

            //if (Date_Box.Hour > startTime.Hour && dateTime.Hour < endTime.Hour)
            //{
            foreach (var service in App.db.OrderedServices)
            {

                dateTimeFromService = service.DayReserv;
                dateTimeFromService = dateTimeFromService.AddMinutes(OrderedService.DurationInMinutes);
                if ((Date_Box > service.DayReserv && Date_Box >= dateTimeFromService) ||
                    (dateTime <= service.DayReserv && dateTime < dateTimeFromService))
                {
                }
                else
                {
                    flag = false;
                    break;
                }
            }
            //DateTime? selectedDate = Date_Box.SelectedDate;
            //}
            //else
            //{
            //    MessageBox.Show("В выбранное вами время парикмахерская не работает!\nВыберите другое время!");
            //}
            if (Date_Box > DateTime.Now && flag)
            {
                try
                {
                    var entity = App.db.OrderedServices.FirstOrDefault(x => x.Id == ID);
                    entity.DayReserv = Date_Box;
                    App.db.SaveChanges();
                }
                catch { MessageBox.Show("Выберите правильную дату!", "Error"); }
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
                MessageBox.Show("Выберите правильную дату!", "Error");
                Date_Box = Date_Box.AddMinutes(-int.Parse(date[1]));
                Date_Box = Date_Box.AddHours(-int.Parse(date[0]));
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
