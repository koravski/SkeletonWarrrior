//A class for the player
using System;
using System.Collections.Generic;
using System.Threading;

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
        private ConsoleColor playerColor = ConsoleColor.Cyan;
        private static List<Bullet> shotBullets = new List<Bullet>();
        private int currentBulletID = 0;

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
        public ConsoleColor PlayerColor
        {
            get { return this.playerColor; }
            set { this.playerColor = value; }
        }
        public static List<Bullet> ShotBullets
        {
            get { return shotBullets; }
            set { shotBullets = value; }
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
                        ShotBullets.Add(new Bullet(x, y - 1, 1, currentBulletID));
                        currentBulletID++;
                        break;
                    case ConsoleKey.DownArrow:
                        ShotBullets.Add(new Bullet(x, y + 1, 2, currentBulletID));
                        currentBulletID++;
                        break;
                    case ConsoleKey.RightArrow:
                        ShotBullets.Add(new Bullet(x + 1, y, 4, currentBulletID));
                        currentBulletID++;
                        break;
                    case ConsoleKey.LeftArrow:
                        ShotBullets.Add(new Bullet(x - 1, y, 3, currentBulletID));
                        currentBulletID++;
                        break;
                    default:  
                        break;
                }
                CheckWallCollision();
            }
        }

        public void HandleBullets()
        {
            foreach (var element in ShotBullets)
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