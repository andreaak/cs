﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _01_ASPMVCTest.Areas._003_Model.Models
{
    public class AccountModel
    {
        public string Login { get; set; } // обязательное значение
        public string Password { get; set; } // Password и PasswordConfirm должны совпадать
        public string PasswordConfirm { get; set; }
        public string Email { get; set; } // должно быть валидным email адресом

    }
}