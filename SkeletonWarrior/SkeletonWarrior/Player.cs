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
        public int playerLevel;
        private int health;
        private string playerModel;
        private int x;
        private int y;
        public static ConsoleColor playerColor = ConsoleColor.Blue;
        private int collisions;

        public  int Collisions
        {
            get { return collisions; }
            set { collisions = value; }
        }

        
        public Player(int x, int y, int movementSpeed, int attackPower, int firingSpeed, int playerLevel, int health)
        {
            this.x = x;
            this.y = y;
            this.movementSpeed = movementSpeed;
            this.attackPower = attackPower;
            this.firingSpeed = firingSpeed;
            this.playerLevel = playerLevel;
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
        public  int PlayerLevel
        {
            get { return this.playerLevel; }
            set { this.playerLevel = value; }
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
                                GameLogic.ShotBullets.Add(new Bullet(x - 4, y, 3, true));
                            }
                            break;
                    }
                    //   this.Collisions = 0;
                    CheckWallCollision();
                }
            }
            
        }
     
        public void ShootAndMoveBullets()
        {
            foreach (var element in GameLogic.ShotBullets.ToList())
            {
                element.WriteBulletOnScreen();
                element.UpdateBullet();
            }
            //if (firingSpeed <= 10)
            //{
            //    Thread.Sleep(200 - 18 * firingSpeed);
            //}
            //else
            //{
            //    Thread.Sleep(20);
            //}
        }
        
        private void CheckWallCollision()
        {
            if (this.x <= movementSpeed)
            {
                this.x = Console.WindowWidth - 1 - playerModel.Length;
            }
            else if (this.x > Console.WindowWidth - playerModel.Length)
            {
                this.x = playerModel.Length / 2 + 1;
            }

            if (this.y < 0)
            {
                this.y = Console.WindowHeight - 1;
            }
            else if (this.y >= Console.WindowHeight)
            {
                this.y = 0;
            }
        }

        public static void UpdateStatsOnLevelUp(Player player)
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
                if (player.movementSpeed == 10)
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
                if (player.attackPower == 10)
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
                if (player.firingSpeed == 10)
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
                Console.ForegroundColor = foreground;

                //player choice

                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        player.movementSpeed++;
                        player.playerLevel++;
                        player.Collisions = 0;
                        isMenuShow = false;
                        break;   // Do something 
                    case ConsoleKey.D2:
                        player.attackPower++;
                        player.playerLevel++;
                        player.Collisions = 0;
                        isMenuShow = false;
                        break;   // Do something 
                    case ConsoleKey.D3:
                        player.firingSpeed++;
                        player.playerLevel++;
                        player.Collisions = 0;
                        isMenuShow = false;
                        break;   // Do something 
                    case ConsoleKey.D4:
                        player.health += 10;
                        player.playerLevel++;
                        player.Collisions = 0;
                        isMenuShow = false;
                        break;   // Do something 
                    default:
                        break;
                }

                
                //Runs when the player levels up.
                player.SetPlayerColorOnLevelUp();
            }
        }
        public static void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(Console.BufferWidth - 120 + x, Console.BufferHeight - 40 + y);
        }

        public void SetPlayerColorOnLevelUp()
        {
            Thread.Sleep(20 - movementSpeed);
            //Runs when the player levels up and makes him darker.
            switch (playerLevel)
            {
                case 0: playerColor = ConsoleColor.White; break;
                case 1: playerColor = ConsoleColor.Blue; break;
                case 2: playerColor = ConsoleColor.Red; break;
                case 3: playerColor = ConsoleColor.Green; break;
                case 4: playerColor = ConsoleColor.DarkCyan; break;
                case 5: playerColor = ConsoleColor.Yellow; break;
                case 6: playerColor = ConsoleColor.DarkMagenta; break;

            }
        }

        public void PrintPlayerStats()
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(Console.WindowWidth - 22, 1);
            Console.Write("STATS:");
            Console.SetCursorPosition(Console.WindowWidth - 22, 3);
            Console.Write("Movement Speed - " + movementSpeed);
            Console.SetCursorPosition(Console.WindowWidth - 22, 4);
            Console.Write("Attack Power - " + attackPower);
            Console.SetCursorPosition(Console.WindowWidth - 22, 5);
            Console.Write("Firing Speed - " + firingSpeed);
            Console.SetCursorPosition(Console.WindowWidth - 22, 6);
            Console.Write("Lives - " + health);
            Console.ForegroundColor = foreground;
        }
    }
}