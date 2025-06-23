using System;
using System.Collections.Concurrent;

namespace ConcurrentCollections
{
    public static class ConcurrentBag
    {
        public static void Main()
        {
            // ConcurrentBag<T> is a thread-safe, unordered collection of objects. 
            // It's part of the System.Collections.Concurrent namespace and is designed for scenarios where you have multiple threads adding and removing items from a collection, and the order of items is not important.
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            List<Task> tasks = new List<Task>();

            for(int i =0; i<10; i++)
            {
                int item = i;
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    bag.Add(item);
                    Console.WriteLine($"The task {Task.CurrentId} has added item {item} to the bag");

                    int res;
                    // Try to peek at an item without removing it
                    if(bag.TryPeek(out res))
                    {
                        Console.WriteLine($"Task {Task.CurrentId} has peeked item {res}");
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            int last;
            // There is no order of elements maintained in concurrent bag
            // Hence we can get any item
            if(bag.TryTake(out last))
                Console.WriteLine($"I got item {last}");
        }
    }
}
