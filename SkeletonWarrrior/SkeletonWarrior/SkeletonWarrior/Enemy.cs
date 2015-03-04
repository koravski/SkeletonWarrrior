//A class for the enemies
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SkeletonWarrior
{
    class Enemy
    {
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
        private static int enemyLevel = 1;
        private int health;
        private char enemyType;
        private static ConsoleColor enemyColor = ConsoleColor.Yellow;
        //public ConsoleColor enemyColor = ConsoleColor.Red;

        public Enemy(int movementSpeed, int attackPower, int firingSpeed, int health, char enemyType)
        {
            this.movementSpeed = movementSpeed;
            this.attackPower = attackPower;
            this.firingSpeed = firingSpeed;
            this.health = health;
            this.enemyType = enemyType;
            PickEnemyCoords();
        }
        private void PickEnemyCoords()
        {
            Random coordsPicker = new Random();
            this.x = coordsPicker.Next(3, Console.WindowWidth - 3);
            this.y = coordsPicker.Next(3, Console.WindowHeight - 3);
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
        public static int EnemyLevel
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
        
        public static ConsoleColor SetEnemyColorOnLevelUp()
        {
            switch (Player.playerColor)
            {
                case ConsoleColor.DarkBlue: enemyColor = ConsoleColor.Red; break;
                case ConsoleColor.Blue: enemyColor = ConsoleColor.DarkRed; break;
                case ConsoleColor.Cyan: enemyColor = ConsoleColor.DarkYellow; break;
                case ConsoleColor.DarkMagenta: enemyColor = ConsoleColor.Blue; break;
                default: return enemyColor;
            }
            return enemyColor;
        }
        public void WriteEnemyOnScreen()
        {
            ConsoleColor foreground = Console.BackgroundColor;
            Console.ForegroundColor = SetEnemyColorOnLevelUp();
            Console.SetCursorPosition(this.x, this.y);
            Console.Write(EnemyType);
            Console.ForegroundColor = foreground;
        }

        public void Move(Player player)
        {
            if (this.X > player.X + 1)
            {
                this.X--;
            }
            else if (this.X < player.X + 1)
            {
                this.X++;
            }
            if (this.Y > player.Y)
            {
                this.Y--;
            }
            else if (this.Y < player.Y)
            {
                this.Y++;
            }
        }

        public void Shoot(Player player)
        {
            Thread.Sleep(20);
            if (player.X - 3 <= this.x &&
                player.X + 5 >= this.x &&
                player.Y < this.y)
            {
                GameLogic.ShotBullets.Add(new Bullet(this.x - 1, this.y - 1, 1,false));
            }
            else if (player.X - 3 <= this.x &&
                     player.X + 5 >= this.x &&
                     player.Y > this.y)
            {
                GameLogic.ShotBullets.Add(new Bullet(this.x - 1, this.y + 1, 2, false));
            }
            else if (player.Y - 1 <= this.y &&
                     player.Y + 1 >= this.y && 
                     player.X < this.x)
            {
                GameLogic.ShotBullets.Add(new Bullet(this.x - 2, this.y, 3, false));
            }
            else if (player.Y - 1 <= this.y &&
                     player.Y + 1 >= this.y &&
                     player.X > this.x)
            {
                GameLogic.ShotBullets.Add(new Bullet(this.x, this.y, 4, false));
            }
        }

        public static void GetBoss()
        {
            ReadBoss();

            Console.ForegroundColor = BossColor;

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

        private static void ReadBoss()
        {
            try
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
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not found boss file!");
            }
        }
    }
}
