using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MiniRoguelike
{
    public class GameBoard
    {   
        public Cell Hero { get; }
        public IReadOnlyList<IReadOnlyList<Cell>> Board { get; }

        public GameBoard(IReadOnlyList<IReadOnlyList<Cell>> board, Cell hero)
        {
            Board = board;
            Hero = hero;
        }

        public void MoveHero(int x, int y)
        {
            if (x < 0 || x >= Board.Count 
                || y < 0 || y >= Board[0].Count 
                || Board[x][y].Type != CellType.Free)
            {
                return;
            }
            
            Hero.X = x;
            Hero.Y = y;
        }

        public static GameBoard From(string filePath)
        {
            var field = File.ReadLines(filePath)
                .Select((line, i) => line.Select((c, j) => new Cell(i, j, CreateCellType(c))).ToList())
                .ToList();
            
            var hero = field.SelectMany(list => list).First(cell => cell.Type == CellType.Hero);
            field[hero.X][hero.Y].Type = CellType.Free;
            
            return new GameBoard(field, new Cell(hero.X, hero.Y, CellType.Hero));
        }

        private static CellType CreateCellType(char c)
        {
            switch (c)
            {
                case (char) CellType.Wall: 
                    return CellType.Wall;
                case (char) CellType.Free: 
                    return CellType.Free;
                case (char) CellType.Hero:
                    return CellType.Hero;
                default: 
                    throw new ArgumentException();
            }
        }
        
        public class Cell
        {
            public int X { get; internal set; }
            public int Y { get; internal set; }
            public CellType Type { get; internal set; }

            public Cell(int x, int y, CellType type)
            {
                X = x;
                Y = y;
                Type = type;
            }            
        }

        public enum CellType
        {
            Hero = '@',
            Wall = '*',
            Free = ' '
        }
    }
}