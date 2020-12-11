using System;

namespace ConwayExplorer
{
    class GameBoard
    {
        public const int Size = 40;
        public byte[,] Fields = new byte[Size, Size];

        public GameBoard() {}

        public GameBoard(GameBoard other)
        {
            System.Array.Copy(other.Fields, Fields, Fields.Length);
        }

        public GameBoard Randomize()
        {
            Random generator = new Random();
            for (int x = 0; x < Size; x++)
                for (int y = 0; y < Size; y++)
                {
                    Fields[x, y] = (generator.Next(100) < 50) ? (byte)0 : (byte)1;
                }
            return this;
        }

        public uint GetHash()
        {
            uint hashValue = 0;
            foreach (byte field in Fields)
            {
                hashValue = 23 * hashValue + field;
            }
            return hashValue;
        }

        internal int CountNeighbours(int x, int y)
        {
            int counter = 0;
            for (int dx= -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                    {
                        continue;
                    }
                    else
                    {
                        counter += Fields[(x + dx + Size) % Size, (y + dy + Size) % Size];
                    }
                }
            return counter;
        }
    }
}
