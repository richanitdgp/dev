using System;
using System.Collections.Concurrent;

namespace ConcurrentCollections
{
    public static class ConcurrentStack
    {
        public static void Main()
        {
            ConcurrentStack<int> stack = new ConcurrentStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            int res;
            if (stack.TryPeek(out res))
            {
                Console.WriteLine($"Stack peek element {res}");
            }

            if(stack.TryPop(out res))
            {
                Console.WriteLine($"Popped element {res}");
            }

            int[] items = new int[5];
            if(stack.TryPopRange(items, 0, 5) > 0)
            {
                string text = string.Join(", ", items.Select(i => i.ToString()));
                Console.WriteLine($"Popped these elements from the stack {text}");
            }
        }


    }
}


