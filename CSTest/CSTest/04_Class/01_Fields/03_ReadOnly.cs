﻿using System.Diagnostics;
using CSTest._03_Structure._0_Setup;

namespace CSTest._04_Class._01_Fields
{
    class _03_ReadOnly
    {
        public static readonly int fieldStatic = Init();
        static _03_ReadOnly()
        {
            fieldStatic = 7;
        }

        public int fieldInt = 0;

        // Поле только для чтения
        public readonly string readonlyField = "hello";

        public readonly TestClass fieldClass = new TestClass();

        public readonly int readOnlyInt = Init();

        private static int Init()
        {
            return 8;
        }

        // Конструктор.
        public _03_ReadOnly()
        {
            Debug.WriteLine(readonlyField);
            readonlyField = "Поле только для чтения ";
            readonlyField += "!";
        }

        public _03_ReadOnly(int i)
            : this()
        {
            readonlyField = "Поле только для чтения ";

            readonlyField += "!";
        }
    }
}
