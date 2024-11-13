using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

// To execute C#, please define "static void Main" on a class
// named Solution.

class Solution
{
    public static Queue messageQueue = new Queue();
    public static int capacity = 5;
    public static int count;
    public static ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();


    static void Main(string[] args)
    {
        //List<Task> tasks = new List<Task>();
        int i = 0;
        Task task1 = new Task(() => 
        {
            Producer producer = new Producer();
            producer.Produce(messageQueue, capacity, rwLock, i++);
        });
        task1.Start();

        Task task3 = new Task(() => 
        {
            Producer producer = new Producer();
            producer.Produce(messageQueue, capacity, rwLock, i++);
        });
        task3.Start();

        Task task5 = new Task(() => 
        {
            Producer producer = new Producer();
            producer.Produce(messageQueue, capacity, rwLock, i++);
        });
        task5.Start();

        Task task7 = new Task(() => 
        {
            Producer producer = new Producer();
            producer.Produce(messageQueue, capacity, rwLock, i++);
        });
        task7.Start();

        Task task9 = new Task(() => 
        {
            Producer producer = new Producer();
            producer.Produce(messageQueue, capacity, rwLock, i++);
        });
        task9.Start();

        int j = 0;
        Task task2 = new Task(() => 
        {
            Consumer consumer = new Consumer();
                consumer.Consume(messageQueue, capacity, rwLock, j++);
        });
        task2.Start();

        Task task4 = new Task(() => 
        {
            Consumer consumer = new Consumer();
                consumer.Consume(messageQueue, capacity, rwLock, j++);
        });
        task4.Start();

        Task task6 = new Task(() => 
        {
            Consumer consumer = new Consumer();
                consumer.Consume(messageQueue, capacity, rwLock, j++);
        });
        task6.Start();

        task1.Wait();
        task2.Wait();
        task3.Wait();
        task4.Wait();
        task5.Wait();

        task6.Wait();
        task7.Wait();
        task9.Wait();

        /*for(int i=0; i< 3; i++)
        {
            tasks.Add(Task.Factory.StartNew(() =>
            {
                // Create 1 producer
            Producer producer = new Producer();
            producer.Produce(messageQueue, capacity, rwLock, i);

            }));
        }

        for(int i=0; i< 3; i++)
        {
            tasks.Add(Task.Factory.StartNew(() =>
            {
                Consumer consumer = new Consumer();
                consumer.Consume(messageQueue, capacity, rwLock, i);

            }));
        }*/
        

        

    }
}

public class Producer
{

    public void Produce(Queue queue, int capacity, ReaderWriterLockSlim rwLock, int id)
    {
        Console.WriteLine($"Inside Produce {id}..");
        // Add a message to the queue.
        // Only 1 producer can write at a time

        rwLock.EnterWriteLock();
        while (queue.Count == capacity)
        {
            Thread.Sleep(1000);
        }

        queue.Enqueue("abc" + id.ToString());

        rwLock.ExitWriteLock();

        Console.WriteLine("Added item in Produce()..");

    }

}

public class Consumer
{
    public void Consume(Queue queue, int capacity, ReaderWriterLockSlim rwLock, int id)
    {
        // Fetch message from the queue
        // Only 1 consumer should process a single message

        Console.WriteLine($"Inside Consumer {id}..");
        // Add a message to the queue.
        // Only 1 producer can write at a time

        rwLock.EnterReadLock();
        while (queue.Count == 0)
        {
            Thread.Sleep(1000);
        }

        object res = queue.Dequeue();

        rwLock.ExitReadLock();

        Console.WriteLine($"Consumed item {res.ToString()}..");
    }

}
