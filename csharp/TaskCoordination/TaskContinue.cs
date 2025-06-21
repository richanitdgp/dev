using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace TaskCoordination
{
    public static class TaskContinue
    {
        public static void Main()
        {
            PerformTasks1();
            PerformTasks2();

            Console.WriteLine("Main program done");
        }

        // In the .NET Task Parallel Library, the ContinueWith method is used to define and execute a continuation task 
        // that runs after a specified antecedent Task has completed. 
        // This allows for chaining operations, where one task's completion triggers the execution of another, enabling more complex asynchronous workflows. 
        public static void PerformTasks1()
        {
            Task task1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water...");
            });

            Task task2 = task1.ContinueWith(t =>
            {
                Console.WriteLine($"Completed task {t.Id}, pour water into cup");
            });

            task2.Wait();
        }

        public static void PerformTasks2()
        {
            Task task1 = Task.Factory.StartNew(() => "Task 1");
            Task task2 = Task.Factory.StartNew(() => "Task 2");

            Task task3 = Task.Factory.ContinueWhenAll(new[] {task1, task2},
            tasks =>
            {
                Console.WriteLine("Tasks completed:");
                foreach(var t in tasks)
                    Console.WriteLine(" - " + t.Id);
                Console.WriteLine("All tasks done");
            });

            task3.Wait();
        }
    }
}
