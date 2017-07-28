using System;
using System.Security.Cryptography;
using System.Text;

namespace ASPWebFormsTest._03_StateSaving._06_AuthenticationSample
{
    public static class CryptoProvider
    {
        /// <summary>
        /// Возвращает хэш для указанной строки
        /// </summary>
        /// <param name="p">Строка, для которой нужно вычислить хэш</param>
        /// <returns>Хэш</returns>
        public static string GetMD5Hash(string p)
        {
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] hashCode = md5Provider.ComputeHash(Encoding.Default.GetBytes(p));
            return BitConverter.ToString(hashCode).ToLower().Replace("-", "");
        }
    }
}