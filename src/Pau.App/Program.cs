namespace Pau.App
{
    internal class Program
    {
        
        private static Application Application = new();
        
        private static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            Console.CancelKeyPress += ConsoleOnCancelKeyPress;


            Console.WriteLine("Hello World!");
            await Application.Start(args);
        }

        private static void CurrentDomainOnProcessExit(object? sender, EventArgs e)
        {
            Console.WriteLine("ProcessExit occurred.");
        }
        
        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("UnhandledException occurred.");
            Console.WriteLine(e.ExceptionObject);
            Application.GracefullyShutdown();
        }
        
        private static void ConsoleOnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("CancelKeyPress occurred.");
            Application.GracefullyShutdown();
        }
    }
}
