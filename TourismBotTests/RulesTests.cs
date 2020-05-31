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
        public void IsTextContains_SimpleInput_Found()
        {
            var retVal = _privateObject.Invoke("IsTextContains", "о еде", Rules.Food);
            Assert.AreEqual(true, retVal);
        }

        [Test]
        public void IsTextContains_ComplexInput_Found()
        {
            var retVal = _privateObject.Invoke("IsTextContains",
                "Мы с женой хотим отдохнуть в Белеке, в хорошем отеле на берегу с хорошей едой. Номер должен быть обязательно с видом на море.",
                Rules.Food);
            Assert.AreEqual(true, retVal);
        }
    }
}