using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Resources.ScriptableObjects;
using Assets.Scripts.Model;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    [RequireComponent (typeof (PlayerController))]
    public class GameController : MonoBehaviour {
	
        private ActorFactory _objFactory;
        private World _world;
        private PlayerController _playerController;

        private ActorComponent[] _backgroundArray;
        
        public bool CreateAirPlatforms = true;
        public bool ManageStrategies = true;
        public bool InitializePlayer = true;

        public ScrollingBackgroundController BackgroundA;
        public ScrollingBackgroundController BackgroundB;

        public UIController UIController;

        public LevelConfig LevelConfig;

        public List<PlatformGenerationStrategy> GenerationStrategyList = new List<PlatformGenerationStrategy>();
        private PlatformBuilder _platformBuilder;

        public Button TestAir;
        public Button TestGround;
        public Button TestNothing;


        //float multiplier = 0.1f;

        //private IEnumerator routineCreateObstacles;
        //private IEnumerator routineMultiply;

        private void Awake(){
	
            _objFactory = ActorFactory.getInstance();
            _world = World.GetInstance();
            

            _playerController = GetComponent<PlayerController> ();

	
        }
		
        // Use this for initialization
        // ReSharper disable once UnusedMember.Local
        private void Start () {

            Physics2D.gravity = _world.Gravity;

            _world.Initialize(0, LevelConfig.Levels.ToList().Select(level => level), new ScreenModel());
            _world.OnLevelFinishedAction += OnLevelFinish;
            _world.OnLevelStartedAction += OnLevelStarted;
            _world.OnWorldFinishedAction += OnWorldFinished;

            _world.BaseSpeed = new Vector2(-10,0);

            _platformBuilder = new PlatformBuilder(PlatformGenerationStrategy.GroundOnly, _world, ActorFactory.getInstance());

            GenerationStrategyList.Add(PlatformGenerationStrategy.GroundOnly);


            _platformBuilder.CreateInitialPlatforms();
            StartCoroutine(_platformBuilder.EvaluateBuilding());

            StartCoroutine(RoutineManageStrategies());

            UIController.Initialize(_world);

            if (InitializePlayer)
            {
                _playerController.Initialize(_objFactory.buildPlayer(), UIController.UpdateJumps, UIController.OnPlayerDeath);
                //_playerController.PlayerAvatar = _objFactory.buildPlayer();
            }
            

            // Initialize Parallax Background
            BackgroundA.Initialize(_world, 0.03f);
            BackgroundB.Initialize(_world, 0.01f);

            //backgroundArray = new ActorComponent[3];
            //backgroundArray[0] = objFactory.buildActor (new Background ());
            //backgroundArray[1] = objFactory.buildActor (new Background ());
            //backgroundArray[2] = objFactory.buildActor (new Background ());

            //StartCoroutine(RoutineParallaxBackground());

            //CreateInitialPlatforms ();


            //GenerationStrategyList.Add(PlatformGenerationStrategy.Air);

            TestGround.onClick.AddListener(() => _platformBuilder.GenerationStrategy = PlatformGenerationStrategy.GroundOnly);
            TestAir.onClick.AddListener(() => _platformBuilder.GenerationStrategy = PlatformGenerationStrategy.AirOnly);
            //TestNothing.onClick.AddListener(() => _platformBuilder.GenerationStrategy = PlatformGenerationStrategy.Nothing);


        }

        private void OnWorldFinished()
        {
            
        }

        private void OnLevelStarted()
        {
            _platformBuilder.GenerationStrategy = PlatformGenerationStrategy.GroundOnly;
        }

        private void OnLevelFinish()
        {
            _platformBuilder.GenerationStrategy = PlatformGenerationStrategy.Nothing;
        }

        void FixedUpdate(){

            _world.UpdateWorld ();
        }

    
        public void ResetStrategy()
        {
            _platformBuilder.GenerationStrategy = GenerationStrategyList[0];
        }

        private IEnumerator RoutineManageStrategies() {

            float ticks = 0;

            while (true)
            {

                var level = _world.CurrentLevel;
               

                yield return null;
            }

        }
        

        /*
        private IEnumerator RoutineIncreaseDifficulty() {
            while (true) {

                yield return new WaitForSeconds(_world.DificultyIncreaseTime);

                if (IncreaseDifficulty){

                    float x = _world.BaseSpeed.x - _world.DifficultyIncrease;
                    float y = _world.BaseSpeed.y;

                    _world.BaseSpeed = new Vector2(x, y);
                    _world.SpawnTimer -= _world.SpawnTimer - _world.DifficultyIncrease;
                }

            }
        }
        */
		

       

	


    }
}
