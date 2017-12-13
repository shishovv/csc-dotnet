using System;
using System.Security;

namespace MiniRoguelike
{
    public class MiniRoguelikeGame
    {
        private readonly GameBoard _board;

        public MiniRoguelikeGame(GameBoard board)
        {
            _board = board;
        }

        public void Play()
        {
            var eventLoop = new EventLoop();
            eventLoop.MoveHandler += Move;
            eventLoop.ExitHandler += Console.Clear;
            
            ConsoleCancelEventHandler cancelEventHandler = (a, b) => Console.Clear();
            Console.CancelKeyPress += cancelEventHandler;
            
            Draw();
            try
            {
                Console.CursorVisible = false;
            }
            catch (SecurityException e)
            {
                // ignore
            }
            
            eventLoop.Run();
            
            eventLoop.MoveHandler -= Move;
            eventLoop.ExitHandler -= Console.Clear;
            Console.CancelKeyPress -= cancelEventHandler;
        }
        
        private void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    MoveUp();
                    break;
                case Direction.Down:
                    MoveDown();
                    break;
                case Direction.Left:
                    MoveLeft();
                    break;
                case Direction.Right:
                    MoveRight();
                    break;
            }
        }

        private void MoveRight()
        {
            if (_board.MoveHero(_board.Hero.X, _board.Hero.Y + 1))
            {
                Redraw((_board.Hero.X, _board.Hero.Y - 1));
            }
        }
        
        private void MoveLeft()
        {
            if (_board.MoveHero(_board.Hero.X, _board.Hero.Y - 1))
            {
                Redraw((_board.Hero.X, _board.Hero.Y + 1));
            }
        }
        
        private void MoveUp()
        {
            if (_board.MoveHero(_board.Hero.X - 1, _board.Hero.Y))
            {
                Redraw((_board.Hero.X + 1, _board.Hero.Y));
            }
        }

        private void MoveDown()
        {
            if (_board.MoveHero(_board.Hero.X + 1, _board.Hero.Y))
            {
                Redraw((_board.Hero.X - 1, _board.Hero.Y));
            }
        }

        private void Redraw(params ValueTuple<int, int>[] cells)
        {
            foreach (var cell in cells)
            {
                DrawAt(cell.Item1, cell.Item2, (char) _board.Board[cell.Item1][cell.Item2].Type);
            }
            DrawAt(_board.Hero.X, _board.Hero.Y, (char) _board.Hero.Type);
        }

        private void DrawAt(int x, int y, char c)
        {
            Console.SetCursorPosition(y, x);
            Console.Write(c);
        }
        
        private void Draw()
        {
            Console.Clear();
            foreach (var row in _board.Board)
            {
                foreach (var cell in row)
                {
                    if (cell.X == _board.Hero.X && cell.Y == _board.Hero.Y)
                    {
                        Console.Write((char) _board.Hero.Type);
                    }
                    else
                    {
                        Console.Write((char) cell.Type);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}