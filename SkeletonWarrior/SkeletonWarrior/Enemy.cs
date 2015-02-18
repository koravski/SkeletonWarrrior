//A class for the enemies
using System;

namespace SkeletonWarrior
{
    class Enemy
    {
        private int movementSpeed;
        private int attackPower;
        private int firingSpeed;
        private int enemyLevel;
        private int health;
        private char enemyType;

        public Enemy(int movementSpeed, int attackPower, int firingSpeed, int enemyLevel, int health, char enemyType)
        {
            this.movementSpeed = movementSpeed;
            this.attackPower = attackPower;
            this.firingSpeed = firingSpeed;
            this.enemyLevel = enemyLevel;
            this.health = health;
            this.enemyType = enemyType;
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

        public void Move()
        {

        }

        public void Shoot()
        {

        }

        public void Chase()
        {

        }
    }
}
