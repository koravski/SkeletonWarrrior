//The menu the player sees when he starts the game. Contains the functions for starting new game, difficulty level, leaderboards, credits, exit game
using System;
using System.IO;
using System.Threading;
using System.Linq;

namespace SkeletonWarrior
{
    class Menu
    {
        /// <summary>
        /// Main menu
        /// </summary>
        private static readonly string[] menu = { "Start New Game", "Change Difficulty", "Leaderboards", "Credits", "Exit Game" };

        private static int currentSelection = 0;
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
                Thread.Sleep(75);
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

            Player player = new Player(Console.WindowWidth / 2, Console.WindowHeight / 2, 1, 1, 1, 1, 20);
            player.PlayerModel = "=-.☺.-=";

            Thread moveThread = new Thread(player.MoveAndShoot);
            moveThread.Start();

            Thread shootThread = new Thread(player.ShootAndMoveBullets);
            shootThread.Start();
            while (playing)
            {
                player.CheckWallCollision();
                Console.SetCursorPosition(player.X - player.PlayerModel.Length / 2 + 1, player.Y);
                ConsoleColor foreground = Console.ForegroundColor;
                Console.ForegroundColor = Player.playerColor;
                Console.Write(player.PlayerModel);
                Console.ForegroundColor = foreground;
                //player.MoveAndShoot();
                //player.ShootAndMoveBullets();
                player.PrintPlayerStats();
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
                    if (enemy.X == player.X && 
                        enemy.Y == player.Y)
                    {
                        GameLogic.EnemyList.Remove(enemy);
                        player.Health--;
                        break;
                    }
                }
                if (player.Health == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 10);
                    Console.Write("YOU LOST.");
                    Console.ReadKey();
                    break;
                }
                if (moving)
                {
                    mover = 0;
                }
                foreach (var bullet in GameLogic.ShotBullets.ToList())
                {
                    if (bullet.BulletCollisionCheck(player))
                    {
                        break;
                    }
                    bullet.WallCollisionChecker();
                    bullet.WriteBulletOnScreen();

                    if ((bullet.Friendly == false) &&
                        (player.X - 3 <= bullet.X &&
                        player.X + 5 >= bullet.X) &&
                        (player.Y <= bullet.Y &&
                         player.Y >= bullet.Y))
                    {
                        int indexOfBullet = GameLogic.ShotBullets.IndexOf(bullet);
                        GameLogic.ShotBullets.RemoveAt(indexOfBullet);
                        player.Health--;
                        break;
                    }
                    bool removed = false;
                    if (bullet.Friendly == true)
                    {
                        foreach (var enemyBullet in GameLogic.ShotBullets)
                        {
                            if ((enemyBullet.Friendly == false) &&
                                bullet.X == enemyBullet.X &&
                                bullet.Y == enemyBullet.Y)
                            {
                                int indexOfBullet = GameLogic.ShotBullets.IndexOf(bullet);
                                GameLogic.ShotBullets.RemoveAt(indexOfBullet);
                                indexOfBullet = GameLogic.ShotBullets.IndexOf(enemyBullet);
                                GameLogic.ShotBullets.RemoveAt(indexOfBullet);
                                removed = true;
                                break;
                            }
                        }
                    }
                    if (removed)
                    {
                        break;
                    }
                    
                }

                if (player.Collisions == 5)
                {
                    Player.UpdateStatsOnLevelUp(player);
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
            Console.Write(
@"
                 _________-----_____                  
       _____------           __      ----_            
___----             ___------              \                 ___________          .__          __                 
   ----________        ----                 \               /   _____/  | __ ____ |  |   _____/  |_  ____   ____  
               -----__    |             _____)              \_____  \|  |/ // __ \|  | _/ __ \   __\/  _ \ /    \ 
                    __-                /     \              /        \    <\  ___/|  |_\  ___/|  | (  <_> )   |  \
        _______-----    ___--          \    /)\            /_______  /__|_ \\___  >____/\___  >__|  \____/|___|  /
  ------_______      ---____            \__/  /                    \/     \/    \/          \/                 \/ 
               -----__    \ --    _          /\             __      __                     .__              
                      --__--__     \_____/   \_/\          /  \    /  \_____ ______________|__| ___________ 
                              ----|   /          |         \   \/\/   /\__  \\_  __ \_  __ \  |/  _ \_  __ \
                                  |  |___________|          \        /  / __ \|  | \/|  | \/  (  <_> )  | \/
                                  |  | ((_(_)| )_)           \__/\  /  (____  /__|   |__|  |__|\____/|__|
                                  |  \_((_(_)|/(_)
                                  \             (
                                   \_____________)");

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
            string endNames = "The participants in \"Skeleton Warrior\" game project:\n";

            string[] arrayOfNames = new string[]{
                "Nikolay Karagyozov",
                "Ivailo Kolarov",
                "Pavlina Dragneva",
                "Ivan Donchev",
                "Vasil Todorov",
                "Krasimir Georgiev",
                "Stefan  Dimitrov",
                "Kaloian Koravski",
            };

            Array.Sort(arrayOfNames);

            Console.WriteLine();
            Console.WriteLine("{0}", endNames);
            foreach (var day in arrayOfNames)
            {
                Console.WriteLine("{0}\n", day);
            }

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
    }
}
