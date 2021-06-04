using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.ViewModels.Base;
using WpfApp1.Model;
using System.Text.RegularExpressions;

namespace WpfApp1.ViewModels.Client
{
    public class RegisterViewModel : BaseViewModel
    {
        public ICommand RegisterButton { get; private set; }
        public ICommand LoginButton { get; private set; }
        public RegisterViewModel()
        {
            RegisterButton = new RelayCommand(on_register);
            LoginButton = new RelayCommand(on_login);
        }


        private string email_box;

        public string Email_Box
        {
            get { return email_box; }
            set { email_box = value;
                OnPropertyChanged("Email_Box");
            }
        }

        private string firstPasswordBox;

        public string FirstPasswordBox
        {
            get { return firstPasswordBox; }
            set { firstPasswordBox = value;
                OnPropertyChanged("FirstPassword_Box");
            }
        }

        private string secondPasswordBox;

        public string SecondPasswordBox
        {
            get { return secondPasswordBox; }
            set { secondPasswordBox = value;
                OnPropertyChanged("SecondPassword_Box");
            }
        }



       

        private void on_login(object sender)
        {
            Login log = new Login();
            log.Show();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is Register)
                {
                    win.Close();
                }
            }
        }

        private void on_register(object sender)
        {
            //bool flag = true;
            Regex complexPassword = new Regex("(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}");
            Regex complexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            
            /// <summary>
            /// Свойства, в которые передаются значения для полей из окна
            /// </summary>
            Email_Box = Email_Box.Trim();
            FirstPasswordBox = FirstPasswordBox.Trim();
            SecondPasswordBox = SecondPasswordBox.Trim();
            if (!complexPassword.IsMatch(FirstPasswordBox))
            {
                MessageBox.Show("Пароль должен быть в длину более 6 символов, содержать цифры, спец символы, латинские буквы в верхнем и нижнем регистре");
                FirstPasswordBox = null;
                return;
            }
            if (!complexEmail.IsMatch(Email_Box))
            {
                MessageBox.Show("Введите настоящий E-mail адрес!");
                Email_Box = null;
                return;
            }


            if (Email_Box == null || FirstPasswordBox == null || SecondPasswordBox == null)
            {
                MessageBox.Show("Заполните все поля!");


            }
            else
            {
                if (FirstPasswordBox == SecondPasswordBox)
                {
                    /// <summary>
                    /// Валидация всех ЭУ в окне
                    /// </summary>
                    try
                    {

                        App.db.Clients.Add(new Model.Client { Email = Email_Box, Password = FirstPasswordBox, Role = 0 });
                        App.db.SaveChanges();
                    }
                    catch
                    {

                    }
                    finally
                    {
                        Login login = new Login();
                        foreach (Window win in Application.Current.Windows)
                        {
                            if (win is Register)
                            {
                                win.Close();
                            }
                        }
                        login.Show();
                    }

                }
                else
                {
                    MessageBox.Show("Пароли должны совпадать!");
                }
            }
        }

    }
}
