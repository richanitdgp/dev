using System;

namespace LRUCache
{
    public class LRUDoublyLinkedList
    {
        private LRUNode head;
        private LRUNode tail;

        public LRUDoublyLinkedList()
        {
            /*head = new LRUNode();
            tail = new LRUNode();
            head.next = tail;
            tail.prev = head;*/
            head = null;
            tail = null;
        }

        // Adds new node to the head
        public void AddItemToFront(LRUNode node)
        {
            if (head == null)
            {
                head = node;
                tail = head;
            }
            else if (head != node)
            {
                // Remove the node from its current position and move to head
                RemoveNode(node);
                node.next = head;
                head.prev = node;
                head = node;
                tail.next = head;
                head.prev = tail;
            }
        }

        // Remove a node from the tail - remove LRU node for eviction
        public void RemoveItemFromTail()
        {
            LRUNode temp = tail;
            tail = tail.prev;
            tail.next = head;
            head.prev = tail;

            temp.prev = null;
            temp.next = null;
        }

        // Remove a specified node from LRU doubly linked list
        public void RemoveNode(LRUNode node)
        {
            if (node.prev != null)
                node.prev.next = node.next;
            
            if (node.next != null)
                node.next.prev = node.prev;
                
            node.next = null;
            node.prev = null;
        }

    }

}
