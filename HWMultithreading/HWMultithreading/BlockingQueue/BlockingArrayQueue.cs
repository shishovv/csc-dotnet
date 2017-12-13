using System;
using System.Collections.Generic;
using System.Threading;

namespace HWMultithreading.BlockingQueue
{
    public class BlockingArrayQueue<T> : IQueue<T>
    {
        private bool _isFull;
        private bool _isEmpty;
        private readonly int _capacity;
        private readonly Queue<T> _queue;

        private readonly object _lock;

        public BlockingArrayQueue(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException();
            }
            _capacity = capacity;
            _queue = new Queue<T>(_capacity);
            _isEmpty = true;
            _lock = new object();
        }

        public void Enqueue(T obj)
        {
            lock (_lock)
            {
                while (_isFull)
                {
                    Monitor.Wait(_lock);
                }
                NotSynchronizedEnqueue(obj);
                Monitor.Pulse(_lock);
            }
        }

        public bool TryEnqueue(T obj)
        {
            lock (_lock)
            {
                if (_isFull)
                {
                    return false;
                }
                NotSynchronizedEnqueue(obj);
                return true;
            }
        }

        public T Dequeue()
        {
            lock (_lock)
            {
                while (_isEmpty)
                {
                    Monitor.Wait(_lock);
                }
                var e = NotSynchronizedDequeue();
                Monitor.Pulse(_lock);
                return e;
            }
        }

        public bool TryDequeue(out T val)
        {
            lock (_lock)
            {
                if (_isEmpty)
                {
                    val = default(T);
                    return false;
                }
                val = NotSynchronizedDequeue();
                return true;
            }
        }

        private void NotSynchronizedEnqueue(T obj)
        {
            _queue.Enqueue(obj);
            if (_queue.Count == _capacity)
            {
                _isFull = true;
            }
            _isEmpty = false;
        }

        private T NotSynchronizedDequeue()
        {
            var e = _queue.Dequeue();
            if (_queue.Count == 0)
            {
                _isEmpty = true;
            }
            _isFull = false;
            return e;
        }

        public void Clear()
        {
            if (_isEmpty)
            {
                return;
            }
            lock (_lock)
            {
                _queue.Clear();
                _isEmpty = true;
                _isFull = false;
            }
        }
    }
}