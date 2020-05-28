using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TourismBot.Models;

namespace TourismBot.Workers
{
    public class TelegramWorkerService
    {
        private readonly ITelegramBotClient _botClient;

        public TelegramWorkerService()
        {
            _botClient = new TelegramBotClient(TelegramSettings.BotToken);
        }

        public async Task Talk()
        {
            var allowedUpdates = new List<UpdateType> {UpdateType.Message};
            await _botClient.GetUpdatesAsync(0, 10, 20, allowedUpdates);
            // await _botClient.SendTextMessageAsync(chatId: settings.ChatId, text: textMessage.ToString(),
            //        ParseMode.Markdown);
        }
    }
}