using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._02_View.Models
{
    public class V_04_ModelWithMetaData
    {
        // Указывает, что свойство должно быть отображено как <input type="hidden"> + текстовый литерал.
        //[HiddenInput]  

        // Указывает, что свойство должно быть отображено только как <input type="hidden">.
        [HiddenInput(DisplayValue = false)]
        public int Id
        {
            get;
            set;
        }

        // задает значение элемента label при использовании метода LabelFor.
        [Display(Name = "Имя")]
        public string FirstName
        {
            get;
            set;
        }

        [Display(Name = "Фамилия")]
        public string LastName
        {
            get;
            set;
        }

        // Инструкция для отображения значения свойства модели. 
        // Теперь выводиться дата без времени.
        [DataType(DataType.Date)]
        public DateTime RegistrationDate
        {
            get;
            set;
        }

        public bool IsApproved
        {
            get;
            set;
        }

        // свойство не отображается при использовании шаблонизированных методов
        // Исключает свойство из генерируемой HTML разметки.
        [ScaffoldColumn(false)]
        public int Foo
        {
            get;
            set;
        }

        // Выбор шаблона отображения
        [UIHint("MultilineText")]
        public string Description
        {
            get;
            set;
        }
    }
}