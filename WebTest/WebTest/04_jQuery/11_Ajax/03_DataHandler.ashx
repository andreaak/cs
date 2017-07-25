<%@ WebHandler Language="C#" Class="_03_DataHandler" %>

using System.Threading;
using System.Web;

public class _03_DataHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        Thread.Sleep(2000);

        var parametrs = context.Request.Form;

        context.Response.ContentType = "text/plain";

        // проверка параметров
        if (parametrs["login"] == "user1" && parametrs["pass"] == "12345")
        {
            // отправка ответа
            context.Response.Write("Авторизация прошла успешно");
        }
        else
        {
            context.Response.Write("Ошибка авторизации");
        }
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}