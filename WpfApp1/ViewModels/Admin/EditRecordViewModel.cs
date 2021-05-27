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


namespace WpfApp1.ViewModels
{
    public class EditRecordViewModel : BaseViewModel
    {

        public ICommand Save_Service { get; private set; }
        public ICommand GoBack_EditView { get; private set; }

        Service Service;
        public EditRecordViewModel(Service init)
        {
            this.Service = init;
            Title_Box = init.Title;
            Cost_Box = init.Costedit;
            Time_Box = init.DurationInSecondsEdit;
            Skidka_Box = init.DiscountEdit;
            Order_Date = init.OrderDateEdit;
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

        private string order_date;
        //private DateTime selectedDate;

        public string Order_Date
        {
            get { return order_date; }
            set { order_date = value; }
        }

        public int Id { get; private set; }



        #endregion


        private void record_save(object obj)
        {

            
            if (Date_Box > DateTime.Now)
            {
                try
                {
                    var entity = App.db.OrderedServices.FirstOrDefault(x => x.Id == Id);
                    entity.DayReserv = date_box.Date.ToShortDateString();
                    entity.Id = Service.Id;
                    //App.db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    App.db.SaveChangesAsync().GetAwaiter();
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
