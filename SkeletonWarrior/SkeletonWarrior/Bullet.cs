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
        private int indexOfBullet;
        //directions:
        // 1 - up
        // 2 - down
        // 3 - left
        // 4 - right

        public Bullet(int x, int y, int direction)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
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
        public bool BulletCollisionCheck()
        {
            foreach (var enemy in GameLogic.EnemyList)
            {
                if ( (this.x == enemy.X - 1) &&
                     (this.y == enemy.Y) )
                {
                    int indexOfBullet = GameLogic.ShotBullets.IndexOf(this);
                    GameLogic.ShotBullets.RemoveAt(indexOfBullet);
                    GameLogic.EnemyList.RemoveAt(GameLogic.EnemyList.IndexOf(enemy));
                    return true;
                }
            }
            return false;
        }

        public void WriteBulletOnScreen()
        {
            ConsoleColor background = Console.BackgroundColor;
            Console.ForegroundColor = Player.playerColor;
            Console.SetCursorPosition(this.x + 1, this.y);
            Console.Write(bulletModel);
            Console.ForegroundColor = background;
        }
        public void UpdateBullet()
        {
            bool forRemoval = false;

            if ((this.y < 1) ||
                (this.y > Console.WindowHeight - 2) ||
                (this.x < 1) ||
                (this.x >= Console.WindowWidth - 2))
            {
                forRemoval = true;
            }

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
            else // if bullet is going right
	        {
                this.x++;
	        }
            if (forRemoval)
            {
                int indexOfBullet = GameLogic.ShotBullets.IndexOf(this);
                GameLogic.ShotBullets.RemoveAt(indexOfBullet);
            }
        }
    }
}