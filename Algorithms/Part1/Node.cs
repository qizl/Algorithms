﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part1
{
    public class Node
    {
        /// <summary>
        /// 占位符，表示链表要处理的任意数据类型
        /// </summary>
        public object Item { get; set; }
        public Node Next { get; set; }
    }

    /// <summary>
    /// 下压堆栈
    /// </summary>
    /// <typeparam name="Item"></typeparam>
    public class Stack<Item>
    {
        private node _first;
        private int _n;
        private class node
        {
            public Item Item { get; set; }
            public node Next { get; set; }
        }

        public bool IsEmpty { get { return this._first == null; } }
        public int Size { get { return this._n; } }

        public void Push(Item item)
        {
            node oldfirst = this._first;
            this._first = new node();
            this._first.Item = item;
            this._first.Next = oldfirst;
            this._n++;
        }

        public Item Pop()
        {
            Item item = this._first.Item;
            this._first = this._first.Next;
            this._n--;

            return item;
        }
    }

    /// <summary>
    /// 先进先出队列
    /// </summary>
    /// <typeparam name="Item"></typeparam>
    public class Queue<Item>
    {
        private node _first;
        private node _last;
        private int _n;
        private class node
        {
            public Item Item { get; set; }
            public node Next { get; set; }
        }

        public bool IsEmpty { get { return this._first == null; } }
        public int Size { get { return this._n; } }

        public void Enqueue(Item item)
        {
            node oldLast = this._last;
            this._last = new node();
            this._last.Item = item;
            this._last.Next = null;

            if (this.IsEmpty)
                this._first = this._last;
            else
                oldLast.Next = this._last;

            this._n++;
        }

        public Item Dequeue()
        {
            Item item = this._first.Item;
            this._first = this._first.Next;

            if (this.IsEmpty)
                this._last = null;

            this._n--;

            return item;
        }
    }

    /// <summary>
    /// 背包
    /// </summary>
    /// <typeparam name="Item"></typeparam>
    public class Bag<Item>
    {
        private node _first;
        private int _n;
        private class node
        {
            public Item Item { get; set; }
            public node Next { get; set; }
        }

        public bool IsEmpty { get { return this._first == null; } }
        public int Size { get { return this._n; } }

        public void Add(Item item)
        {
            node oldfirst = this._first;
            this._first = new node();
            this._first.Item = item;
            this._first.Next = oldfirst;
            this._n++;
        }
    }
}
