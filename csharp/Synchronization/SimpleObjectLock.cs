# Example from Gemini
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// Define a simple BankAccount class
public class BankAccount
{
    private decimal _balance;
    private readonly object _balanceLock = new object(); // Object to use for locking to ensure thread safety

    public BankAccount(decimal initialBalance)
    {
        _balance = initialBalance;
        Console.WriteLine($"Bank Account created with initial balance: {_balance:C}");
    }

    public void Deposit(decimal amount, int taskId)
    {
        // Use a lock to ensure only one thread can modify the balance at a time
        lock (_balanceLock)
        {
            _balance += amount;
            Console.WriteLine($"Task {taskId}: Deposited {amount:C}. New balance: {_balance:C}. Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(50); // Simulate some processing time
        }
    }

    public void Withdraw(decimal amount, int taskId)
    {
        // Use a lock for withdrawal as well
        lock (_balanceLock)
        {
            if (_balance >= amount)
            {
                _balance -= amount;
                Console.WriteLine($"Task {taskId}: Withdrew {amount:C}. New balance: {_balance:C}. Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            }
            else
            {
                Console.WriteLine($"Task {taskId}: Attempted to withdraw {amount:C} but insufficient funds. Current balance: {_balance:C}. Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            }
            Thread.Sleep(50); // Simulate some processing time
        }
    }

    public decimal GetBalance()
    {
        // No need to lock for reading if `_balance` is only ever modified under a lock.
        // However, for strict consistency, one might lock for reads too,
        // but for a simple decimal, it's typically fine without.
        return _balance;
    }
}

public class BankTransactionSimulator
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Starting bank transaction simulation...");
        Console.WriteLine($"Main thread ID: {Thread.CurrentThread.ManagedThreadId}\n");

        BankAccount account = new BankAccount(1000m); // Initial balance

        decimal transactionAmount = 100m;
        int numberOfTasks = 4;

        List<Task> allTasks = new List<Task>();

        // Create and add deposit tasks
        for (int i = 0; i < numberOfTasks; i++)
        {
            int taskId = i + 1; // Unique ID for logging
            // Task.Run offloads the synchronous Deposit method to a ThreadPool thread
            allTasks.Add(Task.Run(() => account.Deposit(transactionAmount, taskId)));
        }

        // Create and add withdrawal tasks
        for (int i = 0; i < numberOfTasks; i++)
        {
            int taskId = numberOfTasks + i + 1; // Unique ID for logging
            // Task.Run offloads the synchronous Withdraw method to a ThreadPool thread
            allTasks.Add(Task.Run(() => account.Withdraw(transactionAmount, taskId)));
        }

        Console.WriteLine($"\nAll {allTasks.Count} tasks have been initiated. Waiting for them to complete...");

        // Wait for all tasks to complete.
        // Task.WaitAll blocks the calling thread (in this case, the Main thread)
        // until all tasks in the collection have finished.
        try
        {
            Task.WaitAll(allTasks.ToArray()); // Convert list to array for WaitAll
            Console.WriteLine("\nAll tasks completed successfully.");
        }
        catch (AggregateException ae)
        {
            // Handle any exceptions that occurred in the tasks
            Console.WriteLine("\nOne or more tasks threw an exception:");
            foreach (var ex in ae.Flatten().InnerExceptions)
            {
                Console.WriteLine($"- {ex.GetType().Name}: {ex.Message}");
            }
        }

        Console.WriteLine($"\nFinal Balance: {account.GetBalance():C}");
        Console.WriteLine("Simulation finished.");
    }
}
