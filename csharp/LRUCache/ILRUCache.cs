using System;

namespace LRUCache
{
    public interface ILRUCache
    {
        // Return the value for this key if present in the cache, -1 otherwise
        public int GetItem(int key);

        // Add an entry to save the current key, value pair in the cache
        public void AddItem(int key, int val);

    }

}
