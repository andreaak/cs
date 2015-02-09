using System.Configuration;

namespace GrabDatabase
{
    public class Options
    {
        private static string helpProvider = null;
        public static string HelpProvider 
        {
            get
            {
                if (helpProvider == null)
                {
                    helpProvider = ConfigurationManager.ConnectionStrings["HELP"].ProviderName;
                }
                return helpProvider;
            }
        
        }

        private static string helpConnString = null;
        public static string HelpConnString 
        {
            get
            {
                if (helpConnString == null)
                {
                    helpConnString = ConfigurationManager.ConnectionStrings["HELP"].ConnectionString;
                }
                return helpConnString;
            }
        }
    }
}
