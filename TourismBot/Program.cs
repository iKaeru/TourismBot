using TourismBot.Workers;

namespace TourismBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var worker = new TelegramWorkerService();
            worker.ExecuteCore();
        }
    }
}