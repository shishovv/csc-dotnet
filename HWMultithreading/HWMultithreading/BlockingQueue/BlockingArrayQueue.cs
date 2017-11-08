using System.Collections.Generic;
using System.Threading;

namespace HWMultithreading.BlockingQueue
{
        public class BlockingArrayQueue<T>: IQueue<T>
    {
        private bool _isFull;
        private bool _isEmpty;
        private readonly int _capacity;
        private readonly Queue<T> _queue;
        
        private readonly object _lock = new object();
        
        public BlockingArrayQueue(int capacity)
        {
            _capacity = capacity;
            _queue = new Queue<T>(_capacity);
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
                Monitor.PulseAll(_lock);
            }
        }

        public bool TryEnqueue(T obj)
        {
            if (!Monitor.TryEnter(_lock))
            {
                return false;
            }
            try
            {
                if (_isFull) return false;
                NotSynchronizedEnqueue(obj);
                return true;
            }
            finally
            {
                Monitor.Exit(_lock);
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
                Monitor.PulseAll(_lock);
                return e;
            }
        }

        public bool TryDequeue(out T val)
        {
            if (!Monitor.TryEnter(_lock))
            {
                val = default(T);
                return false;
            }
            try
            {
                if (_isEmpty)
                {
                    val = default(T);
                    return false;
                }
                val = NotSynchronizedDequeue();
                return true;
            }
            finally
            {
                Monitor.Exit(_lock);
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