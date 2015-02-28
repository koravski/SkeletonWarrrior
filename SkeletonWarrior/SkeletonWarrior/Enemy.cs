//A class for the enemies
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SkeletonWarrior
{
    class Enemy
    {
        /// <summary>
        /// Boss properties
        /// </summary>
        private static readonly string bossFile = "boss.txt";
        private static char[,] bossMatrix = new char[13, 28];
        public static int BossAttackPower = 100;
        public static int BossMovementSpeed = 40;
        public static int BossFiringSpeed = 30;
        public static int BossHealth = 5000;
        public static ConsoleColor BossColor = ConsoleColor.Red;

        private int x;
        private int y;
        private int movementSpeed;
        private int attackPower;
        private int firingSpeed;
        private int enemyLevel;
        private int health;
        private char enemyType;
        private ConsoleColor enemyColor = ConsoleColor.Red;

        public Enemy(int movementSpeed, int attackPower, int firingSpeed, int enemyLevel, int health, char enemyType)
        {
            this.movementSpeed = movementSpeed;
            this.attackPower = attackPower;
            this.firingSpeed = firingSpeed;
            this.enemyLevel = enemyLevel;
            this.health = health;
            this.enemyType = enemyType;
            PickEnemyCoords();
            
        }
        private void PickEnemyCoords()
        {
            Random coordsPicker = new Random();
            this.x = coordsPicker.Next(1, Console.WindowWidth - 1);
            this.y = coordsPicker.Next(1, Console.WindowHeight - 1);
        }

        public int X
        {
            get { return x; }
            set { this.x = value; }
        }
        public int Y
        {
            get { return y; }
            set { this.y = value; }
        }
        public int MovementSpeed
        {
            get { return movementSpeed; }
            set { movementSpeed = value; }
        }
        public int AttackPower
        {
            get { return attackPower; }
            set { attackPower = value; }
        }
        public int FiringSpeed
        {
            get { return firingSpeed; }
            set { firingSpeed = value; }
        }
        public int EntityLevel
        {
            get { return enemyLevel; }
            set { enemyLevel = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public char EnemyType
        {
            get { return enemyType; }
        }

        public static void SetEnemyColorOnLevelUp()
        {
            //Runs when enemy levels up and makes him darker (or the lightest color).
        }

        public void WriteEnemyOnScreen()
        {
            ConsoleColor foreground = Console.BackgroundColor;
            Console.ForegroundColor = enemyColor;
            Console.SetCursorPosition(this.x, this.y);
            Console.Write(EnemyType);
            Console.ForegroundColor = foreground;
        }

        public void Move(int playerX, int playerY)
        {
            
            if (this.x < playerX)
            {

                this.x++;
            }
            else if (this.x > playerX)
            {
                this.x--;
            }
            if (playerY > this.y)
            {
                this.y++;
            }
            else if (this.y > playerY)
            {
                this.y--;
            }
        }

        public void Shoot()
        {
           
        }
    }
}
