using System;
using System.Collections.Concurrent;

namespace ConcurrentCollections
{
    public static class ConcurrentBag
    {
        public static void Main()
        {
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