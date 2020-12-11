using System;
using System.Collections.Generic;

namespace ConwayExplorer
{
    internal class Menu
    {
        private string Prompt;
        private List<MenuItem> Items;
        private int CursorPosition;

        public Menu(string prompt) 
        {
            Prompt = prompt;
            Items = new List<MenuItem>();
            CursorPosition = 0;
        }

        public void AddItem(int id, string name)
        {
            Items.Add(new MenuItem() { Id = id, Name = name });
        }

        internal int Use()
        {
            string margin = "   ";
            string cursor = " > ";

            Console.Clear();
            Console.WriteLine(margin + Prompt);
            for (int pos=0; pos<Items.Count; pos++)
            {
                if (pos == CursorPosition)
                {
                    Console.WriteLine(cursor + Items[pos].Name);
                }
                else
                {
                    Console.WriteLine(margin + Items[pos].Name);
                }
            }

            while (true)
            {
                var keyInfo = Console.ReadKey(true).Key;

                if (keyInfo == ConsoleKey.UpArrow || keyInfo == ConsoleKey.DownArrow)
                {
                    int positionChange;
                    Console.SetCursorPosition(0, CursorPosition + 1);
                    Console.Write(margin);
                    if (keyInfo == ConsoleKey.UpArrow)
                    {
                        positionChange = -1;
                    }
                    else
                    {
                        positionChange = 1;
                    }
                    CursorPosition = (CursorPosition + positionChange + Items.Count) % Items.Count;
                    Console.SetCursorPosition(0, CursorPosition + 1);
                    Console.Write(cursor);
                }
                else if (keyInfo == ConsoleKey.Enter)
                {
                    return Items[CursorPosition].Id;
                }

            }

        }
    }
}