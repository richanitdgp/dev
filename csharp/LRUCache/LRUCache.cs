using System;
using System.Collections.Concurrent;
using LRUCache;

namespace LRUCache
{
    public class LRUCache : ILRUCache
    {
        private int capacity;
        private int count;
        public ConcurrentDictionary<int, LRUNode> cache;
        public LRUDoublyLinkedList lrulist;

        public LRUCache(int capacity)
        {
            this.capacity = capacity;
            this.count = 0;
            this.cache = new ConcurrentDictionary<int, LRUNode>(2, 10);
            this.lrulist = new LRUDoublyLinkedList();
        }

        // Return the value for this key if present in the cache, -1 otherwise
        public int GetItem(int key)
        {
            if (cache.ContainsKey(key))
            {
                // add this item to the front of the LRU list
                lrulist.AddItemToFront(cache[key]);
                return cache[key].value;
            }
            else
                return -1;

        }

        // Add an entry to save the current key, value pair in the cache
        public void AddItem(int key, int val)
        {
            LRUNode node;
            if (!cache.ContainsKey(key))
            {
                node = new LRUNode(key, val);
                cache.TryAdd(key, node);

                // Add to top of LRU list
                lrulist.AddItemToFront(node);
            }
            else
            {
                Console.WriteLine($"Item already present in the concurrent dictionary");
                return;
            }
            
        }
    }
}
