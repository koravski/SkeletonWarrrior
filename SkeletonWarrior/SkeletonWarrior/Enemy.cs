//A class for the enemies
using System;
using System.Collections.Generic;
using System.IO;

namespace SkeletonWarrior
{
    class Enemy
    {
        private static readonly string bossFile = "boss.txt";
        private static char[,] bossMatrix = new char[13, 28];
        private const int BossAttackPower = 4;
        private const int BossMovementSpeed = 3;
        private const int BossFiringSpeed = 3;
        private const int BossHealth = 5;

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

        public static string GetBossFile
        {
            get { return bossFile; }
        }

        public static void SetEnemyColorOnLevelUp()
        {
            //Runs when enemy levels up and makes him darker (or the lightest color).
        }

        public void WriteEnemyOnScreen()
        {
            ConsoleColor foreground = Console.BackgroundColor;
            Console.ForegroundColor =  enemyColor;
            Console.SetCursorPosition(this.x, this.y);
            Console.Write(EnemyType);
            Console.ForegroundColor = foreground;
        }

        public void Move()
        {

        }

        public void Shoot()
        {

        }

        public void Chase()
        {

        }

        public static void GetBoss(string bossFile)
        {
            ReadBoss(bossFile);

            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < bossMatrix.GetLength(0); i++)
            {
                GameLogic.SetCursorPosition(Console.WindowWidth / 2 - 25, Console.WindowHeight / 16 + i);
                for (int j = 0; j < bossMatrix.GetLength(1); j++)
                {
                    Console.Write(bossMatrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        private static void ReadBoss(string bossFile)
        {
            String input = File.ReadAllText(bossFile);

            int i = 0;
            
            foreach (var row in input.Split('\n'))
            {
                for (int j = 0; j < row.Length; j++)
                {
                    bossMatrix[i, j] = row[j];
                }
                i++;
            }
        }
    }
}
