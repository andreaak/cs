﻿using System.Web;

namespace ASPWebFormsTest._16_Handlers
{
    // Для того что бы экземпляры данного класса могли использоваться для обработки входящих запросов,
    // необходимо зарегистрировать класс MyFirstHandler как обработчик в файле web.config
    public class _02_Handler : IHttpHandler
    {
        // Свойство используется для того, что бы определить может ли один и тот же экземпляр
        // класса HTTP обработчика использоваться для обработки нескольких запросов.
        // В большинстве случаев свойство возвращает значение false - на каждый запрос создается новый экземпляр 
        // обработчика, но в тех ситуациях, когда создание обработчика связанно с большими накладными расходами,
        // есть смысл указать, что обработчик может использоваться повторно, вернув значение true в данном свойстве.
        public bool IsReusable
        {
            get { return false; }
        }

        // Данный метод будет запущен когда поступит запрос адресованный текущему HTTP обработчику.
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write("Hello from HttpHandler");
        }
    }
}
