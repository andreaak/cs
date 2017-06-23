<%@ WebHandler Language="C#" Class="Manifest" %>

using System;
using System.Web;

public class Manifest : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        // Контент файла манифеста должен возвращаться с сервера с ContentType text/cache-manifest.
        context.Response.ContentType = "text/cache-manifest";
        context.Response.WriteFile(context.Server.MapPath("Manifest.appcache"));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
}