using System;
using System.Collections.Generic;

namespace ConwayExplorer
{
    internal class GameOfLife
    {

        private GameBoard Seed;
        private GameBoard Board;
        private List<(int, int, byte)> Changes = new List<(int, int, byte)>();
        private Dictionary<long, int> SeenStates = new Dictionary<long, int>();

        public GameOfLife(GameBoard seed)
        {
            Seed = seed;
            Board = new GameBoard(seed);
        }

        public Experiment RunAndShow(int maxGenerations = 1000)
        {
            DrawBoard();
            SeenStates.Add(Board.GetHash(), 0);
            int generation = 0;
            while (generation < maxGenerations)
            {
                generation++;
                CalculateChanges();
                ApplyAndShowChanges();
                uint newHash = Board.GetHash();
                if (SeenStates.ContainsKey(newHash))
                {
                    return new Experiment()
                    {
                        Tested = true,
                        TestLimit = maxGenerations,
                        Hash = Seed.GetHash(),
                        Pattern = Seed,
                        LoopFound = true,
                        LoopStart = SeenStates[newHash],
                        LoopLength = generation - SeenStates[newHash]
                    };
                }
                else
                {
                    SeenStates.Add(newHash, generation);
                }
                WriteGenerationNumber(generation, maxGenerations);
            }
            return new Experiment() 
            {
                Tested = true,
                TestLimit = maxGenerations,
                Hash = Seed.GetHash(),
                Pattern = Seed,
                LoopFound = false,
                LoopStart = 0,
                LoopLength = 0
            };
        }


        private void DrawBoard(int startX = 0, int startY = 0)
        {
            Console.Clear();
            char borderChar = '▒';
            for (int x = 0; x < GameBoard.Size + 2; x++)
                for (int halfY = 0; halfY < GameBoard.Size / 2 + 2; halfY++)
                {
                    if (x == 0 || x == GameBoard.Size + 1 || halfY == 0 || halfY == GameBoard.Size/2 + 1)
                    {
                        Console.SetCursorPosition(x + startX, halfY + startY);
                        Console.Write(borderChar);
                    }
                }
            for (int y = 0; y < GameBoard.Size / 2; y++)
                for (int x = 0; x < GameBoard.Size; x++)
                    DrawField(x, y*2, startX, startY);

            // Write hash
            Console.SetCursorPosition(2 + startX, startY);
            Console.Write($"Seed: {Seed.GetHash()}");
        }

        private void WriteGenerationNumber(int current, int maxGen, int startX = 0, int startY = 0)
        {
            Console.SetCursorPosition(2 + startX, GameBoard.Size / 2 + 1 + startY);
            Console.Write($"Generation: {current}/{maxGen}");
        }

        private void DrawField(int x, int y, int startX = 0, int startY = 0)
        {
            if (y%2 == 1)
            {
                y--;
            }
            Console.SetCursorPosition(1 + x + startX, 1 + y/2 + startY);
            if (Board.Fields[x, y] == 0)
            {
                if (Board.Fields[x, y + 1] == 0)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write("▄");
                }
            }
            else
            {
                if (Board.Fields[x, y + 1] == 0)
                {
                    Console.Write("▀");
                }
                else
                {
                    Console.Write("█");
                }
            }
                    
        }

        private void CalculateChanges()
        {
            Changes.Clear();
            for (int x = 0; x < GameBoard.Size; x++)
                for (int y = 0; y < GameBoard.Size; y++)
                {
                    if (Board.Fields[x, y] == 0)  // an empty cell
                    {
                        if (Board.CountNeighbours(x, y) == 3)
                        {
                            Changes.Add((x, y, 1));
                        }
                    }
                    else  // a living cell
                    {
                        int neighbours = Board.CountNeighbours(x, y);
                        if (neighbours != 2 && neighbours != 3)
                        {
                            Changes.Add((x, y, 0));
                        }
                    }
                }
        }
        private void ApplyChanges()
        {
            foreach (var (x, y, newValue) in Changes)
            {
                Board.Fields[x, y] = newValue;
            }
        }
        private void ApplyAndShowChanges()
        {
            foreach (var (x, y, newValue) in Changes)
            {
                Board.Fields[x, y] = newValue;
                DrawField(x, y);
            }
        }

    }
}