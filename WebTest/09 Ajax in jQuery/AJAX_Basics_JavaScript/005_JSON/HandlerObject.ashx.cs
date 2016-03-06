using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace AJAX_Basics_JavaScript
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {
        class Student
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Course { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "json/application";    
            JavaScriptSerializer js = new JavaScriptSerializer();

            Student st = new Student();
            st.Name = "Ivan";
            st.Age = 25;
            st.Course = "jQuery";
            context.Response.Write(js.Serialize(st));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}