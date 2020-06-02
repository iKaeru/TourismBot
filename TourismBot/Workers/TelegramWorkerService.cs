using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
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

        private void GetMaximumRating(string text)
        {
            Type type = typeof(Rules);
            foreach (var p in type.GetFields(System.Reflection.BindingFlags.Static |
                                             System.Reflection.BindingFlags.NonPublic))
            {
                var v = p.GetValue(null);
                Console.WriteLine(v.ToString());
            }
        }

        private float GetRuleRating(string text, Rule rule)
        {
            if (IsTextContains(text, rule.AssociatedPhrases))
                return rule.RatingValue;
            return 0f;
        }

        private bool IsTextContains(string text, List<string> phrases)
        {
            foreach (var phrase in phrases)
            {
                if (text.Contains(phrase))
                    return true;
            }

            return false;
        }
    }
}