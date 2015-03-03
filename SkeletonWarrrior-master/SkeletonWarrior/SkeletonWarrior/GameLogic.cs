// This class will handle the game logic and how everything updates
using System;
using System.Collections.Generic;

namespace SkeletonWarrior
{
    class GameLogic
    {

        private static List<Enemy> enemyList = new List<Enemy>();
        private static List<Bullet> shotBullets = new List<Bullet>();

        private ConsoleColor playerColor = ConsoleColor.Cyan;

        public static List<Enemy> EnemyList
        {
            get { return enemyList; }
            set { enemyList = value; }
        }
        public static List<Bullet> ShotBullets
        {
            get { return shotBullets; }
            set { shotBullets = value; }
        }
        public ConsoleColor PlayerColor
        {
            get { return this.playerColor; }
            set { this.playerColor = value; }
        }

        public void MapLevelUp(int playerColor)
        {
            //Runs when the background color changes.
            SetBackgroundColorOnLevelChange();
        }
        

        public static void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(Console.BufferWidth - 120 + x, Console.BufferHeight - 40 + y);
        }
        public void SetBackgroundColorOnLevelChange()
        {
            //Runs when background has to change and makes it darker.
        }
    }
}
