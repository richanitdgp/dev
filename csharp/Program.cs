using System;

namespace ConsoleApp1
{
    public static class Test1
    {
        /*public static void Main()
        {
            // Write chars concurrently with multiple threads
            Task.Factory.StartNew(() => Write(','));

            Task t = new Task(() => Write('?'));
            t.Start();

            Write('c');

            // Write objects concurrently with multiple threads
            Task t2 = new Task(WriteObject, "hello...");
            t2.Start();

            Task.Factory.StartNew(WriteObject, 123);

            Console.WriteLine("Hello, World!");
            Console.WriteLine("Hello, 123!");
            Console.ReadLine();
        }

        private static void Write(char c)
        {
            int i = 0;
            while (i < 1000)
            {
                Console.Write(c);
                i++;
            }
        }

        private static void WriteObject(Object o)
        {
            int i = 0;
            while (i < 1000)
            {
                Console.Write(o);
                i++;
            }
        }*/
    }
}

