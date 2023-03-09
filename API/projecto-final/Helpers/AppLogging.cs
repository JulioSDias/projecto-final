using System.Diagnostics.Eventing.Reader;

namespace Projecto_Final.Helpers
{
    public class AppLogging : IAppLogging
    {
        public bool LogInfo(string message) {

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[APP LOGGING INFO]:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("    " + message);
            return true;
        }

        public bool LogWarning(string message)
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[APP LOGGING WARNING]:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("    " + message);
            return true;
        }

        public bool LogError(string message)
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[APP LOGGING ERROR]:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("    " + message);
            return false;
        }
    }
}
