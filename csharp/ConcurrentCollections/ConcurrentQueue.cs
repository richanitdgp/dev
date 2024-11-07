using System;
using System.Collections.Concurrent;

namespace ConcurrentCollections
{
    public static class ConcurrentQueue
    {
        public static void Main()
        {
            ConcurrentQueue<int> q = new ConcurrentQueue<int>();
            q.Enqueue(5);
            q.Enqueue(8);

            int res;
            // TryDequeue returns true only if at least one element was present in the queue
            // Returns false if it fails to dequeue an element
            if(q.TryDequeue(out res))
            {
                Console.WriteLine($"Removed element {res}");
            }

            // TryPeek returns true only if there is an element in the front of the queue
            // Returns false otherwise
            if(q.TryPeek(out res))
            {
                Console.WriteLine($"Front element in the queue {res}");
            }
        }


    }
}


