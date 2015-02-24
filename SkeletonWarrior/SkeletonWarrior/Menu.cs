//The menu the player sees when he starts the game. Contains the functions for starting new game, difficulty level, leaderboards, credits, exit game
using System;
using System.Threading;

namespace SkeletonWarrior
{
    class Menu
    {
        private static string[] menus =  {"Start New Game", "Change Difficulty", "Leaderboards", "Credits", "Exit Game"};
        private static int currentSelection = 0;
        private static int menuSelected = 0;
        private static string characterName;
        private static bool playing = true;

        public static void Show()
        {
            //Runs in the beginning of the game.
        }
        public static void StartGame()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 25, Console.WindowHeight / 2);
            Console.Write("Enter character name: ");
            characterName = Console.ReadLine();

            Console.Clear();

            Player player = new Player(Console.WindowWidth / 2, Console.WindowHeight / 2, 1, 5, 2, 1, 10);
            player.PlayerModel = "-.☺.-";
            while(playing)
            {
                Console.SetCursorPosition(player.X - player.PlayerModel.Length / 2 + 1, player.Y);
                Console.Write(player.PlayerModel);
                player.MoveAndShoot();
                player.HandleBullets();
                Thread.Sleep(10);
                Console.Clear();
            }
        }

        private static void ChangeDifficultyLevel()
        {

        }

        private static void ShowLeaderboards()
        {
            
        }

        private static void ShowCredits()
        {

        }

        private static void ExitGame()
        {
            Environment.Exit(0);
        }
    }
}
