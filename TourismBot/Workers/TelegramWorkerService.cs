using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TourismBot.Models;

namespace TourismBot.Workers
{
    public class TelegramWorkerService
    {
        private static ITelegramBotClient _botClient;

        public TelegramWorkerService()
        {
            _botClient = new TelegramBotClient(TelegramSettings.BotToken);
        }

        public async Task ExecuteCore()
        {
            while (true)
            {
                await ReceiveMessage();
                await Task.Delay(10 * 1000);
            }
        }

        public async Task ReceiveMessage()
        {
            var me = await _botClient.GetMeAsync();
            _botClient.OnMessage += Bot_OnMessage;
            _botClient.StartReceiving();
        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

                await _botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "You said:\n" + e.Message.Text
                );
            }
        }

       private bool IsTextContains(string text, List<string> rules)
        {
            foreach (var rule in rules)
            {
                if (text.Contains(rule))
                    return true;
            }

            return false;
        }
    }
}