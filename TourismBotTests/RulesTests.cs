using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using TourismBot.Models;
using TourismBot.Workers;
using Assert = NUnit.Framework.Assert;

namespace TourismBotTests
{
    public class Tests
    {
        private static TelegramWorkerService _service = new TelegramWorkerService();
        private static PrivateObject _privateObject = new PrivateObject(_service);

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
            _privateObject.Invoke("TryGetRuleRating", args);
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
            _privateObject.Invoke("TryGetRuleRating", args);
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
            _privateObject.Invoke("TryGetRuleRating", args);
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
            _privateObject.Invoke("TryGetRuleRating", args);
            var outParameterValue = args[2];
            Assert.AreEqual(4.7f, outParameterValue);
        }
        
        [Test]
        public void GetMaximumRating_InputWithNoRules_CorrectValue()
        {
            var retVal = _privateObject.Invoke("GetMaximumRating",
                "нам нужно что-то");
            Assert.AreEqual(4f, retVal);
        }
        
        [Test]
        public void GetMaximumRating_InputWith0Rules_CorrectMaxValue()
        {
            var retVal = _privateObject.Invoke("GetMaximumRating",
                "лалала что-то надо хз что");
            Assert.AreEqual(4f, retVal);
        }
        
        [Test]
        public void GetMaximumRating_InputWith1Rule_CorrectMaxValue()
        {
            var retVal = _privateObject.Invoke("GetMaximumRating",
                "нам нужен бюджетный отель");
            Assert.AreEqual(3.5f, retVal);
        }

        [Test]
        public void GetMaximumRating_InputWith2Rules_CorrectMaxValue()
        {
            var retVal = _privateObject.Invoke("GetMaximumRating",
                "бюджетный лалала непривередливым гостям");
            Assert.AreEqual(4.5f, retVal);
        }

        [Test]
        public void CountWordOccurence_InputWithOneOccurrence_Succes()
        {
            var retVal = _privateObject.Invoke("CountWordOccurence",
                "отель с хорошей едой", Rules.Food.AssociatedPhrases);
            Assert.AreEqual(1, retVal);
        }

        [Test]
        public void CountWordOccurence_InputWithThreeOccurrence_Succes()
        {
            var retVal = _privateObject.Invoke("CountWordOccurence",
                "отель с хорошей едой, не беспокоиться о еде, много еды", Rules.Food.AssociatedPhrases);
            Assert.AreEqual(3, retVal);
        }
    }
}