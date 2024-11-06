using System;

namespace ConsoleApp1
{
    public static class ExceptionHandling
    {
        public static void Main()
        {
            CancelTasks1();

            try
            {
                CancelTasks2();
            }
            catch(AggregateException ae)
            {
                foreach(Exception e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Handled elsewhere {e.GetType()} from source {e.Source} with message {e.Message}");
                }
            }

            Console.WriteLine("Main program is done.");
            Console.ReadLine();
        }

        public static void CancelTasks1()
        {
            Task task1 = Task.Factory.StartNew(() => 
            {
                throw new InvalidOperationException("Invalid operation exception "){ Source = "task1" };
            });

            Task task2 = new Task(() =>
            {
                throw new UnauthorizedAccessException("Unauthorized operation exception"){ Source = "task2" };
            });

            try
            {
                Task.WaitAll(task1, task2);
            }
            catch(AggregateException ae)
            {
                foreach(Exception e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Found exception of type {e.GetType()} from source {e.Source} with message {e.Message}");
                }
            }
        }


        public static void CancelTasks2()
        {
            Task task1 = Task.Factory.StartNew(() => 
            {
                throw new InvalidOperationException("Invalid operation exception "){ Source = "task1" };
            });

            Task task2 = new Task(() =>
            {
                throw new UnauthorizedAccessException("Unauthorized operation exception"){ Source = "task2" };
            });

            try
            {
                Task.WaitAll(task1, task2);
            }
            catch(AggregateException ae)
            {
                ae.Handle(e => 
                {
                    if (e is InvalidOperationException)
                    {
                        Console.WriteLine("Handled Invalid op exception");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
        }
    }
}


