using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (PlayerController))]
public class GameController : MonoBehaviour {
	
	private ActorFactory objFactory;
	private World world;
	private PlayerController playerController;

	private ActorComponent[] backgroundArray;

	public Text distanceScore;

	public bool createAirPlatforms = true;
	public bool manageStrategies = true;
	public bool increaseDifficulty = true;

	//float multiplier = 0.1f;
	
	//private IEnumerator routineCreateObstacles;
	//private IEnumerator routineMultiply;

	void Awake(){
	
		objFactory = ActorFactory.getInstance();
		world = World.getInstance();

		playerController = GetComponent<PlayerController> ();
	
	}
		
	// Use this for initialization
	void Start () {

		Physics2D.gravity = world.GRAVITY;
		StartCoroutine(RoutineCreateGroundPlatforms());

		StartCoroutine(RoutineCreateAirPlatforms());
		StartCoroutine(RoutineManageStrategies());
		StartCoroutine(RoutineIncreaseDifficulty());

		playerController.PlayerAvatar = objFactory.buildPlayer ();

		backgroundArray = new ActorComponent[3];
		backgroundArray[0] = objFactory.buildActor (new Background ());
		//backgroundArray[1] = objFactory.buildActor (new Background ());
		//backgroundArray[2] = objFactory.buildActor (new Background ());

		CreateInitialPlatforms ();
	
	}
	
	// Update is called once per frame
	void Update () {

		distanceScore.text = "Distance: " + world.TRAVEL_DISTANCE.ToString("N0");
	
	}

	void FixedUpdate(){

		world.update ();

	}

	private void CreateInitialPlatforms(){

		float min = world.SCREEN_LEFT;
		float max = world.X_SPAWN;
		float size = world.FLOOR_PLATFORM_SIZE;

		//objFactory.buildGroundPlatform(FloorPlatformType.LEFT, min - size);

		for (float pos = min - size;  pos < max + size;  pos+= size) {
			objFactory.buildGroundPlatform(FloorPlatformType.CENTER, pos);
		}

		//objFactory.buildGroundPlatform(FloorPlatformType.RIGHT, max);

	}

	private IEnumerator RoutineManageStrategies() {

		float ticks = 0;

		while (true) {

			if (world.TRAVEL_DISTANCE >= world.STRATEGY_CHANGE_FREQUENCY * ticks &&
				Random.Range(0f,1f) < world.STRATEGY_CHANGE_CHANCE) {

				if (manageStrategies){
					world.randomStrategy ();
				}
					
				ticks++;

			}

			yield return null;
		}

	}

	private IEnumerator RoutineCreateGroundPlatforms() {

		float ticks = 0;
		float chunkSize = 0;

		FloorPlatformType lastType = FloorPlatformType.CENTER;

		while (true) {

			if (world.GENERATION_STRATEGY == PlatformGenerationStrategy.Ground) {

				chunkSize = Random.Range (world.GROUND_CHUNK_MIN, world.GROUND_CHUNK_MAX);

				float chunks = chunkSize;

				while (chunks >= 0) {

					if (world.TRAVEL_DISTANCE >= world.FLOOR_PLATFORM_SIZE * ticks) {

						FloorPlatformType type = FloorPlatformType.CENTER;
					
						if (lastType == FloorPlatformType.EMPTY)
							type = FloorPlatformType.LEFT;
						
						if (chunks == 0 && lastType == FloorPlatformType.CENTER)
							type = FloorPlatformType.RIGHT;

						float diff = world.TRAVEL_DISTANCE - world.FLOOR_PLATFORM_SIZE * ticks;

						ActorComponent obstacle = objFactory.buildGroundPlatform (type, world.X_SPAWN - diff);

						lastType = type;

						if (world.GENERATION_STRATEGY != PlatformGenerationStrategy.Ground) {
							chunks--;
						}

						ticks++;
					}

					yield return null;

				}

			} else {

				if (world.TRAVEL_DISTANCE >= world.FLOOR_PLATFORM_SIZE * ticks) {
					ticks++;

					lastType = FloorPlatformType.EMPTY;
				}

				yield return null;

			}
		}
	}

	private IEnumerator RoutineParallaxBackground(){

		float ticks = 0;

		foreach (ActorComponent backgroundActor in backgroundArray) {

			Transform t = backgroundActor.transform;
			Background bg = backgroundActor.actor as Background;

			//t.position = bg.startingPosition + new Vector2 ( t.
		}

		while (true) {

			if ( world.TRAVEL_DISTANCE >= world.PLATFORM_FREQUENCY * ticks){

				ticks += 1;
			}

			yield return null;

		}


	}

	// every 2 seconds perform the action
	private IEnumerator RoutineCreateAirPlatforms() {

		float airTicks = 0;
		//float floorTicks = 0;

		while (true) {
		
			if ( world.TRAVEL_DISTANCE >= world.PLATFORM_FREQUENCY * airTicks){

				if (createAirPlatforms) {
					ActorComponent platform = objFactory.buildAirPlatform ();
					AdjustToBounds (platform);

					if (Random.Range (0, 1) <= world.ENEMY_SPAWN_CHANCE_PLATFORMS) {

						ActorComponent enemy = objFactory.buildEnemy ();

						float platformSizeY = (platform.getColliders () [0] as BoxCollider2D).size.y;

						BoxCollider2D enemyCollider = enemy.getColliders () [0] as BoxCollider2D;
						float enemySizeY = enemyCollider.size.y;

						enemy.transform.position = platform.transform.position;
						enemy.transform.Translate (0, platformSizeY * 0.5f + enemySizeY * 0.5f - enemyCollider.offset.y, 0);							
					}
				}
					
				airTicks += 1;
			}

			yield return null;

		}
	}

	private IEnumerator RoutineIncreaseDifficulty() {
		while (true) {

			yield return new WaitForSeconds(world.DIFICULTY_MULTIPLIER_TIME);

			if (increaseDifficulty){
				world.BASE_SPEED *= world.DIFICULTY_MULTIPLIER;
				world.SPAWN_TIMER -= world.SPAWN_TIMER * world.DIFICULTY_MULTIPLIER;
			}

		}
	}
		

	void AdjustToBounds(ActorComponent obj){

		float max = obj.getBounds().max.y;
		float min = obj.getBounds().min.y;


		float diff = 0;
		if (max > world.CELLING_Y) {
			diff = world.CELLING_Y - max;
		}
		else if (min < world.FLOOR_Y){
			diff = world.FLOOR_Y - min;
		}

		obj.transform.Translate (0, diff, 0);
	}

	public void RestartGame(){

		//print ("click");

		Application.LoadLevel(Application.loadedLevel);
		World.restart ();
	}


}
