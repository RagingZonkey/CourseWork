using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.view;
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels.Client
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand Login { get; private set; }
        public ICommand Register { get; private set; }

        private string email_box;

        public string Email_Box
        {
            get { return email_box; }
            set { email_box = value; }
        }

        private string password_box;

        public string Password_Box
        {
            get { return password_box; }
            set { password_box = value; }
        }


        public LoginViewModel()
        {
            Login = new RelayCommand(Button_Click);
            Register = new RelayCommand(Button_Click_1);
        }

        private void Button_Click(object sender)
        {
            
            if (Email_Box != null && Password_Box != null)
            {
                string login = Email_Box.Trim();
                string password = Password_Box.Trim();
                try
                {
                    //Model.Client entity = null;
                    var entity = App.db.Clients.Where(x => x.Email == login && x.Password == password || x.Email == login).SingleOrDefault();

                    if (entity != null)
                    {
                        if (entity.Password == password)
                        {


                            if (entity.Role == 0)
                            {

                                WindowService client = new WindowService(login);
                                foreach (Window win in Application.Current.Windows)
                                {
                                    if (win is Login)
                                    {
                                        win.Close();
                                    }
                                }
                                client.Show();
                            }


                            else
                            {
                                //Model.Client adminentity = null;

                                WindowAdmin windowCompany = new WindowAdmin();
                                foreach (Window win in Application.Current.Windows)
                                {
                                    if (win is Login)
                                    {
                                        win.Close();
                                    }
                                }
                                windowCompany.Show();

                            }
                        }
                        else
                        {
                            MessageBox.Show("Проверьте правильность введенного пароля!");
                        }


                    }
                    else
                    {
                        MessageBox.Show($"Пользователя с логином \"{login}\" не существует!");
                    }
                }




                catch (SqlException)
                {
                    MessageBox.Show("Отсутствует подключение с базой данных");
                }
            }
            else
            {
                MessageBox.Show("Для начала заполните поля!");
            }

        }
    

        private void Button_Click_1(object sender)
        {
            Register reg = new Register();
            reg.Show();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is Login)
                {
                    win.Close();
                }
            }
        }

    }

    



}
