namespace CS_TDD._003_DI._012_Encapsulation
{
    class Program
    {
        static void Main()
        {
#if RELEASE
            string fileName = "file2.log";

            FileManager mgr = new FileManager();

            if (mgr.FindLogFile(fileName))
            {
                Console.WriteLine("File {0} is found!", fileName);
            }
            else
            {
                Console.WriteLine("File {0} is not found!", fileName);
            }

            Console.ReadKey();
#endif                                                                                                         
        }
    }
}
