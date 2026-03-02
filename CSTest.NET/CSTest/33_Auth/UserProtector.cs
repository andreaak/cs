using CSTest._32_Cryptography;
using System.Security.Principal;

namespace CSTest._33_Auth
{
    class UserProtector : Protector
{
    public static void LogIn(string username, string password)
    {
        if (CheckPassword(username, password))
        {
            GenericIdentity gi = new(
                name: username, type: "PacktAuth");
            GenericPrincipal gp = new(
                identity: gi, roles: Users[username].Roles);
            // Установка объекта GenericPrincipal в текущем потоке — это значение 
            // будет использоваться по умолчанию для авторизации.
            Thread.CurrentPrincipal = gp;
        }
    }
}

}
