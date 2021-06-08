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
using WpfApp1.view.Admin;
using WpfApp1.view.Admin.Buttons;
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels
{
    public class WindowRecordViewModel : BaseViewModel
    {
        public ICommand Edit { get; private set; }
        public ICommand Delete { get; private set; }
        public ICommand AdminWindow { get; private set; }
        public ICommand Exit { get; private set; }
        public ICommand Change { get; private set; }

        OrderedService s;
        private OrderedService selectedService;

        public OrderedService SelectedService
        {
            get { return selectedService; }
            set
            {
                selectedService = value;
                OnPropertyChanged("SelectedService");
            }
        }

        private ObservableCollection<OrderedService> orderedServices;

        public ObservableCollection<OrderedService> OrderedServices
        {
            get { return orderedServices; }
            set
            {
                orderedServices = value;
                OnPropertyChanged("OrderedServices");
            }
        }
        public WindowRecordViewModel()
        {
            Edit = new RelayCommand(go_edit);
            Delete = new RelayCommand(go_delete);
            AdminWindow = new RelayCommand(go_admwin);
            Exit = new RelayCommand(go_exit);
            Change = new RelayCommand(go_change);


            OrderedServices = new ObservableCollection<OrderedService>(App.db.OrderedServices);
            foreach (var service in OrderedServices)
            {
                service.DurationInMinutes = App.db.Services.FirstOrDefault(n => n.Id == service.ServiceId).DurationInMinutes;
                service.Cost = App.db.Services.FirstOrDefault(n => n.Id == service.ServiceId).Cost;
                service.MainImagePath = App.db.Services.FirstOrDefault(n => n.Id == service.ServiceId).MainImagePath;
                service.Title = App.db.Services.FirstOrDefault(n => n.Id == service.ServiceId).Title;
                
            }


        }

       

        private void go_change(object sender)
        {
            Login login = new Login();
            login.Show();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowRecord)
                {
                    win.Close();
                }
            }
            
        }

        private void go_exit(object sender)
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowRecord)
                {
                    win.Close();
                }
            }
        }

        private void go_delete(object sender)
        {
            s = SelectedService;
            if (SelectedService != null) // Магия / не трогать
            {
                if (OrderedServices != null)
                {
                
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данную запись?", "Delete", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {

                        try
                        {
                            App.db.OrderedServices.Remove(s);
                            OrderedServices.Remove(s);
                            App.db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                        //finally
                        //{
                        //    MessageBox.Show("Заказ успешно отменен!", "Ok");
                        //    WindowAdmin adm = new WindowAdmin();
                        //    foreach (Window win in Application.Current.Windows)
                        //    {
                        //        if (win is WindowRecord)
                        //        {
                        //            win.Close();
                        //        }
                        //    }
                        //    adm.Show();
                        //}

                    }
                    else
                    {

                    }
                
                }
                else
                {
                MessageBox.Show("В базе данных нет ни одного заказа\n для начала выполните добавление!");
                }
            }
            else
            {
                MessageBox.Show("Выберите запись!", "Error");
            }
        }

        private void go_edit(object sender)
        {
            s = SelectedService;
            if (SelectedService != null) // Магия / не трогать
            {
                if (OrderedServices != null)
            {
                
                    EditRecord ed = new EditRecord(SelectedService);
                    ed.Show();
                    foreach (Window win in Application.Current.Windows)
                    {
                        if (win is WindowRecord)
                        {
                            win.Hide();
                        }
                    }
                
            }
            else
            {
                MessageBox.Show("В базе данных нет ни одного заказа\n для начала выполните добавление!");
            }
            }
            else
            {
                MessageBox.Show("Выберите запись!", "Error");
            }
        }

        private void go_admwin(object sender)
        {
            WindowAdmin adm = new WindowAdmin();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowRecord)
                {
                    win.Close();
                }
            }
            adm.Show();
        }
    }
}
