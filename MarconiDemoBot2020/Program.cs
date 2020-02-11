using System;

namespace MarconiDemoBot2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Avvio applicazione...");

            IBot bot = new Bot();
            bot.Start();

            Console.WriteLine("Applicazione avviata");
            Console.ReadLine();
        }
    }
}
