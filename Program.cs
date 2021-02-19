using System;
using TrafficWizard.controller;
namespace TrafficWizard
{
    class Program
    {
        static void Main(string[] args)
        {
            TrafficWizardCtr tw = new TrafficWizardCtr();
            Console.WriteLine("Debug:1");
            tw.Run();
        }
    }
}
