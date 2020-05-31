using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using TourismBot.Models;
using TourismBot.Workers;
using Assert = NUnit.Framework.Assert;

namespace TourismBotTests
{
    public class Tests
    {
        private TelegramWorkerService _service = new TelegramWorkerService();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            PrivateObject obj = new PrivateObject(_service);
            var retVal = obj.Invoke("IsTextContains", "о еде", Rules.Food);
            Assert.AreEqual(true, retVal);
        }
    }
}