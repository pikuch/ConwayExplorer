using System;
using System.Collections.Generic;

namespace ConwayExplorer
{
    class Program
    {
        private static List<Experiment> Experiments = new List<Experiment>();
        public static int ExperimentLength { get; set; } = 100;

        private static Menu InitializeMenu()
        {
            Menu mainMenu = new Menu("Choose an option:");
            mainMenu.AddItem(1, "Show known patterns");
            mainMenu.AddItem(2, "Run a random experiment");
            mainMenu.AddItem(3, "Set simulation steps");
            mainMenu.AddItem(4, "Exit");
            return mainMenu;
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Menu mainMenu = InitializeMenu();
            bool timeToExit = false;

            while (!timeToExit)
            {
                int choice = mainMenu.Use();

                switch (choice)
                {
                    case 1:
                        PatternList();
                        break;
                    case 2:
                        GameOfLife game = new GameOfLife(new GameBoard().Randomize());
                        Experiments.Add(game.RunAndShow(ExperimentLength));
                        break;
                    case 3:
                        SetSimulationSteps();
                        break;
                    case 4:
                        timeToExit = true;
                        break;
                    default:
                        // Impossible!
                        break;
                }
            }

        }

        private static void PatternList()
        {
            int cursorPosition = 0;
            string margin = "   ";
            string cursor = " > ";

            Console.Clear();

            if (Experiments.Count == 0)
            {
                Console.WriteLine("There are no patterns to show!");
                System.Threading.Thread.Sleep(1000);
                return;
            }

            Console.WriteLine(margin + "Known patterns: (ESC to exit)");
            for (int pos = 0; pos < Experiments.Count; pos++)
            {
                if (pos == cursorPosition)
                {
                    Console.WriteLine(cursor + Experiments[pos].ToString());
                }
                else
                {
                    Console.WriteLine(margin + Experiments[pos].ToString());
                }
            }

            while (true)
            {
                var keyInfo = Console.ReadKey(true).Key;

                if (keyInfo == ConsoleKey.UpArrow || keyInfo == ConsoleKey.DownArrow)
                {
                    int positionChange;
                    Console.SetCursorPosition(0, cursorPosition + 1);
                    Console.Write(margin);
                    if (keyInfo == ConsoleKey.UpArrow)
                    {
                        positionChange = -1;
                    }
                    else
                    {
                        positionChange = 1;
                    }
                    cursorPosition = (cursorPosition + positionChange + Experiments.Count) % Experiments.Count;
                    Console.SetCursorPosition(0, cursorPosition + 1);
                    Console.Write(cursor);
                }
                else if (keyInfo == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
        private static void SetSimulationSteps()
        {
            Console.Clear();
            Console.CursorVisible = true;
            int answer = 0;
            while (true)
            {
                Console.Write("Choose the number of simulation steps: ");
                bool result = int.TryParse(Console.ReadLine(), out answer);
                if (!result)
                {
                    Console.WriteLine("Not a number.");
                }
                else if (answer <= 0)
                {
                    Console.WriteLine("Tne number of simulation steps has to be > 0.");
                }
                else
                {
                    ExperimentLength = answer;
                    break;
                }
            }
            Console.CursorVisible = false;
        }

    }
}
