using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        private int CountWordOccurence(string text, List<string> phrases)
        {
            return text.Split('.', ' ', ',')
                .Where(phrases.Contains)
                .Count();
        }

        private float GetMaximumRating(string text)
        {
            var resultRating = 0f;
            var isRuleFound = false;
            Type type = typeof(Rules);
            foreach (var p in type.GetFields(System.Reflection.BindingFlags.Static |
                                             System.Reflection.BindingFlags.NonPublic))
            {
                var property = p.GetValue(null);
                var ruleRating = GetRuleRating(text, property as Rule);
                if (ruleRating != 0f) isRuleFound = true;
                if (ruleRating > resultRating)
                    resultRating = ruleRating;
            }

            if (!isRuleFound) return 4f;
            return resultRating;
        }

        private float GetRuleRating(string text, Rule rule)
        {
            if (IsTextContains(text, rule.AssociatedPhrases))
                return rule.RatingValue[0].Item2; // todo fix to iterate
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