using System;
using System.Threading.Tasks;

namespace LRUCache
{
    public class Program
    {
        public static Random random = new Random();

        public static ReaderWriterLockSlim rwlock = new ReaderWriterLockSlim();
        public static void Main()
        {
            Console.WriteLine("Starting cache...");
            LRUCache cache = new LRUCache(5);

            Console.WriteLine("Adding item '8' to cache...");
            cache.AddItem(8, 123);
            Console.WriteLine("Completed adding item '5' to cache...");

            Console.WriteLine("Fetching item '3'...");
            int val = cache.GetItem(3);
            Console.WriteLine($"Fetched item with key '3' having value '{val}'");

            int k = 0;
            List<Task> tasks = new List<Task>();
            for(var i = 0; i<5; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    rwlock.EnterWriteLock();
                    k++;
                    Console.WriteLine($"Task {k} adding to cache");
                    cache.AddItem(k, random.Next(100));
                    Console.WriteLine($"Task {k} done");
                    rwlock.ExitWriteLock();
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("Fetching item '3'...");
            val = cache.GetItem(3);
            Console.WriteLine($"Fetched item with key '3' having value '{val}'");

        }

    }

}
