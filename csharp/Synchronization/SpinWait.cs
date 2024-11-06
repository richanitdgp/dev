using System;
using System.Threading.Tasks;

namespace Synchronization
{
    public static class SpinWait
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

            // Create new instance of spinlock
            // Lock that keeps spinning while waiting for the lock to be available
            SpinLock lock1 = new SpinLock();

            

            for(int i=0; i< 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j=0; j<1000; j++)
                    {
                        // Bool to indiciate whether lock was taken.
                        bool lockTaken = false;

                        try
                        {
                            // Task keeps spinning till lock gets acquired
                            // Perform deposit only after lock acquired
                            lock1.Enter(ref lockTaken);
                            account1.Deposit(100);
                        }
                        // Release lock should be under finally section to ensure lock gets released
                        // Even if there is an exception, lock should be released
                        // Otherwise it leads to LockRecursionException since we retry taking the same lock
                        // and spin lock does not support recursion
                        finally
                        {
                            // If lock was acquired, release it.
                            if (lockTaken)
                                lock1.Exit();
                        }
                        
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j=0; j<1000; j++)
                    {
                        // Bool to indiciate whether lock was taken.
                        bool lockTaken = false;

                        try
                        {
                            // Task keeps spinning till lock gets acquired
                            // Perform withdrawal only after lock acquired
                            lock1.Enter(ref lockTaken);
                            account1.Withdraw(100);
                        }
                        finally
                        {
                            // Release if lock was acquired
                            if (lockTaken)
                                lock1.Exit();
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Account balance for account1 is {account1.Balance}");
        }
    }

    public class BankAccount
    {
        public int Balance { get; set; }

        public void Deposit(int amount)
        {
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            Balance -= amount;
        }
    }
}


