using System;
using System.Collections.Concurrent;

namespace ConcurrentCollections
{
    public static class ProducerConsumer
    {
        // ConcurrentBag cannot be used for ProducerConsumer pattern since it does not have bounded 
        // capacity. Also, it does not block when producer/consumer is trying to wait on an event
        // ConcurrentBag<int> bag = new ConcurrentBag<int>();
        // BlockingCollection is ideal for this pattern.
        public static BlockingCollection<int> messages = new BlockingCollection<int>(new ConcurrentBag<int>(), boundedCapacity: 10);

        public static CancellationTokenSource cts = new CancellationTokenSource();

        public static Random random = new Random();

        public static void Main()
        {
            Console.WriteLine($"Starting producer and consumer...");

            Task producer = Task.Factory.StartNew(RunProducer);
            Task consumer = Task.Factory.StartNew(RunConsumer);

            try
            {
                Task.WaitAll(new[]{producer, consumer}, cts.Token);
            }
            catch(AggregateException ae)
            {
                ae.Handle(e => {
                    return true;
                });
            }
        }

        public static void RunProducer()
        {
            while(true)
            {
                cts.Token.ThrowIfCancellationRequested();
                int item = random.Next(100);
                messages.Add(item);
                Console.WriteLine($"Producer: +{item}");
                Thread.Sleep(random.Next(1000));
            }
        }

        public static void RunConsumer()
        {
            // The GetConsumingEnumerable() removes the item from messages collection
            // No need to remove explicitly
            foreach(var item in messages.GetConsumingEnumerable())
            {
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"Consumed: {item}");
                Thread.Sleep(random.Next(1000));
            }
        }
    }
}