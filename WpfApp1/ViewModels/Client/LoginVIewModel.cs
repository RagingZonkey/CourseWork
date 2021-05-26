using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.view;
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels.Client
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand Login { get; private set; }
        public ICommand Register { get; private set; }


        public LoginViewModel()
        {

        }

        private void Button_Click(object sender)
        {
            string login = Email_box.Text.Trim();
            string password = password_box.Password.Trim();

            try
            {
                //DB db = new DB();
                //DataTable table = new DataTable();
                //SqlDataAdapter adapter = new SqlDataAdapter();
                //SqlCommand command = new SqlCommand("SELECT * FROM Client WHERE Email= @uL AND Password = @uP", db.getConnection());
                //db.openConnection();
                var entity = App.db.Clients.Where(x => x.Email == ).SingleOrDefault();

                command.Parameters.Add("@uL", SqlDbType.VarChar).Value = login;
                command.Parameters.Add("@uP", SqlDbType.VarChar).Value = password;
                
                adapter.Fill(table);

               
                    
            }
            catch (SqlException)
            {
                MessageBox.Show("Отсутствует подключение с базой данных");
            }
            finally
            {
                if (CheckRole())
                {

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
                else
                {

                    WindowClient client = new WindowClient(login);
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
                    MessageBox.Show("Неправильный логин или пароль");
                }
            }
        }


        private bool CheckRole()
        {
            DB db = new DB();

            String loginUser = Email_box;

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM Client WHERE Email = @email and Role = 1", db.getConnection());
            command.Parameters.Add("@email", SqlDbType.VarChar).Value = loginUser;
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
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
