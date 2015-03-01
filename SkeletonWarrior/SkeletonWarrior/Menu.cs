//The menu the player sees when he starts the game. Contains the functions for starting new game, difficulty level, leaderboards, credits, exit game
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
        private static readonly string[] menu = { "Start New Game", "Change Difficulty", "Leaderboards", "Credits", "Exit Game" };

        private static int currentSelection = 0;
        private static int menuSelected = 0;
        private static string characterName;
        private static bool playing = true;
        private static Random enemySpawner = new Random();
        private static string selector = ">";
        private static int mover = 0;

        public static int Mover
        {
            get { return mover; }
            set { mover = value; }
        }
        public static void Show()
        {
            while (true)
            {
                ShowMenu();
                HandleInput();
                Thread.Sleep(20);
                Console.Clear();
            }
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

                if (GameLogic.EnemyList.Count > 0)
                {
                    mover++;
                }
                bool moving = false;
                foreach (var enemy in GameLogic.EnemyList)
                {
                    enemy.WriteEnemyOnScreen();
                    if (mover == 11)
                    {
                        if (enemy.EnemyType != '*')
                        {
                            enemy.Move(player);
                        }
                        else
                        {
                            enemy.Shoot(player);
                        }
                        moving = true;
                    }
                }
                if (moving)
                {
                    mover = 0;
                }
                foreach (var bullet in GameLogic.ShotBullets)
                {
                    if (bullet.BulletCollisionCheck(player))
                    {
                        break;
                    }
                }

                //Enemy.GetBoss(Enemy.GetBossFile);
                Thread.Sleep(20);
                Console.Clear();
            }
        }

        /// <summary>
        /// Show main menu
        /// </summary>
        private static void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                    currentSelection--;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    currentSelection++;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    CheckUserChoice();
                }

                if (currentSelection < 0)
                {
                    currentSelection = menu.GetLength(0) - 1;
                }
                else if (currentSelection > menu.GetLength(0) - 1)
                {
                    currentSelection = 0;
                }
            }
        }

        private static void CheckUserChoice()
        {
            switch (currentSelection)
            {
                case 0:
                    StartGame();
                    break;
                case 1:
                    ChangeDifficultyLevel();
                    break;
                case 2:
                    ShowLeaderboards();
                    break;
                case 3:
                    ShowCredits();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }

        public static void ShowMenu()
        {

            for (int i = 0; i < menu.GetLength(0); i++)
            {
                if (currentSelection == i)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - menu[i].Length / 2 - selector.Length, Console.WindowHeight / 2 + 2 * i);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(selector + menu[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - menu[i].Length / 2, Console.WindowHeight / 2 + 2 * i);
                    Console.Write(menu[i]);
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
            //TODO: show leaders board;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Console.WindowWidth / 6, Console.WindowHeight / 6);
            Console.WriteLine("Failed Attempts:");
            try
            {
                using (var reader = new StreamReader(@"..\..\failed.txt"))
                {
                    int failedCount = 1;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.SetCursorPosition(Console.WindowWidth / 6, Console.WindowHeight / 5 + counter);
                        Console.WriteLine(failedCount + ". " + line);
                        counter++;
                        failedCount++;
                    }
                    failedCount = 0;
                    Console.ResetColor();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Can't load failed attempts database!");
            }

            int counter1 = 0;
            string line1;
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 6);
            Console.WriteLine("Successful Attempts:");

            try
            {
                using (var reader1 = new StreamReader(@"..\..\successful.txt"))
                {
                
                    int successCount = 1;
                    while ((line1 = reader1.ReadLine()) != null)
                    {
                        Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 5 + counter1);
                        Console.WriteLine(successCount + ". " + line1);
                        counter1++;
                        successCount++;
                    }
                    successCount = 0;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Can't load successfully attempts database!");
            }
            
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
