//The main class where the game will run.
using System;

namespace SkeletonWarrior
{
    class SWMain
    {
        static void Main()
        {
            SetInitialConsoleSettings();
        }

        private static void SetInitialConsoleSettings()
        {
            Console.SetWindowSize(120, 40);
            Console.Title = "Skeleton Warrior";
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }
    }
}
