using System.Linq;
using System.Web.Services;

namespace ASPWebFormsTest._11_AJAX._03_AJAX
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService] // Разрешает вызовы JSON из клиентов.
    public class WebService1 : System.Web.Services.WebService
    {
        private CustomersRepository repository = new CustomersRepository();

        [WebMethod] // Атрибут разрешает удаленный вызов метода.
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public Customer FindCustomer(string text)
        {
            return repository.Customers.FirstOrDefault(x => x.FirstName == text || x.LastName == text);
        }
    }
}
