using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HWMultithreading.DiningPhilosophersProblem.OrderedAquire
{
    public class Simulator
    {
        public int PhilosophersCount { get; }
        public int Time { get; }

        public Simulator(int philosophersCount, int time)
        {
            PhilosophersCount = philosophersCount;
            Time = time;
        }

        public void Simulate()
        {
            var forks = Enumerable.Range(0, PhilosophersCount).Select(id => new Fork(id)).ToList();
            var philosophers = Enumerable.Range(0, PhilosophersCount)
                .Select(id => new Philosopher(id, forks[id], forks[(id + 1) % PhilosophersCount]))
                .ToList();
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            
            philosophers.ForEach(philosopher => Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    philosopher.Eat();
                }
            }, token));
            
            Thread.Sleep(Time);
            tokenSource.Dispose();
            
            var totalEaten = philosophers.Select(i => i.TotalEaten).Sum();
            Console.WriteLine($"Total eaten {totalEaten}");
            philosophers.ForEach(philosopher => 
                Console.WriteLine($"Philosopher {philosopher.Id} ate " +
                                  $"{philosopher.TotalEaten} " +
                                  $"({Math.Round((double) philosopher.TotalEaten / totalEaten * 100)})"));
        }
    }
}