using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public void ExecuteCore()
        {
            while (true)
            {
                ReceiveMessage();
                Thread.Sleep(int.MaxValue);
            }
        }

        private void ReceiveMessage()
        {
            _botClient.OnMessage += Bot_OnMessage;
            _botClient.StartReceiving();
        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

                var ratingRetrieved = GetMaximumRating(e.Message.Text);
                await _botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: $"По вашему запросу получен рейтинг: *{ratingRetrieved}*",
                    ParseMode.Markdown
                );
            }
        }

        private static float GetMaximumRating(string text)
        {
            var resultRating = 0f;
            var isRuleFound = false;
            Type type = typeof(Rules);
            foreach (var p in type.GetFields(System.Reflection.BindingFlags.Static |
                                             System.Reflection.BindingFlags.NonPublic))
            {
                var property = p.GetValue(null);
                float ruleRating = default;
                var ruleFound = TryGetRuleRating(text, property as Rule, out ruleRating);
                Console.WriteLine($"rule rating {ruleRating}");
                Console.WriteLine($"rule found {ruleFound}");

                if (p.Name == $"<{nameof(Rules.TheCheapest)}>k__BackingField" && ruleFound)
                {
                    Console.WriteLine($"!---! rule rating {ruleRating}");
                    return ruleRating;
                }

                if (ruleRating > resultRating)
                    resultRating = ruleRating;
                if (ruleFound) isRuleFound = true;
            }

            if (!isRuleFound) return 4f;
            return resultRating;
        }

        private static bool TryGetRuleRating(string text, Rule rule, out float rating)
        {
            var occurrences = CountWordOccurence(text, rule.AssociatedPhrases);
            if (occurrences > 0)
            {
                var ratingAmount = rule.RatingValue.Count;
                rating = rule.RatingValue[0].Item2;

                for (var i = 0; i < ratingAmount; i++)
                {
                    if (i + 1 >= ratingAmount)
                    {
                        if (occurrences >= rule.RatingValue[i].Item1)
                            rating = rule.RatingValue[i].Item2;
                        break;
                    }

                    if (occurrences >= rule.RatingValue[i].Item1 &&
                        occurrences < rule.RatingValue[i + 1].Item1)
                        rating = rule.RatingValue[i].Item2;
                    else
                        rating = rule.RatingValue[i + 1].Item2;
                }

                return true;
            }

            rating = 0f;
            return false;
        }

        private static int CountWordOccurence(string text, List<string> phrases)
        {
            var counter = 0;
            phrases.ForEach(phrase =>
            {
                if (text.Contains(phrase)) counter++;
            });
            return counter;
        }
    }
}