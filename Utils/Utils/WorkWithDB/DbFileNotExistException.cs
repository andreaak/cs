using System;

namespace Utils.WorkWithDB
{
    public class DbFileNotExistException : Exception
    {
        public DbFileNotExistException()
            : base("Database File Doesn't Exist")
        {
        }
        public DbFileNotExistException(string messsage)
            : base(messsage)
        {
        }
    }
}
