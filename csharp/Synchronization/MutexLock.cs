using System;
using System.Threading.Tasks;

namespace Synchronization
{
    public static class MutexLock
    {
        public static void Main()
        {
            PerformTasks1();
            Console.WriteLine("Main program completed.");
        }

        // The account balance would be different every time without locks
        // After adding locks, the account balance would be 0 at the end
        // Lock ensures that each deposit and each withraw is atomic operation.
        public static void PerformTasks1()
        {
            BankAccount account1 = new BankAccount();
            List<Task> tasks = new List<Task>();
            Mutex mutex1 = new Mutex();

            for(int i=0; i< 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j=0; j<1000; j++)
                    {
                        // Bool to indiciate whether lock was taken.
                        bool haveLock = mutex1.WaitOne();

                        try
                        {
                            // Entered critical section - perform operation
                            account1.Deposit(100);
                        }
                        // Release lock should be under finally section to ensure lock gets released
                        // Even if there is an exception, lock should be released
                        finally
                        {
                            // If lock was acquired, release it.
                            if (haveLock)
                                mutex1.ReleaseMutex();
                        }
                        
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j=0; j<1000; j++)
                    {
                        // Bool to indiciate whether lock was taken.
                        bool haveLock = mutex1.WaitOne();

                        try
                        {
                            account1.Withdraw(100);
                        }
                        finally
                        {
                            // Release if lock was acquired
                            if (haveLock)
                                mutex1.ReleaseMutex();
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Account balance for account1 is {account1.Balance}");
        }
    }
}


