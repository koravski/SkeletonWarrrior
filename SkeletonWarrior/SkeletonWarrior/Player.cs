//A class for the player
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace SkeletonWarrior
{
    class Player
    {
        private int movementSpeed;
        private int attackPower;
        private int firingSpeed;
        private int playerLevel;
        private int health;
        private string playerModel;
        private int x;
        private int y;

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
        public int PlayerLevel
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
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.W:
                        this.y -= this.movementSpeed;
                        break;
                    case ConsoleKey.S:
                        this.y += this.movementSpeed;
                        break;
                    case ConsoleKey.A:
                        this.x -= this.movementSpeed;
                        break;
                    case ConsoleKey.D:
                        this.x += this.movementSpeed;
                        break;
                    case ConsoleKey.UpArrow:
                        if (y > 0)
                        {
                            GameLogic.ShotBullets.Add(new Bullet(x, y - 1, 1));
                        }
                        break;                     
                    case ConsoleKey.DownArrow:     
                        if (y < Console.WindowHeight - 1)
                        {
                            GameLogic.ShotBullets.Add(new Bullet(x, y + 1, 2));
                        }         
                        break;                     
                    case ConsoleKey.RightArrow:
                        if (x < Console.WindowWidth - 4)
                        {
                            GameLogic.ShotBullets.Add(new Bullet(x + 3, y, 4));
                        }                           
                        break;                     
                    case ConsoleKey.LeftArrow:
                        if (x > 2)
                        {
                            GameLogic.ShotBullets.Add(new Bullet(x - 4, y, 3)); 
                        }                       
                        break;
                    default:  
                        break;
                }
                CheckWallCollision();
            }
        }

        public void ShootAndMoveBullets()
        {
            foreach (var element in GameLogic.ShotBullets.ToList())
            {
                element.WriteBulletOnScreen();
                element.UpdateBullet();
            }
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

        public void UpdateStatsOnLevelUp()
        {
            //Runs when the player levels up.
            SetPlayerColorOnLevelUp();
        }

        public void SetPlayerColorOnLevelUp()
        {
            //Runs when the player levels up and makes him darker.
        }
    }
}