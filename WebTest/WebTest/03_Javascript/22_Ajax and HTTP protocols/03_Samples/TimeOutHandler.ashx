<%@ WebHandler Language="C#" Class="TimeOutHandler" %>

using System;
using System.Web;

public class TimeOutHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        System.Threading.Thread.Sleep(3000); // Задержка на 3 секунды.
        context.Response.Write("Hello World " + DateTime.Now.ToLocalTime()); // Отправка ответа.
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}