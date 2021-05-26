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
    public class WindowAdminServiceViewModel : BaseViewModel
    {
        public ICommand Edit { get; private set; }
        public ICommand Delete { get; private set; }
        public ICommand Add { get; private set; }
        public ICommand Main { get; private set; }
        public ICommand Exit { get; private set; }
        public ICommand Change { get; private set; }

        Service s;
        public WindowAdminServiceViewModel()
        {
            Edit = new RelayCommand(go_edit);
            Delete = new RelayCommand(go_delete);
            Add = new RelayCommand(go_add);
            Main = new RelayCommand(go_main);
            Exit = new RelayCommand(go_exit);
            Change = new RelayCommand(go_change);

            var entity = App.db.Services.SingleOrDefault();
            
            Services.Add(new Service
            {
                Id = int.Parse(entity.Id.ToString()),
                Title = entity.Title.ToString(),
                Cost = float.Parse(entity.Cost.ToString()).ToString() + " рублей за "
                + int.Parse(entity.DurationInSeconds)/60 + " мин",
                Costedit = float.Parse(entity.Costedit.ToString()).ToString(),
                DurationInSeconds = int.Parse(reader[3].ToString()).ToString(),
                Discount = float.Parse(reader[4].ToString()).ToString() + "% скидка",
                DiscountEdit = float.Parse(reader[4].ToString()).ToString(),
                MainImagePath = reader[5].ToString()
            });


        }
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



        



        
        

        private void go_edit(object sender)
        {
            if (SelectedService != null) // Магия / не трогать
            {
                Edit ed = new Edit(SelectedService);
                ed.Show();
                foreach (Window win in Application.Current.Windows)
                {
                    if (win is WindowAdminService)
                    {
                        win.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите услугу!", "Error");
            }
        }

        private void go_delete(object sender)
        {
            s = SelectedService;
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данную услугу?", "Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    App.db.Services.Remove(s);
                    App.db.SaveChangesAsync().GetAwaiter();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show("Услуга успешно удалена!", "Ok");
                    WindowAdminService adm = new WindowAdminService();
                    
                    adm.Show();
                }

            }
            else
            {

            }
        }

        private void go_add(object sender)
        {
            Add add = new Add();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowAdminService)
                {
                    win.Close();
                }
            }
            add.Show();


        }
        private void go_main(object sender)
        {
            WindowAdmin rec = new WindowAdmin();
            rec.Show();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is Add)
                {
                    win.Close();
                }
            }
        }
        private void go_exit(object sender)
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowAdminService)
                {
                    win.Close();
                }
            }
        }

        private void go_change(object sender)
        {
            Login login = new Login();
            login.Show();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is WindowAdminService)
                {
                    win.Close();
                }
            }
        }

    }
}
