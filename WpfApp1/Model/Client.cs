﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class Client
    {
        
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName {get; set;} 
        public string Patronymic { get; set; }
        public string Birthday { get; set; }
        public string RegistrationDate { get; set; } 
        public string Email { get; set;} 
        public string Phone { get; set; } 
        public string GenderCode { get; set; } 
        public string Password { get; set; } 
        public string Role { get; set; }

    }
}
