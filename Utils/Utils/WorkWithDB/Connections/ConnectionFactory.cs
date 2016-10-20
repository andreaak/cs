namespace Utils.WorkWithDB.Connections
{
    public class ConnectionFactory
    {
        public static IDBConnection GetConnection(string provider, string connString)
        {
            return new DBConnection(provider, connString);
            //return new DBConnectionCL();
            //return new DBConnectionDL();
        }

        public static IDBConnection GetConnection()
        {
            return new DBConnection();
        }
    }
}
