using System;
using System.Web;

namespace ASPWebFormsTest._17_Modules
{
    public class _02_Module : IHttpModule
    {
        private string _header = "Header from HTTP MODULE";
        private string _footer = "Footer from HTTP MODULE";

        // В качестве параметра в данный метод будет передана ссылка на HttpApplication, который выполняет обработку запроса.
        // В методе Init необходимо выбрать события приложения, которые будет обрабатывать модуль.
        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
            context.EndRequest += context_EndRequest;
        }

        // Перед запуском HTTP обработчика выполнится данный метод и добавит текст в ответ.
        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            application.Response.Write(_header);
        }

        // После запуска HTTP обработчика выполнится данный метод и добавит текст в конец ответа.
        void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            application.Response.Write(_footer);
        }

        // Метод будет вызван когда модуль больше не будет использоваться HTTP приложением.
        public void Dispose()
        {
        }
    }
}