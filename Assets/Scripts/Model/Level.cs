using System;

namespace Assets.Scripts.Model
{
    
    [Serializable]
    public class Level  {

        public float EnemySpawnChancePlatforms = 1f;
        public float EnemySpawnChanceGround = 1f;

        public float PickupSpawnChancePlatforms = 0.5f;
        public float PickupSpawnChanceGround = 0.2f;

        public int Number;

        public float AirPlatformGapMin;
        public float AirPlatformGapMax;

        [NonSerialized]
        public bool Initialized = false;

        public int ActiveLength;
        public int GapLength;
    }
}
