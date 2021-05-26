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

        Service s;
        private Service selectedService;

        public Service SelectedService
        {
            get { return selectedService; }
            set
            {
                selectedService = value;
                OnPropertyChanged("SelectedService");
            }
        }

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
        public WindowRecordViewModel()
        {
            Edit = new RelayCommand(go_edit);
            Delete = new RelayCommand(go_delete);
            AdminWindow = new RelayCommand(go_admwin);
            Exit = new RelayCommand(go_exit);
            Change = new RelayCommand(go_change);

            //DataTable table = new DataTable();
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //SqlCommand command = new SqlCommand("SELECT * FROM OrderService", db.getConnection());
            //db.openConnection();
            //SqlDataReader reader = command.ExecuteReader();

            //Services = new ObservableCollection<Service> { };
            //while (reader.Read())
            //{
            //    Services.Add(new Service
            //    {
            //        ID = int.Parse(reader[0].ToString()),
            //        Title = reader[1].ToString(),
            //        Cost = "Итоговая стоимость - " + float.Parse(reader[3].ToString()).ToString() + " рублей",
            //        Costedit = float.Parse(reader[3].ToString()).ToString(),
            //        DurationInSeconds = "Оплаченное время работы мастера - " + int.Parse(reader[4].ToString()).ToString() + " мин",
            //        DurationInSecondsEdit = int.Parse(reader[4].ToString()).ToString(),
            //        Discount = "Скидка - " + float.Parse(reader[5].ToString()).ToString() + "%",
            //        DiscountEdit = float.Parse(reader[5].ToString()).ToString(),
            //        OrderDate = "Дата заказа - " + reader[6].ToString(),
            //        OrderDateEdit = reader[6].ToString(),
            //        ReservDay = "Дата записи - " + reader[7].ToString(),
            //        MainImagePath = reader[9].ToString()
            //    });

            //}
            //recordlist.ItemsSource = Services;
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
                    App.db.Services.Remove(s);
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
