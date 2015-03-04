// This class will handle the game logic and how everything updates
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;

namespace SkeletonWarrior
{
    class GameLogic
    {

        private static List<Enemy> enemyList = new List<Enemy>();
        private static List<Bullet> shotBullets = new List<Bullet>();
        private static bool playing = true;
        private static Random enemySpawner = new Random();
        private static int mover = 0;
        private static readonly string failedAttemptsPath = "failed.txt";
        private static readonly string successAttemptsPath = "successful.txt";
        private static bool bossTime = true;

        private ConsoleColor playerColor = ConsoleColor.Cyan;

        public static string FailedAttemptsPath
        {
            get { return failedAttemptsPath; }
        }

        public static string SuccessAttemptsPath
        {
            get { return successAttemptsPath; }
        }

        public static List<Enemy> EnemyList
        {
            get { return enemyList; }
            set { enemyList = value; }
        }
        public static List<Bullet> ShotBullets
        {
            get { return shotBullets; }
            set { shotBullets = value; }
        }
        public ConsoleColor PlayerColor
        {
            get { return this.playerColor; }
            set { this.playerColor = value; }
        }

        public static void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(Console.BufferWidth - 120 + x, Console.BufferHeight - 40 + y);
        }
        public static int Mover
        {
            get { return mover; }
            set { mover = value; }
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
                    sw.WriteLine(Menu.CharacterName + "\t\t" + Player.Score);
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
            // Starts a new instance of the program itself
            System.Diagnostics.Process.Start("SkeletonWarrior.exe");
            Environment.Exit(0);
        }


        public static void StartGame()
        {
            Player player = new Player(Console.WindowWidth / 2, Console.WindowHeight / 2, 1, 1, 1, 20);
            player.PlayerModel = "-.☺.-";
            Player.PlayerLevel = 1;
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
                        ResetGame();
                    }
                }

                if (Player.PlayerLevel >= 3)
                {
                    Enemy.GetBoss();
                    if (bossTime)
                    {
                        for (int i = 0; i < GameLogic.EnemyList.Count; i++)
                        {
                            EnemyList[i].Health += 15;
                        }
                        bossTime = false;
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

                    if (Player.PlayerLevel % 3 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (Player.PlayerLevel % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                }
                //Enemy.GetBoss();
                Thread.Sleep(20);
                Console.Clear();
            }
        }
    }
}
