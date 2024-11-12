using System;

namespace LRUCache
{
    public class LRUNode
    {
        public LRUNode prev {get;set;}
        public LRUNode next {get;set;}
        public int key {get;set;}
        public int value {get;set;}

        public LRUNode(){}
        public LRUNode(int k, int v)
        {
            this.key = k;
            this.value = v;
        }

    }

}
