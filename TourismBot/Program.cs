using System;
using System.Threading.Tasks;
using TourismBot.Workers;

namespace TourismBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var worker = new TelegramWorkerService();
            await worker.ExecuteCore();
        }
    }
}