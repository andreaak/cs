﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest._01_Elements_CS._05_Constants
{
    // Константа (Constant) - это область памяти, которая хранит в себе некоторое значение, которое нельзя изменить.
    class _01_Constants
    {
        const int x = 5;
        
        public void Show()
        {
            const int y = 0;
            // Попытка присвоения константе нового значения, приводит к ошибке уровня компиляции!

            //y = 25555; 
        }
    }
}