using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace TaskCoordination
{
    public static class TaskBarrier
    {
        // Barrier is a mechanism to create stages for worker threads
        // All workers in stage1 should complete before moving on to next stage
        public static Barrier barrier = new Barrier(2, b =>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
        });

        public static void Water()
        {
            Console.WriteLine("Putting the kettle on");
            Thread.Sleep(2000);
            barrier.SignalAndWait();
            Console.WriteLine("Pouring water into cup");
            barrier.SignalAndWait();
            Console.WriteLine("Putting the kettle away");


        }

        public static void Cup()
        {
            Console.WriteLine("Finding a cup");
            barrier.SignalAndWait();
            Console.WriteLine("Adding tea");
            barrier.SignalAndWait();
            Console.WriteLine("Adding suger");
        }

        public static void Main()
        {
            Task water = Task.Factory.StartNew(Water);
            Task cup = Task.Factory.StartNew(Cup);

            var tea = Task.Factory.ContinueWhenAll(new[] {water, cup}, tasks =>
            {
                Console.WriteLine("Enjoy your cup of tea");
            });

            tea.Wait();

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
