using System;
using System.Collections.Generic;

namespace SkeletonWarrior
{
    class Bullet
    {
        private int x;
        private int y;
        private int direction;
        private char bulletModel = '@';
        private int bulletID;
        //directions:
        // 1 - up
        // 2 - down
        // 3 - left
        // 4 - right

        public Bullet(int x, int y, int direction, int bulletID)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
            this.bulletID = bulletID;
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
        public int BulletID
        {
            get { return bulletID; }
        }

        public bool BulletCollision()
        {
            bool isColliding = false;



            return isColliding;
        }
        public void WriteBulletOnScreen()
        {
            Console.SetCursorPosition(this.x, this.y);
            Console.Write(bulletModel);
        }
        public void UpdateBullet()
        {
            if ( direction == 1 )
	        {
                if ( this.y < 1 )
                {
                    Player.ShotBullets.RemoveAt(this.bulletID);
                }
                else
                {
                    this.y--;
                }
	        }
            else if (direction == 2)
            {
		        this.y++;
            }
            else if (direction == 3)
	        {
		        this.x--;
	        }
            else if (true)
	        {
		        this.x++;
	        }
        }
    }
}
