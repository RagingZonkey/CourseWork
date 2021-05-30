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

            
            var entity = App.db.OrderedServices.SingleOrDefault();
            
            OrderedServices.Add(new OrderedService
            {
                Id = int.Parse(entity.Id.ToString()),
                Title = entity.Title.ToString(),
                Cost = "Итоговая стоимость - " + float.Parse(entity.Cost.ToString()).ToString() + " рублей",
                DurationInMinutes = "Оплаченное время работы мастера - " + int.Parse(entity.DurationInMinutes.ToString()).ToString() + " мин",
                MainImagePath = entity.MainImagePath.ToString()
            });

            
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
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данную запись?", "Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                //DB db = new DB();
                //SqlCommand command = new SqlCommand("DELETE FROM OrderService WHERE ID = @id", db.getConnection());
                //command.Parameters.AddWithValue("@id", s.ID);
                //db.openConnection();
                try 
                {
                    App.db.OrderedServices.Remove(s);
                    App.db.SaveChangesAsync().GetAwaiter();
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show("Услуга успешно удалена!", "Ok");
                    WindowRecord record = new WindowRecord();
                    foreach (Window win in Application.Current.Windows)
                    {
                        if (win is WindowRecord)
                        {
                            win.Close();
                        }
                    }
                    record.Show();
                }
               
            }
            else
            {

            }
        }

        private void go_edit(object sender)
        {
            if (SelectedService != null) // Магия / не трогать
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
