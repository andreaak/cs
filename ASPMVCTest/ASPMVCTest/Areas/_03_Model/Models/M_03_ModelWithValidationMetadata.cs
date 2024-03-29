﻿using System.ComponentModel.DataAnnotations;

namespace _01_ASPMVCTest.Areas._03_Model.Models
{
    public class M_03_ModelWithValidationMetadata
    {
        // Required - поле обязательное для заполнения
        // StringLength - допустимая длина слова для поля ввода
        // RegularExpression - регулярное выражение для проверки введенных данных
        // Range - диапазон допустимых значений для поля ввода
        // Compare - значения свойств должны иметь одинаковые значения

        [Required(ErrorMessage = "Вы не ввели логин")]
        [StringLength(10, ErrorMessage = "Логин не может привышать 10 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле пароль обязательно для заполнения")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Следует указать пароль от 5 до 20 символов")]
        [System.Web.Mvc.Compare("PasswordConfirm", ErrorMessage = "Пароли не совпадают")]
        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Поле Email обязательно для заполнения")]
        [RegularExpression(@"(?i)\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", ErrorMessage = "Email адрес указан не правильно")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле Age обязательно для заполнения")]
        [Range(18, 60, ErrorMessage = "Значение поля возраст должно попадать в диапазон от 18 до 60")]
        public int Age { get; set; }
    }

    public class M_03_ModelWithCustomValidationAttribute : M_03_ModelWithValidationMetadata
    {
        [MustBeTrue(ErrorMessage = "Вы не согласились с условиями использования")]
        public bool TermsAccepted { get; set; }
    }
}