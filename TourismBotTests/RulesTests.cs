using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using TourismBot.Models;
using TourismBot.Workers;
using Assert = NUnit.Framework.Assert;
using CollectionAssert = NUnit.Framework.CollectionAssert;

namespace TourismBotTests
{
    public class Tests
    {
        private static readonly PrivateType RulesPrivate = new PrivateType(typeof(NounCollection));
        private static readonly PrivateType TelegramWorkerPrivate = new PrivateType(typeof(TelegramWorkerService));
        private static readonly string CountOccurenceName = "CountWordOccurence";
        private static readonly string GetMaxRatingName = "GetMaximumRating";
        private static readonly string TryGetRatingName = "TryGetRuleRating";
        private static readonly string GetDeclensionName = "GetNounDeclension";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TryGetRuleRating_OneOccurrence_CorrectValue()
        {
            var result = 0f;
            object[] args =
            {
                "Мы с женой хотим отдохнуть в Белеке, в хорошем отеле на берегу с хорошей едой. " +
                "Номер должен быть обязательно с видом на море.",
                Rules.Food, result
            };
            TelegramWorkerPrivate.InvokeStatic(TryGetRatingName, args);
            var outParameterValue = args[2];
            Assert.AreEqual(4.5f, outParameterValue);
        }

        [Test]
        public void TryGetRuleRating_TwoOccurrence_CorrectValue()
        {
            var result = 0f;
            object[] args =
            {
                "Мы с женой хотим отдохнуть в Белеке, в хорошем отеле на берегу с хорошей едой. " +
                "Номер должен быть обязательно с видом на море. И чтобы можно было не думать о еде",
                Rules.Food, result
            };
            TelegramWorkerPrivate.InvokeStatic(TryGetRatingName, args);
            var outParameterValue = args[2];
            Assert.AreEqual(4.5f, outParameterValue);
        }

        [Test]
        public void TryGetRuleRating_ThreeOccurrence_CorrectValue()
        {
            var result = 0f;
            object[] args =
            {
                "Нужен отель на берегу с хорошей едой. " +
                "Номер должен быть обязательно с видом на море. И чтобы можно было не думать о еде" +
                "И еще раз с едой.",
                Rules.Food, result
            };
            TelegramWorkerPrivate.InvokeStatic(TryGetRatingName, args);
            var outParameterValue = args[2];
            Assert.AreEqual(4.5f, outParameterValue);
        }

        [Test]
        public void TryGetRuleRating_FourOccurrence_CorrectValue()
        {
            var result = 0f;
            object[] args =
            {
                "Нужен отель на берегу с хорошей едой. " +
                "Номер должен быть обязательно с видом на море. И чтобы можно было не думать о еде" +
                "И еще раз с едой. Много еды.",
                Rules.Food, result
            };
            TelegramWorkerPrivate.InvokeStatic(TryGetRatingName, args);
            var outParameterValue = args[2];
            Assert.AreEqual(4.7f, outParameterValue);
        }

        [Test]
        public void GetMaximumRating_InputWithNoRules_CorrectValue()
        {
            var retVal = TelegramWorkerPrivate.InvokeStatic(GetMaxRatingName,
                "нам нужно что-то");
            Assert.AreEqual(4f, retVal);
        }

        [Test]
        public void GetMaximumRating_InputWith0Rules_CorrectMaxValue()
        {
            var retVal = TelegramWorkerPrivate.InvokeStatic(GetMaxRatingName,
                "лалала что-то надо хз что");
            Assert.AreEqual(4f, retVal);
        }

        [Test]
        public void GetMaximumRating_InputWith1Rule_CorrectMaxValue()
        {
            var retVal = TelegramWorkerPrivate.InvokeStatic(GetMaxRatingName,
                "нам нужен бюджетный отель");
            Assert.AreEqual(3.5f, retVal);
        }

        [Test]
        public void GetMaximumRating_InputWith2Rules_CorrectMaxValue()
        {
            var retVal = TelegramWorkerPrivate.InvokeStatic(GetMaxRatingName,
                "бюджетный лалала непривередливым гостям");
            Assert.AreEqual(4.5f, retVal);
        }

        [Test]
        public void CountWordOccurence_InputWithOneOccurrence_Succes()
        {
            var retVal = TelegramWorkerPrivate.InvokeStatic(CountOccurenceName,
                "отель с хорошей едой", Rules.Food.AssociatedPhrases);
            Assert.AreEqual(1, retVal);
        }

        [Test]
        public void CountWordOccurence_InputWithThreeOccurrence_Succes()
        {
            var retVal = TelegramWorkerPrivate.InvokeStatic(CountOccurenceName,
                "отель с хорошей едой, не беспокоиться о еде, много еды", Rules.Food.AssociatedPhrases);
            Assert.AreEqual(3, retVal);
        }

        [Test]
        public void GetNounDeclension_WordWithoutPluralAndWithDuplicates()
        {
            var obj = RulesPrivate.InvokeStatic(GetDeclensionName, "еда");
            var expected = new List<string> {"еда", "еды", "еде", "еду", "едой"};
            CollectionAssert.AreEquivalent(expected, (List<string>) obj);
        }

        [Test]
        public void GetNounDeclension_WordWithPluralAndWithDuplicates()
        {
            var obj = RulesPrivate.InvokeStatic(GetDeclensionName, "компьютер");
            var expected = new List<string>
            {
                "компьютер", "компьютера", "компьютеру", "компьютером", "компьютере",
                "компьютеры", "компьютеров", "компьютерам", "компьютерами", "компьютерах"
            };
            CollectionAssert.AreEquivalent(expected, (List<string>) obj);
        }
    }
}