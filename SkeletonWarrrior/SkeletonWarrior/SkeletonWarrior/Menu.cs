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
        private static string characterName = "";
        private static bool playing = true;
        private static Random enemySpawner = new Random();
        private static readonly string selector = ">";
        private static int mover = 0;
        private static readonly string failedAttemptsPath = "failed.txt";
        private static readonly string successAttemptsPath = "successful.txt";

        public static int Mover
        {
            get { return mover; }
            set { mover = value; }
        }
        public static void Show()
        {
            ShowLogo();
            while (true)
            {
                ShowMenu();
                HandleInput();
                Thread.Sleep(75);
            }
        }

        private static void ShowLogo()
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
        }
        public static void StartGame()
        {
            while (characterName == "")
            {
                Console.Clear();
                Console.SetCursorPosition(Console.WindowWidth / 2 - 25, Console.WindowHeight / 2);
                Console.Write("Enter character name: ");
                characterName = Console.ReadLine();   
            }

            Console.Clear();

            Player player = new Player(Console.WindowWidth / 2, Console.WindowHeight / 2, 1, 1, 1, 20);
            player.PlayerModel = "-.☺.-";
            Player.PlayerLevel = 1;
            Thread moveThread = new Thread(player.MoveAndShoot);
            moveThread.Start();

            Thread shootThread = new Thread(player.ShootAndMoveBullets);
            shootThread.Start();
            //ResetGame(); //TODO: FIX THIS
            while (playing)
            {
                player.CheckWallCollision();
                Console.SetCursorPosition(player.X - player.PlayerModel.Length / 2 + 1, player.Y);
                ConsoleColor foreground = Console.ForegroundColor;
                Console.ForegroundColor = Player.playerColor;
                Console.Write(player.PlayerModel);
                Console.ForegroundColor = foreground;
                player.PrintPlayerStats();
                int determiner = enemySpawner.Next(1, 250);

                if (determiner == 1)
                {
                    GameLogic.EnemyList.Add(new Enemy(1, 1, 2, 2, '0')); //normal
                }
                else if (determiner == 2)
                {
                    GameLogic.EnemyList.Add(new Enemy(1, 1, 2, 2, '*')); // shoots
                }                                      
                else if (determiner == 3)              
                {                                      
                    GameLogic.EnemyList.Add(new Enemy(1, 2, 2, 2, '=')); // hits harder
                }                                      
                else if (determiner == 4)              
                {                                      
                    GameLogic.EnemyList.Add(new Enemy(1, 1, 2, 5, '&')); //tank
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
                        player.Health -= enemy.AttackPower;
                        break;
                    }
                }
                if (player.Health <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 10);
                    Console.Write("YOU LOST.");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 8);
                    Console.Write("SCORE: " + Player.Score);
                    SetScoreToFailedDatabase(player);
                    bool gameQuit = false;
                    while (true)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter)
                        {
                            gameQuit = true;
                            break;
                        }
                    }
                    if (gameQuit)
                    {
                        break;
                    }
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
                    bullet.PrintBullets();

                    if ((bullet.Friendly == false) &&
                        (player.X - 3 <= bullet.X &&
                        player.X + 5 >= bullet.X) &&
                        (player.Y <= bullet.Y &&
                         player.Y >= bullet.Y))
                    {
                        GameLogic.ShotBullets.Remove(bullet);
                        player.Health -= GameLogic.EnemyList[0].AttackPower;
                        break;
                    }
                    bool removed = false;
                    if (bullet.Friendly == true) // remove two bullets when they collide
                    {
                        foreach (var enemyBullet in GameLogic.ShotBullets.ToList())
                        {
                            if ((enemyBullet.Friendly == false) &&
                                bullet.X == enemyBullet.X &&
                                bullet.Y == enemyBullet.Y)
                            {
                                GameLogic.ShotBullets.Remove(bullet);
                                GameLogic.ShotBullets.Remove(enemyBullet);
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

                if (player.Collisions == 5 + Player.PlayerLevel)
                {
                    player.UpdateStatsOnLevelUp();
                }
                //Enemy.GetBoss();
                Thread.Sleep(20);
                Console.Clear();
            }
        }
 
        /// <summary>
        /// Method storing failed attempts into database file
        /// </summary>
        /// <param name="player"></param>
        private static void SetScoreToFailedDatabase(Player player)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(failedAttemptsPath, true))
                {
                    sw.WriteLine(characterName + "\t\t" + Player.Score);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Can't load failed database to store record!");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Can't open directory to find failed database!");
            }
        }

        private static void ResetGame()
        {
            GameLogic.EnemyList = new System.Collections.Generic.List<Enemy>();
            GameLogic.ShotBullets = new System.Collections.Generic.List<Bullet>();
            Player.Score = 0;
            Player.PlayerLevel = 1;
            Enemy.EnemyLevel = 1;
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
                    Console.SetCursorPosition(Console.WindowWidth / 2 - menu[i].Length / 2 - selector.Length + 1, Console.WindowHeight / 2 + 2 * i);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(selector + menu[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - menu[i].Length / 2, Console.WindowHeight / 2 + 2 * i);
                    Console.Write(" " + menu[i]);
                }
            }
        }

        private static void ChangeDifficultyLevel()
        {
            Console.Clear();
            //TODO: Change current dificulty
            Console.WriteLine(@"                           _____                 _                                     
                          / ____|               (_)                                    
                          | |     ___  _ __ ___  _ _ __   __ _   ___  ___   ___  _ __  
                          | |    / _ \| '_ ` _ \| | '_ \ / _` | / __|/ _ \ / _ \| '_ \ 
                          | |___| (_) | | | | | | | | | | (_| | \__ \ (_) | (_) | | | |
                           \_____\___/|_| |_| |_|_|_| |_|\__, | |___/\___/ \___/|_| |_|
                                                          __/ |                        
                                                         |___/                         ");
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

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Console.WindowWidth / 6, Console.WindowHeight / 6);
            Console.WriteLine("Failed Attempts:");
            try
            {
                using (var reader = new StreamReader(failedAttemptsPath))
                {
                    int failedCount = 1;
                    Console.WriteLine();
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
                using (var reader1 = new StreamReader(successAttemptsPath))
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
            Thread skull = new Thread(Music);
            skull.Start();

            StreamReader szarvas = new StreamReader(@"..\..\skull.txt");
            string szarvasstring = szarvas.ReadToEnd();
            StreamReader szarvas2 = new StreamReader(@"..\..\skull2.txt");
            string szarvasstring2 = szarvas2.ReadToEnd();
            szarvas.Close();
            szarvas2.Close();
            int i = 0;
            while ((i < szarvasstring.Length) && (szarvasstring[i] != 'Q'))
            {
                i++;
            }

            int l = 0;
            while ((l < szarvasstring2.Length) && (szarvasstring2[l] != 'Q'))
            {
                l++;
            }

            int k = 0;
            while ((k < szarvasstring2.Length) && (szarvasstring2[k] != 'O'))
            {
                k++;
            }

            for (int j = 0; j < 13; j++)
            {
                Console.Write(szarvasstring.Substring(0, i - 1));
                Console.Write(szarvasstring[i]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(szarvasstring.Substring(i + 1));

                Thread.Sleep(500);

                Console.Clear();

                Console.Write(szarvasstring2.Substring(0, l - 1));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(szarvasstring2[l]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(szarvasstring2.Substring(l + 1, szarvasstring2.Length - l - (szarvasstring2.Length - k) - 1));
                Console.Write(szarvasstring2[k]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(szarvasstring2.Substring(k + 1));

                Thread.Sleep(500);

                Console.Clear();

            }

            Console.Write(szarvasstring.Substring(0, i - 1));
            Console.Write(szarvasstring[i]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(szarvasstring.Substring(i + 1));
            
            Console.ReadKey();
            BackToMenu();
        }

        /// <summary>
        /// Return back to main menu
        /// </summary>
        private static void BackToMenu()
        {
            Console.Clear();
            ShowLogo();
            ShowMenu();
        }

        #region musicForTheCredits
        static void Music()
        {
            Console.Beep(440, 500);
            Console.Beep(440, 500);
            Console.Beep(440, 500);
            Console.Beep(349, 350);
            Console.Beep(523, 150);
            Console.Beep(440, 500);
            Console.Beep(349, 350);
            Console.Beep(523, 150);
            Console.Beep(440, 1000);
            for (int i = 0; i < 2; i++)
            {
                Console.Beep(659, 500);
                Console.Beep(659, 500);
                Console.Beep(659, 500);
                Console.Beep(698, 350);
                Console.Beep(523, 150);
                Console.Beep(415, 500);
                Console.Beep(349, 350);
                Console.Beep(523, 150);
                Console.Beep(440, 1000);
            }

            Console.ReadKey();
            BackToMenu();

        }
        static void Kurzor(int j)
        {
            Console.SetCursorPosition(2 * j + 2, 0);
        }
        #endregion
    }
}
