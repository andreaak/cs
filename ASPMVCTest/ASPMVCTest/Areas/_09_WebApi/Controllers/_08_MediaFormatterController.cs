using System;
using System.Security.Cryptography;
using System.Web.Http;

namespace _01_ASPMVCTest.Areas._09_WebApi.Controllers
{
    public class _08_MediaFormatterController : ApiController
    {
        // Данный метод принимает на вход массив и возвращает хэш сумму для массива.
        // При отправке следующего запроса сервер должен вернуть sha1 hash для слова Hello

        // Попробуйте отправить следующий запрос с помощью Fiddler

        // POST http://localhost:37031/api/binary HTTP/1.1
        // Host: localhost:37031
        // User-Agent: Fiddler
        // Content-type: application/octet-stream
        // Content-Length: 5
        //
        // Hello

        // Но для media типа application/octet-stream (двоичный файл без указания формата)
        // нет стандартного форматтера, поэтому параметр data будет null
        // Для того что бы преобразовать входящие данные и данные которые будут отправлены в ответ в этом проекте
        // добавлен тип BinaryMediaTypeFormatter и зарегистрирован в файле WebApiConfig

        [HttpPost]
        public string CalculateHash(byte[] data)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                return Convert.ToBase64String(sha1.ComputeHash(data));
            }
        }
    }
}
