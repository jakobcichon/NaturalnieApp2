namespace NaturalnieApp2.Logger.Tests
{
    public class Tests
    {
        Logger logger;

        [SetUp]
        public void Setup()
        {
            logger = new();
        }

        [Test]
        [TestCase ]
        public void AddMessage_MutithreadingAction(string message)
        {
            logger.Info(message);
            Assert.Pass();
        }
    }
}