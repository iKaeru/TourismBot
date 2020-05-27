using Telegram.Bot;

namespace TourismBot.Workers
{
    public class TelegramWorkerService
    {
        private readonly ITelegramBotClient _botClient;

        public TelegramWorkerService()
        {
            var setting = new TelegramSettings("telegram", _settingsService.GetSettings());
            _botClient = new TelegramBotClient(setting.BotToken);
        }
        }
    }
}