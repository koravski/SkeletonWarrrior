// This class will handle the game logic and how everything updates
using System;

namespace SkeletonWarrior
{
    class GameLogic
    {
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

        public void LevelUpPlayer()
        {
            //Runs when the player kills enough enemies that he levels up. 
        }

        public void SetBackgroundColorOnLevelChange()
        {
            //Runs when background has to change and makes it darker.
        }
    }
}
