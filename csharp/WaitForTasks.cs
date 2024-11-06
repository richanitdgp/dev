using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class WaitForTasks
    {
        public static void Main(string[] args)
        {
            WaitForTask1();
            WaitForTask2();
            WaitForTask3();
            WaitForTask4();

            Console.WriteLine($"Main program completed");
            Console.ReadLine();
        }

        public static void WaitForTask1()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;

            Task task1 = new Task(() =>
            {
                Console.WriteLine("Task running for 5s, starting task1...");

                for(int i = 0; i< 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("task1 complete");
            }, token);

            task1.Start();

            // Wait for task1 to complete
            // Pass the token so that waiting thread gets to know if the task got canceled.
            Console.WriteLine("Main program waiting for task1 to complete...");
            task1.Wait(token);
        }

        public static void WaitForTask2()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;

            Task task1 = new Task(() =>
            {
                Console.WriteLine("Task running for 5s, starting task1...");

                for(int i = 0; i< 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("task1 complete");
            }, token);

            task1.Start();

            Task task2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Starting task2...");
                Thread.Sleep(3000);
                Console.WriteLine("task2 complete");
            }, token);

            // Wait for all the specified tasks to complete
            Console.WriteLine("Main program waiting for task1 and task2...");
            Task.WaitAll(task1, task2);
        }

        public static void WaitForTask3()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;

            Task task1 = new Task(() =>
            {
                Console.WriteLine("Task running for 5s, starting task1...");

                for(int i = 0; i< 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("task1 complete");
            }, token);

            task1.Start();

            Task task2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Starting task2...");
                Thread.Sleep(3000);
                Console.WriteLine("task2 complete");
            }, token);

            // Wait for all the specified tasks to complete
            Console.WriteLine("Main program waiting for any one out of task1 and task2 to complete...");
            Task.WaitAny(task1, task2);
        }

        public static void WaitForTask4()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;

            Task task1 = new Task(() =>
            {
                Console.WriteLine("[WaitForTasks4] Starting task1...");

                for(int i = 0; i< 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("task1 complete");
            }, token);

            task1.Start();

            Task task2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Starting task2...");
                Thread.Sleep(3000);
                Console.WriteLine("task2 complete");
            }, token);

            // Wait for all the specified tasks to complete
            Console.WriteLine("Main program waiting for all tasks with threshold of 4s...");
            Task.WaitAll(new[] {task1, task2}, 4000, token);

            Console.WriteLine($"Status of task1 is {task1.Status}");
            Console.WriteLine($"Status of task2 is {task2.Status}");
        }
    }
}