namespace HWMultithreading.BlockingQueue
{
    public interface IQueue<T>
    {
        void Enqueue(T obj);
        bool TryEnqueue(T obj);
        T Dequeue();
        bool TryDequeue(out T val);
        void Clear();
    }
}