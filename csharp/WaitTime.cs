using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class WaitTime
    {
        public static void Main()
        {
            //CancelTask1();
            //CancelTask2();
            WaitOnTask3();
            Console.WriteLine($"Main program completed");
            Console.ReadLine();
        }

        // Wait on task - sleep
        // Thread gives up the CPU and scheduler get another thread assigned on CPU
        // Context switching happens
        private static void WaitOnTask1()
        {
            Task task1 = new Task(() =>
            {
                Thread.Sleep(5000);
            });
            task1.Start();

            Console.WriteLine("Done waiting for task1");
            Console.ReadLine();
        }

        // Spin wait
        private static void WaitOnTask2()
        {
            // Spinwait - thread keeps holding  the cpu
            // scheduler does not assign cpu to any other thread
            // wasting cpu cycles - avoiding context switches
            // no context switching - preferable for small wait times
            Task task1 = new Task(() =>
            {
                SpinWait.SpinUntil(() => 
                {
                    return true;
                });
            });
            task1.Start();

            Console.ReadLine();
        }

        // Wait on cancelation token
        private static void WaitOnTask3()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;

            Task task1 = new Task(() =>
            {
                Console.WriteLine("Press any key within 5s...");
                bool canceled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(canceled ? "Input received timely" : "Boom!!!");
            }, token);
            task1.Start();

            Console.ReadLine();
            cts.Cancel();
        }
    }
}

