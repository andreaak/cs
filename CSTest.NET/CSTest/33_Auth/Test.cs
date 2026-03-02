using Azure.Core;
using CSTest._05_Delegates_and_Events._02_Events._0_Setup;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CSTest._33_Auth
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestAuth()
        {
            Debug.WriteLine("Registering Alice, Bob, and Eve with passwords Pa$$w0rd.");
            UserProtector.Register("Alice", "Pa$$w0rd", roles: new[] { "Admins" });
            UserProtector.Register("Bob", "Pa$$w0rd",
              roles: new[] { "Sales", "TeamLeads" });
            // Зарегистрировать пользователя Eve, не включенного ни в одну из ролей
            UserProtector.Register("Eve", "Pa$$w0rd");
            Debug.WriteLine("");
            // Запросить у пользователя имя и пароль для входа в систему
            // Войти в систему от имени одного из трех пользователей
            Debug.Write("Enter your username: ");
            //string? username = ReadLine()!;
            string? username = "Alice";
            Debug.Write("Enter your password: ");
            //string? password = ReadLine()!;
            string? password = "Pa$$w0rd";
            UserProtector.LogIn(username, password);
            if (Thread.CurrentPrincipal == null)
            {
                Debug.WriteLine("Log in failed.");
                return; // Завершить работу приложения
            }

            IPrincipal p = Thread.CurrentPrincipal;
            Debug.WriteLine($"IsAuthenticated: {p.Identity?.IsAuthenticated}");
            Debug.WriteLine($"AuthenticationType: {p.Identity?.AuthenticationType}");
            Debug.WriteLine($"Name: {p.Identity?.Name}");
            Debug.WriteLine($"IsInRole(\"Admins\"): {p.IsInRole("Admins")}");
            Debug.WriteLine($"IsInRole(\"Sales\"): {p.IsInRole("Sales")}");
            if (p is ClaimsPrincipal principal)
            {
                Debug.WriteLine($"{principal.Identity?.Name} has the following claims:");
                foreach (Claim claim in principal.Claims)
                {
                    Debug.WriteLine($"{claim.Type}: {claim.Value}");
                }
            }
        }

        [Test]
        public void TestAuth2()
        {
            Debug.WriteLine("Registering Alice, Bob, and Eve with passwords Pa$$w0rd.");
            UserProtector.Register("Alice", "Pa$$w0rd", roles: new[] { "Admins" });
            UserProtector.Register("Bob", "Pa$$w0rd",
              roles: new[] { "Sales", "TeamLeads" });
            // Зарегистрировать пользователя Eve, не включенного ни в одну из ролей
            UserProtector.Register("Eve", "Pa$$w0rd");
            Debug.WriteLine("");
            // Запросить у пользователя имя и пароль для входа в систему
            // Войти в систему от имени одного из трех пользователей
            Debug.Write("Enter your username: ");
            //string? username = ReadLine()!;
            string? username = "Alice";
            Debug.Write("Enter your password: ");
            //string? password = ReadLine()!;
            string? password = "Pa$$w0rd";
            UserProtector.LogIn(username, password);
            if (Thread.CurrentPrincipal == null)
            {
                Debug.WriteLine("Log in failed.");
                return; // Завершить работу приложения
            }

            IPrincipal p = Thread.CurrentPrincipal;
            Debug.WriteLine($"IsAuthenticated: {p.Identity?.IsAuthenticated}");
            Debug.WriteLine($"AuthenticationType: {p.Identity?.AuthenticationType}");
            Debug.WriteLine($"Name: {p.Identity?.Name}");
            Debug.WriteLine($"IsInRole(\"Admins\"): {p.IsInRole("Admins")}");
            Debug.WriteLine($"IsInRole(\"Sales\"): {p.IsInRole("Sales")}");
            if (p is ClaimsPrincipal principal)
            {
                Debug.WriteLine($"{principal.Identity?.Name} has the following claims:");
                foreach (Claim claim in principal.Claims)
                {
                    Debug.WriteLine($"{claim.Type}: {claim.Value}");
                }
            }

            if (Thread.CurrentPrincipal is null)
            {
                throw new SecurityException(
                    "A user must be logged in to access this feature.");
            }
            if (!Thread.CurrentPrincipal.IsInRole("Admins"))
            {
                throw new SecurityException("User must be a member of Admins to access this feature.");
            }
            Debug.WriteLine("You have access to this secure feature.");
        }

    }

}
