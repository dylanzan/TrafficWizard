using System;
using TrafficWizard.controller;
namespace TrafficWizard
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n ----- rundate: {0} ----- \n", DateTime.Now.ToString());

            TrafficWizardCtr tw = new TrafficWizardCtr();
            tw.Run();
        }
    } 
}
