﻿namespace ASPWebFormsTest._01_BaseInfo
{
    public class Person
    {
        public string Login { get; set; }
        public string Email { get; set; }

        public Person(string login, string email)
        {
            Login = login;
            Email = email;
        }

        public string GenerateHtml()
        {
            return string.Format("Login: {0}<br />Email: {1}", Login, Email);
        }
    }
}