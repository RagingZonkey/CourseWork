using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class User
    {
        public string Id { get; set; }
        public string Email;
        public string Password;

        public string login
        {
            get
            {
                return Email;
            }
            set { Email = value; }
        }

        public User () { }
        public User(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
    }
}
