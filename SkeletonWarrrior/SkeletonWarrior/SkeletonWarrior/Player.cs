//A class for the player
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace SkeletonWarrior
{
    class Player
    {
        //some changes
        private int movementSpeed;
        private int attackPower;
        private int firingSpeed;
        private static int playerLevel;
        private int health;
        private string playerModel;
        private int x;
        private int y;
        public static ConsoleColor playerColor = ConsoleColor.Blue;
        private int collisions;
        private static int score = 0;

        public  int Collisions
        {
            get { return collisions; }
            set { collisions = value; }
        }

        public static int Score
        {
            get { return score; }
            set { score = value; }
        }
        public Player(int x, int y, int movementSpeed, int attackPower, int firingSpeed, int health)
        {
            this.x = x;
            this.y = y;
            this.movementSpeed = movementSpeed;
            this.attackPower = attackPower;
            this.firingSpeed = firingSpeed;
            this.health = health;
        }

        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }
        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }
        public int MovementSpeed
        {
            get { return this.movementSpeed; }
            set { this.movementSpeed = value; }
        }
        
        public int AttackPower
        {
            get { return this.attackPower; }
            set { this.attackPower = value; }
        }
        public int FiringSpeed
        {
            get { return this.firingSpeed; }
            set { this.firingSpeed = value; }
        }
        public static  int PlayerLevel
        {
            get { return playerLevel; }
            set { playerLevel = value; }
        }
        public int Health
        {
            get { return this.health; }
            set { this.health = value; }
        }
        public string PlayerModel
        {
            get { return this.playerModel; }
            set { this.playerModel = value; }
        }

        public void MoveAndShoot()
        {
            while (true)
            {
                if (movementSpeed <= 10)
                {
                    Thread.Sleep(200 - 18*movementSpeed);
                }
                else
                {
                    Thread.Sleep(20);
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.W:
                            this.y -= 1;
                            break;
                        case ConsoleKey.S:
                            this.y += 1;
                            break;
                        case ConsoleKey.A:
                            this.x -= 1;
                            break;
                        case ConsoleKey.D:
                            this.x += 1;
                            break;
                        case ConsoleKey.UpArrow:
                            if (y > 0)
                            {
                                GameLogic.ShotBullets.Add(new Bullet(x, y - 1, 1, true));
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (y < Console.WindowHeight - 1)
                            {
                                GameLogic.ShotBullets.Add(new Bullet(x, y + 1, 2, true));
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (x < Console.WindowWidth - 4)
                            {
                                GameLogic.ShotBullets.Add(new Bullet(x + 3, y, 4, true));
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (x > 2)
                            {
                                GameLogic.ShotBullets.Add(new Bullet(x - 3, y, 3, true));
                            }
                            break;
                    }
                    //   this.Collisions = 0;
                }
            }
            
        }
     
        public void ShootAndMoveBullets()
        {
            while (true)
            {
                foreach (var element in GameLogic.ShotBullets.ToList())
                {
                    element.UpdateBullet();
                }
                if (firingSpeed <= 10)
                {
                    Thread.Sleep(200 - 18 * firingSpeed);
                }
                else
                {
                    Thread.Sleep(20);
                }
            }
            
        }
        
        public void CheckWallCollision()
        {
            if (this.x <= playerModel.Length - 4)
            {
                this.x = Console.WindowWidth - 1 - playerModel.Length;
            }
            else if (this.x > Console.WindowWidth - playerModel.Length)
            {
                this.x = playerModel.Length / 2 + 1;
            }

            if (this.y <= 0)
            {
                this.y = Console.WindowHeight - 1;
            }
            else if (this.y >= Console.WindowHeight - 1)
            {
                this.y = 0;
            }
        }

        public void UpdateStatsOnLevelUp()
        {
            bool isMenuShow = true;
            while (isMenuShow)
            {
                ConsoleColor foreground = Console.ForegroundColor;
                //menu

                SetCursorPosition(1, 1);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Level UP!");
                SetCursorPosition(1, 2);
                Console.WriteLine("Pick ability: ");
                SetCursorPosition(1, 3);
                if (this.movementSpeed == 10)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("1. Movement Speed");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.WriteLine("1. Movement Speed");
                }
                SetCursorPosition(1, 4);
                if (this.attackPower == 10)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("2. Attack Power");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.WriteLine("2. Attack Power");
                }
                SetCursorPosition(1, 5);
                if (this.firingSpeed == 10)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("3. Firing Speed");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.WriteLine("3. Firing Speed");
                }
                SetCursorPosition(1, 6);
                Console.WriteLine("4. Lives");
                SetCursorPosition(1, 7);

                Console.ForegroundColor = foreground;

                //player choice

                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        this.movementSpeed++;
                        Player.playerLevel++;
                        this.Collisions = 0;
                        isMenuShow = false;
                        break;
                    case ConsoleKey.D2:
                        this.attackPower++;
                        Player.playerLevel++;
                        this.Collisions = 0;
                        isMenuShow = false;
                        break;
                    case ConsoleKey.D3:
                        this.firingSpeed++;
                        Player.playerLevel++;
                        this.Collisions = 0;
                        isMenuShow = false;
                        break;
                    case ConsoleKey.D4:
                        this.health += 10;
                        Player.playerLevel++;
                        this.Collisions = 0;
                        isMenuShow = false;
                        break;
                    default:
                        break;
                }

                Random statsIncrease = new Random();
                int stat = statsIncrease.Next(1, 5);

                foreach (var enemy in GameLogic.EnemyList.ToList())
                {
                    switch (stat)
                    {
                        case 1:
                            enemy.MovementSpeed++;
                            break;
                        case 2:
                            enemy.AttackPower++;
                            break;
                        case 3:
                            enemy.FiringSpeed++;
                            break;
                        case 4:
                            enemy.Health++;
                            break;
                        default:
                            break;
                    }
                }

                //Runs when the player levels up.
                this.SetPlayerColorOnLevelUp();
            }
        }
        public static void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(Console.BufferWidth - 120 + x, Console.BufferHeight - 40 + y);
        }

        public void SetPlayerColorOnLevelUp()
        {
            //Thread.Sleep(20 - movementSpeed);
            //Runs when the player levels up and makes him darker.
            switch (playerLevel)
            {
                case 0: 
                    playerColor = ConsoleColor.White; break;
                case 1:
                case 2: 
                    playerColor = ConsoleColor.Blue; break;
                case 3: 
                    playerColor = ConsoleColor.Red; break;
                case 4: 
                    playerColor = ConsoleColor.Green; break;
                case 5:
                case 6:
                case 7:
                case 8:
                    playerColor = ConsoleColor.DarkCyan; break;
                case 9:
                case 10:
                case 11:
                    playerColor = ConsoleColor.Yellow; break;
                default: 
                    playerColor = ConsoleColor.DarkMagenta; break;
            }

            SetPlayerModelOnLevelUP();
        }

        public void SetPlayerModelOnLevelUP()
        {
            switch (playerLevel)
            {
                case 0: playerModel = "-.☺.-"; break;
                case 1: playerModel = "-.☺.-"; break;
                case 2: playerModel = "-.☺.="; break;
                case 3: playerModel = "=.☺.="; break;
                case 4: playerModel = "-.☺.=="; break;
                case 5: playerModel = "==.☺.=="; break;
                case 6: playerModel = "==.☺.=="; break;

            }
        }

        public void PrintPlayerStats()
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(Console.WindowWidth - 22, 1);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("STATS:");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(Console.WindowWidth - 22, 3);
            Console.Write("Movement Speed - " + movementSpeed);
            Console.SetCursorPosition(Console.WindowWidth - 22, 4);
            Console.Write("Attack Power - " + attackPower);
            Console.SetCursorPosition(Console.WindowWidth - 22, 5);
            Console.Write("Firing Speed - " + firingSpeed);
            Console.SetCursorPosition(Console.WindowWidth - 22, 6);
            Console.Write("Lives - " + health);
            Console.SetCursorPosition(Console.WindowWidth - 22, 7);
            Console.Write("Level - " + playerLevel);
            Console.SetCursorPosition(Console.WindowWidth - 22, 9);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("SCORE: " + score);
            Console.ForegroundColor = foreground;
        }
    }
}