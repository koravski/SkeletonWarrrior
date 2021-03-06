﻿using System;
using System.Collections.Generic;

namespace SkeletonWarrior
{
    class Bullet
    {
        private int x;
        private int y;
        private int direction;
        private char bulletModel = '@';
        private bool friendly;
        //directions:
        // 1 - up
        // 2 - down
        // 3 - left
        // 4 - right

        public Bullet(int x, int y, int direction, bool friendly)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
            this.friendly = friendly;
        }
        public bool Friendly
        {
            get { return friendly; }
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
        public int Direction
        {
            get { return this.direction; }
        }
        public bool BulletCollisionCheck(Player player)
        {
            
            foreach (var enemy in GameLogic.EnemyList)
            {
                if ( (this.x == enemy.X - 1) &&
                     (this.y == enemy.Y) )
                {
                    GameLogic.ShotBullets.Remove(this);
                    if (enemy.Health > 0)
                    {
                        enemy.Health -= player.AttackPower;
                    }
                    else
                    {
                        GameLogic.EnemyList.Remove(enemy);
                        player.Collisions++;
                        Player.Score++;
                        return true;
                    }
                }
            }

            return false;
            
        }

        public void WriteBulletOnScreen()
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(this.x + 1, this.y);
            Console.Write(bulletModel);
            Console.ForegroundColor = foreground;
        }
        public void UpdateBullet()
        {
            if ( direction == 1 ) // if bullet is going up
	        {
                this.y--;
	        }
            else if (direction == 2) // if bullet is going down
            {
                this.y++;
            }
            else if (direction == 3) // if bullet is going left
	        {
                this.x--;
	        }
            else if (direction == 4)// if bullet is going right
	        {
                this.x++;
	        }
        }

        public void PrintBullets()
        {
            bool forRemoval = false;

            if ((this.y < 1) ||
                (this.y > Console.WindowHeight - 3) ||
                (this.x < 1) ||
                (this.x >= Console.WindowWidth - 3))
            {
                forRemoval = true;
            }

            if (forRemoval)
            {
                GameLogic.ShotBullets.Remove(this);
            }
            else
            {
                ConsoleColor foreground = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(this.x + 1, this.y);
                Console.Write(bulletModel);
                Console.ForegroundColor = foreground;
            }

        }
    }
}
