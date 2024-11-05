using System;

namespace ConsoleApp1
{
    public static class Program2
    {
        public static void Main()
        {
            string text1 = "Hello", text2 = "everyone";

            Task<int> task1 = new Task<int>(GetTextLength, text1);
            task1.Start();

            Task<int> task2 = Task.Factory.StartNew(GetTextLength, text2);

            Console.WriteLine($"Length of '{text1}' is {task1.Result}");
            Console.WriteLine($"Length of '{text2}' is {task2.Result}");

            Console.ReadLine();
        }

        private static int GetTextLength(Object o)
        {
            Console.WriteLine($"Task with id {Task.CurrentId} is processing object {o}");
            return o.ToString().Length;
        }
    }
}

