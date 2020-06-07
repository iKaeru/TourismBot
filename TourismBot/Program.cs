using TourismBot.Models;
using TourismBot.Workers;

namespace TourismBot
{
    class Program
    {
        static void Main(string[] args)
        {
            WordsCollection.InitializeWordsCollection();
            var worker = new TelegramWorkerService();
            worker.ExecuteCore();
        }
    }
}