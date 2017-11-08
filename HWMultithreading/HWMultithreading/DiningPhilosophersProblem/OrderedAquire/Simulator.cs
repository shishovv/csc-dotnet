using System;
using System.Linq;
using System.Threading;

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
                .Select(id => new Philosopher(id, forks.ElementAt(id), forks.ElementAt(id % PhilosophersCount)))
                .ToList();
            var threads = philosophers.Select(philosopher => new Thread(() =>
            {
                while (true)
                {
                    philosopher.Eat();
                }
            })).ToList();
            
            threads.ForEach(thread => thread.Start());
            Thread.Sleep(Time);
            threads.ForEach(thread => thread.Abort());
            
            var totalEaten = philosophers.Select(i => i.TotalEaten).Sum();
            Console.WriteLine($"Total eaten {totalEaten}");
            philosophers.ForEach(philosopher => 
                Console.WriteLine($"Philosopher {philosopher.Id} ate " +
                                  $"{philosopher.TotalEaten} " +
                                  $"({Math.Round((double) philosopher.TotalEaten / totalEaten * 100)})"));
        }
    }
}