namespace _01_ASPMVCTest.Areas._03_Model.Models
{
    public class M_03_TestModel
    {
        public string Login { get; set; } // обязательное значение
        public string Password { get; set; } // Password и PasswordConfirm должны совпадать
        public string PasswordConfirm { get; set; }
        public string Email { get; set; } // должно быть валидным email адресом
    }
}