﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest._04_Class._05_Properties
{

    /*
    Автоматически реализуемые свойства (Auto-Implemented properties). 

    Автоматически реализуемые свойства это более лаконичная форма свойств, их есть смысл использовать,
    когда в методах доступа (get и set) не требуется дополнительная логика. 
    При создании автоматически реализуемых свойств, компилятор создаст закрытое, анонимное резервное поле, 
    которое будет доступно с помощью методов get и set свойства.
    */
    class _03_AutoProperties
    {
        // Автоматически реализуемые свойства. 
        public string Name { get; set; }
        public string Book { get; set; }
    }
}