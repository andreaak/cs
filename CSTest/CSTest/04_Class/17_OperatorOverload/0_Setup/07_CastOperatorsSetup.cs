using System.Diagnostics;

namespace CSTest._04_Class._17_OperatorOverload._0_Setup
{
    class _07_CastOperatorsSetup
    {
        int x, y, z; // трехмерные координаты
         
        public _07_CastOperatorsSetup()
        {
            x = y = z = 0;
        }

        public _07_CastOperatorsSetup(int i, int j, int k)
        {
            x = i;
            y = j;
            z = k;
        }

        ///////////////////////////////
        // Выполнить на этот раз явное преобразование типов. 
        public static explicit operator short(_07_CastOperatorsSetup opl)
        {
            return (short)(opl.x * opl.y * opl.z);
        }

        ///////////////////////////////
        // Неявное преобразование объекта типа _07_CastOperatorsSetup к типу long. 
        public static implicit operator int(_07_CastOperatorsSetup opl)
        {
            return opl.x * opl.y * opl.z;
        }

        // Неявное преобразование объекта типа _07_CastOperatorsSetup к типу double. 
        public static implicit operator double(_07_CastOperatorsSetup opl)
        {
            return opl.x * opl.y * opl.z;
        }

        // Вывести координаты X, Y, Z. 
        public void Show()
        {
            Debug.WriteLine(x + ", " + y + ", " + z);
        }
    }
}
