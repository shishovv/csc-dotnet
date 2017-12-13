namespace HWMultithreading.DiningPhilosophersProblem.OrderedAquire
{
    public class Philosopher
    {
        public int Id { get; }
        public int TotalEaten { get; private set; }
        
        private readonly Fork _forkL;
        private readonly Fork _forkR;

        public Philosopher(int id, Fork forkL, Fork forkR)
        {
            Id = id;
            if (forkL.Id < forkR.Id)
            {
                _forkL = forkL;
                _forkR = forkR;
            }
            else
            {
                _forkL = forkR;
                _forkR = forkL;
            }
        }

        public void Eat()
        {
            lock (_forkL) lock (_forkR)
            {
                TotalEaten++;
            }
        }
    }
}