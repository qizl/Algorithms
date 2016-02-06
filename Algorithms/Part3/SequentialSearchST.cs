using System;
using System.Collections.Generic;

namespace Algorithms.Part3
{
    public class SequentialSearchST<Key, Value>
    {
        private int _n;

        private Node _first;
        private class Node
        {
            public Key Key { get; set; }
            public Value Value { get; set; }
            public Node Next { get; set; }

            public Node(Key key, Value value, Node next)
            {
                this.Key = key;
                this.Value = value;
                this.Next = next;
            }
        }

        public int Size()
        {
            return this._n;
        }

        public bool IsEmpty()
        {
            return this.Size() == 0;
        }

        public bool Contains(Key key)
        {
            if (key.Equals(default(Key)))
                throw new Exception("argument to contains() is empty");

            return this.Get(key) != null;
        }

        public Value Get(Key key)
        {
            for (Node x = this._first; x != null; x = x.Next)
                if (key.Equals(x.Key))
                    return x.Value;

            return default(Value);
        }

        public void Put(Key key, Value value)
        {
            for (Node x = this._first; x != null; x = x.Next)
                if (key.Equals(x.Key))
                {
                    x.Value = value;
                    return;
                }

            this._first = new Node(key, value, _first);
            this._n++;
        }

        public void Delete(Key key)
        {
            if (key.Equals(default(Key)))
                throw new Exception("argument to delete() is empty");

            this._first = this.delete(this._first, key);
        }

        private Node delete(Node x, Key key)
        {
            if (x == null)
                return null;

            if (key.Equals(x.Key))
            {
                this._n--;
                return x.Next;
            }

            x.Next = this.delete(x.Next, key);
            return x;
        }

        public IEnumerable<Key> Keys()
        {
            Queue<Key> queues = new Queue<Key>();
            for (Node x = this._first; x != null; x = x.Next)
                queues.Enqueue(x.Key);

            return queues;
        }
    }
}