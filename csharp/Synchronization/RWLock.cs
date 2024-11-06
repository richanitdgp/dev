using System;
using System.Threading.Tasks;

namespace Synchronization
{
    public static class RWLock
    {
        public static ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
        public static int a = 0;

        public static void Main()
        {
            //PerformTasks1();
            PerformTasks2();
            Console.WriteLine("Main program completed.");
        }

        // ReaderWriterLock - use ReaderWriterLockSlim type
        // Multiple readers with read lock, 1 writer 
        // Supports recursive locking but not recommended
        public static void PerformTasks1()
        {
            Random random = new Random();
            List<Task> tasks = new List<Task>();

            for (int i=0; i<10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    rwLock.EnterReadLock();
                    Console.WriteLine($"Entered read lock. Value of a : {a}");
                    Thread.Sleep(2000);

                    rwLock.ExitReadLock();
                    Console.WriteLine($"Exited read lock. Value of a : {a}");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch(AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                });
            }

            while(true)
            {
                Console.ReadLine();

                rwLock.EnterWriteLock();
                Console.WriteLine("Write lock acquired.");
                int newVal = random.Next(10);
                a = newVal;
                Console.WriteLine($"Set a = {a}");

                rwLock.ExitWriteLock();
                Console.WriteLine("Write lock released");
            }
        }

        public static void PerformTasks2()
        {
            Random random = new Random();
            List<Task> tasks = new List<Task>();

            for (int i=0; i<10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    // EnterReadLock cannot be followed by EnterWriteLock
                    // Need to release the read lock before acquiring the write lock
                    // Use UpgradeableReadLock in this case.
                    // rwLock.EnterReadLock();
                    rwLock.EnterUpgradeableReadLock();

                    Console.WriteLine($"Entered upgradeable read lock. Value of a : {a}");
                    Thread.Sleep(2000);

                    if (i%2 == 0)
                    {
                        rwLock.EnterWriteLock();
                        Console.WriteLine($"Entered write lock, a={a}");
                        a = 28;
                        rwLock.ExitWriteLock();
                        Console.WriteLine($"Exited write lock, a={a}");
                    }

                    rwLock.ExitUpgradeableReadLock();
                    Console.WriteLine($"Exited upgradeable read lock. Value of a : {a}");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch(AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                });
            }
        }
    }
}