using System;

namespace MiniRoguelike
{
    public class EventLoop
    {
        public event Action<Direction> MoveHandler;
        public event Action ExitHandler;

        public void Run()
        {
            var key = ConsoleKey.F24;
            while (key != ConsoleKey.Escape)
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            MoveHandler?.Invoke(Direction.Up);
                            break;
                        case ConsoleKey.DownArrow:
                            MoveHandler?.Invoke(Direction.Down);
                            break;
                        case ConsoleKey.LeftArrow:
                            MoveHandler?.Invoke(Direction.Left);
                            break;
                        case ConsoleKey.RightArrow:
                            MoveHandler?.Invoke(Direction.Right);
                            break;
                        case ConsoleKey.Escape:
                            ExitHandler?.Invoke();
                            break;
                    }
                }
            }
        }
    }
}