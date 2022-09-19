namespace NaturalnieApp2.Logger.Tests
{
    using NaturalnieApp2.SharedInterfaces.DialogBox;

    public class Tests
    {
        Logger logger;

        [SetUp]
        public void Setup()
        {
            logger = new();
        }

        [Test]
        public void AddMessage_MutithreadingAction()
        {
            Dictionary<Action<string>, string> keyValuePairs = new();

            keyValuePairs.Add(logger.Debug, "Debug command");
            keyValuePairs.Add(logger.Error, "Error command");
            keyValuePairs.Add(logger.Info, "Info command");
            keyValuePairs.Add(logger.Warn, "Warn command");

            List<Task> tasksList= new();

            foreach(KeyValuePair<Action<string>, string> command in keyValuePairs)
            {
                tasksList.Add(Task.Run(() =>
                {
                    Random rand = new();
                    for (int i = 0; i < 1000; i++)
                    {
                        command.Key.Invoke(command.Value + $" Iteration : {i}");
                        Task.Delay(rand.Next(1, 5));
                    }
                }));
            }

            Task.WaitAll(tasksList.ToArray());


            List<SharedInterfaces.Logger.ILoggerEntry> allEntries = logger.GetLoggerEntries();

            foreach(KeyValuePair<Action<string>, string> element in keyValuePairs)
            {

                for (int i = 0; i < 1000; i++)
                {
                    Assert.IsTrue(allEntries.Any(e => e.Message.Contains(element.Value + $" Iteration : {i}")));
                }
                
            }
        }
    }
}