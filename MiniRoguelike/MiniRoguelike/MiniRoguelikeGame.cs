using System;

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
            eventLoop.UpHandler += MoveUp;
            eventLoop.DownHandler += MoveDown;
            eventLoop.LeftHandler += MoveLeft;
            eventLoop.RightHandler += MoveRight;
            eventLoop.EscHandler += Console.Clear;
            
            Redraw();
            eventLoop.Run();
        }
        
        private void MoveRight()
        {
            _board.MoveHero(_board.Hero.X, _board.Hero.Y + 1);
            Redraw();
        }
        
        private void MoveLeft()
        {
            _board.MoveHero(_board.Hero.X, _board.Hero.Y - 1);
            Redraw();
        }
        
        private void MoveUp()
        {
            _board.MoveHero(_board.Hero.X - 1, _board.Hero.Y);
            Redraw();
        }

        private void MoveDown()
        {
            _board.MoveHero(_board.Hero.X + 1, _board.Hero.Y);
            Redraw();
        }

        private void Redraw()
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