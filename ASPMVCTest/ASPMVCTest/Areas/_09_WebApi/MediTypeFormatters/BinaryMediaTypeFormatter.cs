using System;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace _01_ASPMVCTest.Areas._09_WebApi.MediTypeFormatters
{
    // Пользовательский форматтер.
    // http://byterot.blogspot.com/2012/04/aspnet-web-api-series-part-5.html

    public class BinaryMediaTypeFormatter : MediaTypeFormatter
    {
        // Тип который умеет конвертировать данный форматер.
        private static Type _supportedType = typeof(byte[]);
        private const int BufferSize = 8192; // 8K

        public BinaryMediaTypeFormatter()
        {
            // указание на какое значения заголовка Content-Type будет реагировать данный форматер.
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
        }

        public override bool CanReadType(Type type)
        {
            return type == _supportedType;
        }

        public override bool CanWriteType(Type type)
        {
            return type == _supportedType;
        }

        // Данынй код является синхронным так как расчитан на конвертирование небольших объемов данных.
        // Метод запускается при получении от клиента запроса с MIME типом application/octet-stream
        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
        {
            var taskSource = new TaskCompletionSource<object>();
            try
            {
                var ms = new MemoryStream();
                readStream.CopyTo(ms, BufferSize);
                taskSource.SetResult(ms.ToArray());
            }
            catch (Exception e)
            {
                taskSource.SetException(e);
            }
            return taskSource.Task;
        }

        // Метод будет вызван если при отправке ответа клиенту будет явно указан этот объект.
        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, System.Net.Http.HttpContent content, TransportContext transportContext)
        {
            var taskSource = new TaskCompletionSource<object>();
            try
            {
                if (value == null)
                {
                    value = new byte[0];
                }
                var ms = new MemoryStream((byte[])value);
                ms.CopyTo(writeStream);
                taskSource.SetResult(null);
            }
            catch (Exception e)
            {
                taskSource.SetException(e);
            }
            return taskSource.Task;
        }
    }
}