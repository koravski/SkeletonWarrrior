//The main class where the game will run.
using System;

namespace SkeletonWarrior
{
    class SWMain
    {
        static void Main()
        {
            SetInitialConsoleSettings();
            Menu.StartGame();
        }
        private static void SetInitialConsoleSettings()
        {
            Console.SetWindowSize(120, 40);
            Console.CursorVisible = false;
            Console.Title = "Skeleton Warrior";
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }
    }
}
