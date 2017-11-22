using System;
using MiniRoguelike.Util;

namespace MiniRoguelike
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            switch (args.Length)
            {
                case 1:
                    new MiniRoguelikeGame(GameBoard.From(args[0])).Play();
                    break;
                case 2:
                    new MiniRoguelikeGame(
                        GameBoardGenerator.GenerateGameBoard(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]))
                    ).Play();
                    break;
                default:
                    throw new ArgumentException("type gameboard file or gameboard dimensions");
            }
        }
    }
}