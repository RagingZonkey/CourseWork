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
        #region Properties

        private DateTime date_box;

        public DateTime Date_Box
        {
            get { return date_box; }
            set { date_box = value; }
        }

        //    public SecureString firstPassword = new SecureString();
        //    public SecureString FirstPassword
        //    {
        //        get { return firstPassword; }
        //        set
        //        {
        //            firstPassword = value;
        //            OnPropertyChanged("FirstPassword");
        //        }
        //    }
        //    public SecureString secondPassword = new SecureString();
        //    public SecureString SecondPassword
        //    {
        //        get { return secondPassword; }
        //        set
        //        {
        //            secondPassword = value;
        //            OnPropertyChanged("SecondPassword");
        //        }
        //    }
        //    private string passwordValidation;

        //    public string PasswordValidation
        //    {
        //        get { return passwordValidation; }
        //        set
        //        {
        //            passwordValidation = value;
        //            OnPropertyChanged("PasswordValidation");
        //        }
        //    }

        //    private string secondPasswordValidation;

        //    public string SecondPasswordValidation
        //    {
        //        get { return secondPasswordValidation; }
        //        set
        //        {
        //            secondPasswordValidation = value;
        //            OnPropertyChanged("SecondPasswordValidation");
        //        }
        //    }

        //    private string secondNameValidation;

        //    public string SecondNameValidation
        //    {
        //        get { return secondNameValidation; }
        //        set
        //        {
        //            secondNameValidation = value;
        //            OnPropertyChanged("SecondNameValidation");
        //        }
        //    }

        //    private string nameValidation;

        //    public string NameValidation
        //    {
        //        get { return nameValidation; }
        //        set
        //        {
        //            nameValidation = value;
        //            OnPropertyChanged("NameValidation");
        //        }
        //    }

        //    private string emailValidation;

        //    public string EmailValidation
        //    {
        //        get { return emailValidation; }
        //        set
        //        {
        //            emailValidation = value;
        //            OnPropertyChanged("EmailValidation");
        //        }
        //    }
        private string name_box;

        public string Name_Box
        {
            get { return name_box; }
            set { name_box = value; }
        }

        private string surname_box;

        public string Surname_Box
        {
            get { return surname_box; }
            set { surname_box = value; }
        }

        private string email_box;

        public string Email_Box
        {
            get { return email_box; }
            set { email_box = value; }
        }

        private string firstPasswordBox;

        public string FirstPasswordBox
        {
            get { return firstPasswordBox; }
            set { firstPasswordBox = value; }
        }

        private string secondPasswordBox;

        public string SecondPasswordBox
        {
            get { return secondPasswordBox; }
            set { secondPasswordBox = value; }
        }



        #endregion
        #region MainFunctionality

        //private void OpenSendMessage(object obj)
        //{
        //    bool flag = true;
        //    Regex complexPassword = new Regex("(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}");
        //    IntPtr password1 = default(IntPtr);
        //    IntPtr password2 = default(IntPtr);
        //    string insecurePassword1 = "";
        //    string insecurePassword2 = "";
        //    string hash = "";
        //    try
        //    {
        //        if (App.db.USERS.FirstOrDefault(n => n.EMAIL == Email) != null)
        //        {
        //            App.notifier.ShowWarning("Пользователь с таким Email уже существует");
        //            return;
        //        }

        //        password1 = Marshal.SecureStringToBSTR(FirstPassword);
        //        insecurePassword1 = Marshal.PtrToStringBSTR(password1);
        //        password2 = Marshal.SecureStringToBSTR(SecondPassword);
        //        insecurePassword2 = Marshal.PtrToStringBSTR(password2);
        //        sh = new SaltedHash(insecurePassword1);
        //        if (!complexPassword.IsMatch(insecurePassword1))
        //        {
        //            PasswordValidation = "Пароль должен быть в длину более 6 символов, содержать цифры, спец символы, латинские буквы в верхнем и нижнем регистре";
        //            flag = false;
        //        }
        //        else
        //        {
        //            PasswordValidation = "";
        //        }

        //        if (insecurePassword1 != insecurePassword2)
        //        {
        //            SecondPasswordValidation = "Введенные пароли должны совпадать";
        //            flag = false;
        //        }
        //        else
        //            SecondPasswordValidation = "";

        //        if (Email == null || Email == "")
        //        {
        //            EmailValidation = "Это поле не должно быть пустым";
        //            flag = false;
        //        }
        //        else
        //        {
        //            EmailValidation = "";
        //        }

        //        if (SecondName == null || SecondName == "")
        //        {
        //            SecondNameValidation = "Это поле не должно быть пустым";
        //            flag = false;
        //        }
        //        else
        //        {
        //            SecondNameValidation = "";
        //        }

        //        if (Name == null || Name == "")
        //        {
        //            NameValidation = "Это поле не должно быть пустым";
        //            flag = false;
        //        }
        //        else
        //        {
        //            NameValidation = "";
        //        }

        //        if (!flag) return;

        //        if (insecurePassword1 == insecurePassword2 && flag)
        //        {
        //            hash += sh.Hash + sh.Salt;
        //            SecondPasswordValidation = "";
        //        }

        //        insecurePassword1 = "";
        //        insecurePassword2 = "";
        //        if (FirstPassword != null)
        //        {
        //            FirstPassword.Dispose();
        //        }
        //        if (SecondPassword != null)
        //        {
        //            SecondPassword.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //        insecurePassword1 = "";
        //        insecurePassword2 = "";
        //        if (FirstPassword != null)
        //        {
        //            FirstPassword.Dispose();
        //        }
        //        if (SecondPassword != null)
        //        {
        //            SecondPassword.Dispose();
        //        }
        //    }
        //    MainWindowViewModelSingleton.GetInstance().MainFrameViewModel.SelectedViewModel = new SendMessageViewModel(new USERS { ACCOUNT = "Пользователь", EMAIL = Email, NAME = Name, PASSWORD = hash, SECOND_NAME = SecondName });

        //}


        #endregion

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
            /// <summary>
            /// Свойства, в которые передаются значения для полей из окна
            /// </summary>
            string name = Name_Box.Trim();
            string lastname = Surname_Box.Trim();
            string birthday = Date_Box.ToShortDateString();
            string email = Email_Box.Trim();
            string passwordag = FirstPasswordBox.Trim();
            string passwordag2 = SecondPasswordBox.Trim();


            


            /// <summary>
            /// Валидация всех ЭУ в окне
            /// </summary>
            try
            {


                if (name == null || name == "")
                {
                    MessageBox.Show("Поле \"Имя\" не должно быть пустым!");
                    if (lastname == null || lastname == "")
                    {
                        MessageBox.Show("Поле \"Фамилия\" не должно быть пустым!");
                        if (birthday == null)
                        {
                            MessageBox.Show("Выбор Даты Рождения обязателен для завершения процесса регистрации!");
                            if (email == null || email == "")
                            {
                                MessageBox.Show("Поле \"Email\" не должно быть пустым!");
                                if (passwordag == null || passwordag == "")
                                {
                                    MessageBox.Show("Поле пароля не должно быть пустым!");
                                    if (passwordag2 == null || passwordag2 == "")
                                    {
                                        MessageBox.Show("Повторите введенный пароль для проверки!");
                                    }
                                }
                            }
                        }
                    }
                    return;
                }
                else
                {
                    if (lastname == null || lastname == "")
                    {
                        MessageBox.Show("Поле \"Фамилия\" не должно быть пустым!");
                        return;
                    }
                    if (birthday == null)
                    {
                        MessageBox.Show("Выбор Даты Рождения обязателен для завершения процесса регистрации!");
                        return;
                    }
                    if (email == null || email == "")
                    {
                        MessageBox.Show("Поле \"Email\" не должно быть пустым!");
                        return;
                    }
                    if (passwordag == null || passwordag == "")
                    {
                        MessageBox.Show("Поле пароля не должно быть пустым!");
                        return;
                    }
                    if (passwordag2 == null || passwordag2 == "")
                    { 
                        MessageBox.Show("Повторите введенный пароль для проверки!");
                        return;
                    }
                }


                App.db.Clients.Add(new Model.Client { FirstName = name, LastName = lastname, Birthday = birthday, Email = email, Password = passwordag, Role = 0  });
                App.db.SaveChangesAsync().GetAwaiter();
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

    }
}
