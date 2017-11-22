using System;
using System.Collections.Generic;

namespace MiniRoguelike.Util
{
    public static class GameBoardGenerator
    {
        public static GameBoard GenerateGameBoard(int rows, int columns)
        {
            var board = new List<List<GameBoard.Cell>>();
            var random = new Random();
            for (var i = 0; i < rows; ++i)
            {
                board.Add(new List<GameBoard.Cell>());
                for (var j = 0; j < columns; ++j)
                {
                    board[i].Add(new GameBoard.Cell(i, j, GenerateCellType(random)));
                }
            }
            
            while (true)
            {
                var x = random.Next(rows);
                var y = random.Next(columns);
                if (board[x][y].Type == GameBoard.CellType.Free)
                {
                    return new GameBoard(board, new GameBoard.Cell(x, y, GameBoard.CellType.Hero));
                }
            }
        }

        private static GameBoard.CellType GenerateCellType(Random random)
            => random.Next(10) > 7 ? GameBoard.CellType.Wall : GameBoard.CellType.Free;
    }
}