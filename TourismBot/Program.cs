using System;
using TourismBot.Models;
using TourismBot.Workers;

namespace TourismBot
{
    class Program
    {
        static void Main(string[] args)
        {
            long before = GC.GetTotalMemory(false);
            WordsCollection.InitializeWordsCollection();
            var worker = new TelegramWorkerService(new Logger.Logger());
            long after = GC.GetTotalMemory(false);
            long consumedInMegabytes = (after - before) / (1024 * 1024);
            Console.WriteLine($"Memory consumed in megabytes : {consumedInMegabytes}");
            worker.ExecuteCore();
        }
    }
}