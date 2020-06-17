using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TourismBot.Helpers;
using TourismBot.Models;

namespace TourismBot.Workers
{
    public class TelegramWorkerService
    {
        private static ITelegramBotClient _botClient;
        private static Logger.Logger _logger;

        public TelegramWorkerService(Logger.Logger logger)
        {
            _botClient = new TelegramBotClient(TelegramSettings.BotToken);
            _logger = logger;
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
                var ratingRetrieved = GetMaximumRating(e.Message.Text);
                var resultAnswer = $"По вашему запросу получен рейтинг: *{ratingRetrieved}*";
                await _botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: resultAnswer,
                    ParseMode.Markdown
                );

                var logInfo = new LoggingInfo
                {
                    ChatId = e.Message.Chat.Id,
                    FirstName = e.Message.Chat.FirstName,
                    LastName = e.Message.Chat.LastName,
                    UserName = e.Message.Chat.Username,
                    Question = e.Message.Text,
                    Answer = resultAnswer,
                    Date = e.Message.Date
                };
                _logger.OutputWriter(logInfo);
            }
        }

        private static float GetMaximumRating(string text)
        {
            var resultRating = 0f;
            var isRuleFound = false;
            Type type = typeof(Rules);
            text = text.ToLower();
            foreach (var p in type.GetFields(System.Reflection.BindingFlags.Static |
                                             System.Reflection.BindingFlags.NonPublic))
            {
                var property = p.GetValue(null);
                float ruleRating = default;
                var ruleFound = TryGetRuleRating(text, property as Rule, out ruleRating);
                if (p.Name == $"<{nameof(Rules.TheCheapest)}>k__BackingField" && ruleFound)
                    return ruleRating;

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
                var index = text.IndexOf(phrase, StringComparison.Ordinal);
                if (index != -1 && IsPhraseSeparatedWithWhitespaces(text, phrase, index))
                    counter++;
            });
            return counter;
        }

        private static bool IsPhraseSeparatedWithWhitespaces(string text, string phrase, int index)
        {
            var textLength = text.Length;
            var phraseLength = phrase.Length;
            var firstIndex = index - 1;
            var secondIndex = index + phraseLength;
            var result = true;
            if (firstIndex >= 0)
                result = !Vocabulary.RussianAlphabet.Contains(text[firstIndex]) &&
                         !Vocabulary.EnglishAlphabet.Contains(text[firstIndex]);
            if (secondIndex <= textLength - 1)
                result = result && !Vocabulary.RussianAlphabet.Contains(text[secondIndex]) &&
                         !Vocabulary.EnglishAlphabet.Contains(text[secondIndex]);
            return result;
        }
    }
}