using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class World {

        private static World _instance;
        private readonly List<Level> _levels = new List<Level>();

        public Action OnLevelStartedAction { get; set; }
        public Action OnLevelFinishedAction { get; set; }
        public Action OnWorldFinishedAction { get; set; }

        private int _levelNumber;
        public Level CurrentLevel => _levels[_levelNumber];
        
        public Vector2 Gravity = new Vector2(0, -18);

        public Vector2 BaseSpeed = new Vector2(-3f, 0);
        public float BaseSpeedAnimMultiplier = 0.3f;
        
        public float XSpawn = 16f;
        public float XRemove = -16f;

        public float TravelDistance;

        public int FloorPlatformSize => 5;

        //public PlatformGenerationStrategy GENERATION_STRATEGY = PlatformGenerationStrategy.Air;


        // THIS WILL BE REFACTORED TO SOMEWHERE ELSE

        private Action _starsPickedValueChangedAction;
        private int _starsPicked = 10;

        //

        public int LevelFinishDefaultStrategyDistance = 20;

        private int _nextLevelStart;
        private int _nextLevelEnd;
        private bool _worldRunning = true;
        


        public int StarsPicked {
            get => _starsPicked;

            set {
                _starsPicked = value;
                _starsPickedValueChangedAction?.Invoke();
            }
        }

        public float DistanceTargetRatio => TravelDistance / CurrentLevel.ActiveLength;

        public bool IsDistanceTargetResetStrategy => (CurrentLevel.ActiveLength - (int)TravelDistance) < LevelFinishDefaultStrategyDistance;

        public bool LevelCompleted => TravelDistance > CurrentLevel.ActiveLength;

        
        public float EnemySpawnChanceGround => CurrentLevel.EnemySpawnChanceGround;
        public float PickupSpawnChanceGround => CurrentLevel.PickupSpawnChanceGround;
        public float EnemySpawnChancePlatforms => CurrentLevel.EnemySpawnChancePlatforms;
        public float PickupSpawnChancePlatforms => CurrentLevel.PickupSpawnChancePlatforms;

        public ScreenModel ScreenModel { get; set; }
        public float AirPlatformGapMin => CurrentLevel.AirPlatformGapMin;
        public float AirPlatformGapMax => CurrentLevel.AirPlatformGapMax;

        

        public void Initialize(int firstLevel, IEnumerable<Level> levelConfigList, ScreenModel screenModel){

            ScreenModel = screenModel;

            // Generating levels
            _levels.AddRange(levelConfigList);
            _levelNumber = firstLevel;

            _nextLevelEnd = CurrentLevel.ActiveLength;

            CurrentLevel.Initialized = true; // First level starts initialized
            
        }

        

        public static World GetInstance(){
            if (_instance != null)
                return _instance;

            _instance = new World();
            return _instance;
        }

        // Refactor this to make things faster
        public static World Restart(bool nextLevel){
            
            var levelNumber = _instance.CurrentLevel.Number;

            if (nextLevel) {
                levelNumber += 1;
            }


            //_instance = null;
            //_onWorldRestart();
            //_instance = new World (levelNumber, TODO);

            return _instance;

        }

        public void UpdateWorld(){

            if (!_worldRunning)
            {
                return;
            }

            TravelDistance += BaseSpeed.magnitude * Time.deltaTime;

            if (CurrentLevel.Initialized && TravelDistance > _nextLevelEnd)
            {
                Debug.Log($"[{TravelDistance}] - On Level Finished {_levelNumber} of {_levels.Count}");
                OnLevelFinishedAction?.Invoke();

                if (_levelNumber + 1 >= _levels.Count)
                {
                    _worldRunning = false;
                    OnWorldFinishedAction?.Invoke();
                    Debug.Log($"[{TravelDistance}] - World Finished");
                    return;

                }

                _levelNumber++;
                
                _nextLevelStart = _levels.Take(_levelNumber).Sum(level => level.ActiveLength + level.GapLength);
                Debug.Log($"[{TravelDistance}] - Next Level Start {_nextLevelStart}");


                return;
            }

            if (!CurrentLevel.Initialized && TravelDistance > _nextLevelStart)
            {
                CurrentLevel.Initialized = true;

                _nextLevelEnd = _nextLevelStart + CurrentLevel.ActiveLength;
                Debug.Log($"[{TravelDistance}] - Next Level End {_nextLevelEnd}");


                Debug.Log($"[{TravelDistance}] - On Level Started {_levelNumber} of {_levels.Count}");
                OnLevelStartedAction?.Invoke();
            }

        }

        

        public void RegisterStarsPickedValueChange(Action callback) {

            _starsPickedValueChangedAction += callback;
        }

        public void StopMoving() {

            BaseSpeed = Vector2.zero;

        }

    }
}
