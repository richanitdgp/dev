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
        ReaderWriterLockSlim rwlock = new ReaderWriterLockSlim();

        public LRUCache(int capacity)
        {
            this.capacity = capacity;
            
            rwlock.EnterWriteLock();
            this.count = 0;
            Console.WriteLine("Initialized cache count=0");
            rwlock.ExitWriteLock();
            
            this.cache = new ConcurrentDictionary<int, LRUNode>(2, 10);
            this.lrulist = new LRUDoublyLinkedList();
        }

        // Return the value for this key if present in the cache, -1 otherwise
        public int GetItem(int key)
        {
            Console.WriteLine($"Fetching item for key '{key}' from cache");

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
            Console.WriteLine($"Adding item '{key},{val}' to cache");
            rwlock.EnterReadLock();
            // Check if cache has reached capacity
            if (this.count == this.capacity)
            {
                Console.WriteLine("Cache count reached capacity - removing an item");
                rwlock.ExitReadLock();
                // Remove LRU item before adding new one
                LRUNode remove = lrulist.RemoveItemFromTail();

                // Remoce item from cache
                KeyValuePair<int, LRUNode> kvp = new KeyValuePair<int, LRUNode>(key, remove);
                cache.TryRemove(kvp);

                rwlock.EnterWriteLock();
                this.count--;
                rwlock.ExitWriteLock();
            }
            else
                rwlock.ExitReadLock();

            LRUNode node;
            if (!cache.ContainsKey(key))
            {
                node = new LRUNode(key, val);
                cache.TryAdd(key, node);

                // Add to top of LRU list
                lrulist.AddItemToFront(node);

                rwlock.EnterWriteLock();
                this.count++;
                Console.WriteLine($"Added iten with key '{key}' to cache and count={count}");
                rwlock.ExitWriteLock();

            }
            else
            {
                Console.WriteLine($"Item already present in the concurrent dictionary");
                return;
            }
            
        }
    }
}
