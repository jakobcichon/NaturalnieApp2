namespace NaturalnieApp2.Logger.Tests
{
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System.CodeDom;
    using System.Runtime.CompilerServices;

    public class Tests
    {
        Logger logger;

        [SetUp]
        public void Setup()
        {
            logger = new();
        }

        [Test]
        public void AddMessage_LoggerMethodsCalledByDifferentThread_AllMessagesHasBeenAdded()
        {
            const int numberOfEntriesPerCommand = 1000;
            const int waitingTimeMin = 1;
            const int waitingTimeMax = 10;

            const string iterationText = " Iteration : ";

            Dictionary<Action<string>, string> keyValuePairs = new()
            {
                { logger.Debug, "Debug command" },
                { logger.Error, "Error command" },
                { logger.Info, "Info command" },
                { logger.Warn, "Warn command" },
                { logger.Exception, "Exception action command" },
                { logger.UiAction, "Ui action command" }
            };

            List<Task> tasksList = new();

            foreach (KeyValuePair<Action<string>, string> command in keyValuePairs)
            {
                tasksList.Add(Task.Run(() =>
                {
                    Random rand = new();
                    for (int i = 0; i < numberOfEntriesPerCommand; i++)
                    {
                        command.Key.Invoke(command.Value + iterationText + i);
                        Task.Delay(rand.Next(waitingTimeMin, waitingTimeMax));
                    }
                }));
            }

            Task.WaitAll(tasksList.ToArray());

            List<SharedInterfaces.Logger.ILoggerEntry> allEntries = logger.GetLoggerEntries();

            foreach (KeyValuePair<Action<string>, string> element in keyValuePairs)
            {
                for (int i = 0; i < numberOfEntriesPerCommand; i++)
                {
                    Assert.That(allEntries.Any(e => e.Message.Contains(element.Value + iterationText + i)), Is.True);
                }

            }
        }
    }
}