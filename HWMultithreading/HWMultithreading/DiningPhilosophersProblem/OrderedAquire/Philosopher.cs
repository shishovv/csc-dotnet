namespace HWMultithreading.DiningPhilosophersProblem.OrderedAquire
{
    public class Philosopher
    {
        public int Id { get; }
        public int TotalEaten { get; private set; }
        
        private readonly Fork _fork1;
        private readonly Fork _fork2;

        public Philosopher(int id, Fork fork1, Fork fork2)
        {
            Id = id;
            if (fork1.Id < fork2.Id)
            {
                _fork1 = fork1;
                _fork2 = fork2;
            }
            else
            {
                _fork1 = fork2;
                _fork2 = fork1;
            }
        }

        public void Eat()
        {
            lock (_fork1) lock (_fork2)
            {
                TotalEaten++;
            }
        }
    }
}