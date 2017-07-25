<%@ WebHandler Language="C#" Class="_01_JsonParseHandler" %>

using System.Web;

public class _01_JsonParseHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        // ContentType указывающий на наличие JSON объекта в ответе.
        context.Response.ContentType = "application/json";

        // отправка JSON объекта 
        // { "firstName":"Ivan", "lastName":"Ivanov" } - объект со свойтсвами firstName и lastName.
        context.Response.Write("{\"firstName\":\"Ivan\", \"lastName\":\"Ivanov\"}");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}