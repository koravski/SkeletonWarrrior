﻿//The menu the player sees when he starts the game. Contains the functions for starting new game, difficulty level, leaderboards, credits, exit game
using System;
using System.IO;
using System.Threading;

namespace SkeletonWarrior
{
    class Menu
    {
        /// <summary>
        /// Main menu
        /// </summary>
        private static readonly string[] menus = { "Start New Game", "Change Difficulty", "Leaderboards", "Credits", "Exit Game" };

        private static int currentSelection = 0;
        private static int menuSelected = 0;
        private static string characterName;
        private static bool playing = true;
        private static Random enemySpawner = new Random();

        public static void Show()
        {
            //Runs in the beginning of the game.
        }
        public static void StartGame()
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 25, Console.WindowHeight / 2);
            Console.Write("Enter character name: ");
            characterName = Console.ReadLine();

            Console.Clear();

            Player player = new Player(Console.WindowWidth / 2, Console.WindowHeight / 2, 1, 5, 2, 1, 10);
            player.PlayerModel = "=-.☺.-=";
            while (playing)
            {
                Console.SetCursorPosition(player.X - player.PlayerModel.Length / 2 + 1, player.Y);
                ConsoleColor background = Console.ForegroundColor;
                Console.ForegroundColor = Player.playerColor;
                Console.Write(player.PlayerModel);
                Console.ForegroundColor = background;
                player.MoveAndShoot();
                player.ShootAndMoveBullets();

                int determiner = enemySpawner.Next(1, 250);

                if (determiner == 1)
                {
                    GameLogic.EnemyList.Add(new Enemy(1, 2, 2, 1, 5, '0'));
                }
                else if (determiner == 2)
                {
                    GameLogic.EnemyList.Add(new Enemy(1, 2, 2, 1, 5, '*'));
                }
                else if (determiner == 3)
                {
                    GameLogic.EnemyList.Add(new Enemy(1, 2, 2, 1, 5, '='));
                }
                else if (determiner == 4)
                {
                    GameLogic.EnemyList.Add(new Enemy(1, 2, 2, 1, 5, '&'));
                }
                foreach (var enemy in GameLogic.EnemyList)
                {
                    enemy.WriteEnemyOnScreen();
                }
                foreach (var bullet in GameLogic.ShotBullets)
                {
                    if (bullet.BulletCollisionCheck())
                    {
                        break;
                    }
                }

                Thread.Sleep(20);
                Console.Clear();
            }
        }

        /// <summary>
        /// Show main menu
        /// </summary>
        public static void ShowMenu()
        {
            for (int i = 1; i <= menus.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 25, Console.WindowHeight / 8 + i);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0}.{1}", i, menus[i-1]);
            }

            bool choice = false;
            while (!choice)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                            StartGame();
                            choice = true;
                            break;
                        case ConsoleKey.D2:
                            ChangeDifficultyLevel();
                            choice = true;
                            break;
                        case ConsoleKey.D3:
                            ShowLeaderboards();
                            choice = true;
                            break;
                        case ConsoleKey.D4:
                            ShowCredits();
                            choice = true;
                            break;
                        case ConsoleKey.D5:
                            ExitGame();
                            break;
                        default:
                            choice = false;
                            break;
                    }
                }
            }
        }

        private static void ChangeDifficultyLevel()
        {
            Console.Clear();
            //TODO: Change current dificulty

            Console.ReadKey();
            BackToMenu();
        }

        /// <summary>
        /// Read from file all leaders and show them on console
        /// </summary>
        private static void ShowLeaderboards()
        {
            Console.Clear();
            int counter = 0;
            string line;
            
            
           
            Console.SetCursorPosition(Console.WindowWidth / 6, Console.WindowHeight/6);
            Console.WriteLine("Failed Attempts:");
            var reader = new StreamReader(@"..\..\file.txt");
            int failedCount = 1;
            while ((line = reader.ReadLine()) != null)
            {
                Console.SetCursorPosition(Console.WindowWidth / 6, Console.WindowHeight / 5  + counter);
                Console.WriteLine(failedCount + ". " + line);
                counter++;
                failedCount++;
            }
            reader.Close();
            failedCount = 0;
            Console.ResetColor();
            
            int counter1 =  0;
            string line1;
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 6);
            Console.WriteLine("Successful Attempts:");
            var reader1 = new StreamReader(@"..\..\file.txt");
            int successCount = 1;
            while ((line1 = reader1.ReadLine()) != null)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 5  +  counter1);
                Console.WriteLine(successCount + ". " + line1);
                counter1++;
                successCount++;
            }
            reader.Close();
            successCount = 0;
            
            Console.ReadKey();
            BackToMenu();
        }

        /// <summary>
        /// Show credits and final text
        /// </summary>
        private static void ShowCredits()
        {
            Console.Clear();
            //TODO: show text
            Console.WriteLine("Credits:");
            
            Console.ReadKey();
            BackToMenu();
        }

        /// <summary>
        /// Return back to main menu
        /// </summary>
        private static void BackToMenu()
        {
            Console.Clear();
            ShowMenu();
        }

        /// <summary>
        /// Exit game!
        /// </summary>
        private static void ExitGame()
        {
            Environment.Exit(0);
        }
    }
}
