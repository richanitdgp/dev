using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace TaskCoordination
{
    public static class ChildTasks
    {
        public static void Main()
        {
            PerformTasks1();

            Console.WriteLine("Main program done");
        }

        public static void PerformTasks1()
        {
            Task parent = new Task(() =>
            {
                // detached
                Task child = new Task(() =>
                {
                    Console.WriteLine("Child task starting");
                    Thread.Sleep(3000);
                    Console.WriteLine("Child task completing");
                }, TaskCreationOptions.AttachedToParent);

                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Child task {t.Id} completed with status {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                var failureHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Child task {t.Id} failed with status {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

                child.Start();
            });

            parent.Start();

            try
            {
                parent.Wait();
            }
            catch(AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }
    }
}
