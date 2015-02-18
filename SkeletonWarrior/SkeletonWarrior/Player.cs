//A class for the player
using System;

namespace SkeletonWarrior
{
    class Player
    {
        private int movementSpeed;
        private int attackPower;
        private int firingSpeed;
        private int playerLevel;
        private int health;
        private string playerModel;

        public Player(int movementSpeed, int attackPower, int firingSpeed, int playerLevel, int health)
        {
            this.movementSpeed = movementSpeed;
            this.attackPower = attackPower;
            this.firingSpeed = firingSpeed;
            this.playerLevel = playerLevel;
            this.health = health;
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
        public int PlayerLevel
        {
            get { return playerLevel; }
            set { playerLevel = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public string PlayerModel
        {
            get { return playerModel; }
            set { playerModel = value; }
        }

        public static void Move()
        {

        }

        public static void Shoot()
        {

        }

        public void UpdateStatsOnLevelUp()
        {
            //Runs when the player levels up.
            SetPlayerColorOnLevelUp();
        }

        public void SetPlayerColorOnLevelUp()
        {
            //Runs when the player levels up and makes him darker.
        }

    }
}
