using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Program3
    {
        public static void Main()
        {
            //CancelTask1();
            //CancelTask2();
            CancelTask3();
            Console.WriteLine($"Main program completed");
            Console.ReadLine();
        }

        // Soft cancellation of a task
        private static void CancelTask1()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;

            Task task1 = new Task(() =>
            {
                int i = 0;
                while(true)
                {
                    // Soft cancelation of task - breaks silently without exception
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancellation requested...");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{i++}");
                        Thread.Sleep(1000);
                    }    
                        
                }
            }, token);
            task1.Start();

            Console.ReadLine();
            cts.Cancel();
        }

        // Throw exception on cancelation
        private static void CancelTask2()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;

            Task task1 = new Task(() =>
            {
                int i = 0;
                while(true)
                {
                    // Throw exception if cancelation requested
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancellation requested...");
                        throw new OperationCanceledException();
                    }
                    else
                    {
                        Console.WriteLine($"{i++}");
                        Thread.Sleep(1000);
                    }    
                        
                }
            }, token);
            task1.Start();

            Console.ReadLine();
            cts.Cancel();
        }

        // Throw exception on cancelation - canonical way recommended by TPL
        private static void CancelTask3()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;

            // To get notified when cancelation has been requested
            // In this case, main thread will be notified
            // Its like subscribing to an event
            token.Register(() =>
            {
                Console.WriteLine("Cancelation has been requested.");
            });

            Task task1 = new Task(() =>
            {
                int i = 0;
                while(true)
                {
                    // Throw exception if cancelation requested
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}");
                    Thread.Sleep(1000);                        
                }
            }, token);
            task1.Start();

            // Creating a new task to wait on cancelation request
            // Similar to subscribing to an event
            Task.Factory.StartNew(() => 
            {
                // Blocking function call to wait until cancelation request
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle released, cancelation was requested");
            });

            Console.ReadLine();
            cts.Cancel();
        }
    }
}

