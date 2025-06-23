using System;
using System.Threading.Tasks;

public static class BasicTaskRun
{
    public static async Task Main()
    {
        Console.WriteLine("Happy Monday...!!");
        Console.WriteLine($"Main thread ID: {Thread.CurrentThread.ManagedThreadId}");

        Console.WriteLine("Starting a task with Task.Run()...");

        // Create and start a new Task using Task.Run()
        Task<int> myTask = Task.Run(() =>
        {
            Console.WriteLine($"Task started on thread ID: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
            Console.WriteLine("Task.Run finished.");
            return 8;
        });

        Console.WriteLine("Task.Run initiated. Main thread continues.");

        // Await the task's completion - non-blocking
        int res = await myTask;

        Console.WriteLine($"Task.Run result: {res}. Main thread finished waiting.");
    }
}