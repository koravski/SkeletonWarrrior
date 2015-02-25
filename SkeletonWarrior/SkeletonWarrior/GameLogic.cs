﻿// This class will handle the game logic and how everything updates
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

        public void MapLevelUp()
        {
            //Runs when the background color changes.
            SetBackgroundColorOnLevelChange();
            Enemy.SetEnemyColorOnLevelUp();
        }
        
        public void LevelUpEnemies()
        {
            //Runs when the player kills enough enemies that they their color becomes darker.
            Enemy.SetEnemyColorOnLevelUp();
        }

        public static void LevelUpPlayer()
        {
            //Runs when the player kills enough enemies that he levels up. 
            ConsoleColor foreground = Console.ForegroundColor;
            //menu
            SetCursorPosition(1, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Level UP!");
            SetCursorPosition(1, 2);
            Console.WriteLine("Pick ability: ");
            SetCursorPosition(1, 3);
            Console.WriteLine("1. Movement Speed");
            SetCursorPosition(1, 4);
            Console.WriteLine("2. Attack Power");
            SetCursorPosition(1, 5);
            Console.WriteLine("3. Firing Speed");
            SetCursorPosition(1, 6);
            Console.WriteLine("4. Lives");
            Console.ForegroundColor = foreground;

            //player choice
            bool commandCorrect = false;
            while (!commandCorrect)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.D1: commandCorrect = true; break;   // Do something 
                        case ConsoleKey.D2: commandCorrect = true; break;   // Do something 
                        case ConsoleKey.D3: commandCorrect = true; break;   // Do something 
                        case ConsoleKey.D4: commandCorrect = true; break;   // Do something 
                        default: break;
                    }
                }
            }
        }
        static void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(Console.BufferWidth - 120 + x, Console.BufferHeight - 40 + y);
        }

        public void SetBackgroundColorOnLevelChange()
        {
            //Runs when background has to change and makes it darker.
        }
    }
}
