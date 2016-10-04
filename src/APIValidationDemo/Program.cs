using Microsoft.Owin.Hosting;
using System;

namespace ApiValidationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "API Validation Demo";

            var host = "http://localhost:60000";
            using (WebApp.Start(host))
            {
                Console.WriteLine("API Validation Demo running on {0}.", host);
                Console.ReadKey();
            }
        }
    }
}
