using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (PlayerController))]
public class GameController : MonoBehaviour {
	
	private ObjectFactory objFactory;
	private World world;
	private PlayerController playerController;

	//float multiplier = 0.1f;
	
	//private IEnumerator routineCreateObstacles;
	//private IEnumerator routineMultiply;

	void Awake(){
	
		objFactory = ObjectFactory.getInstance();
		world = World.getInstance();

		playerController = GetComponent<PlayerController> ();
	
	}
		
	// Use this for initialization
	void Start () {

		Physics2D.gravity = world.GRAVITY;

		StartCoroutine(RoutineCreateAirPlatforms());
		StartCoroutine(RoutineCreateGroundPlatforms());
		StartCoroutine(RoutineMultiply(world.DIFICULTY_MULTIPLIER_TIME));

		playerController.PlayerAvatar = objFactory.buildPlayer ();

		CreateInitialPlatforms ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
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

	private IEnumerator RoutineCreateGroundPlatforms() {

		float ticks = 0;
		float chunkSize = 0;

		while (true) {

			if (world.GENERATION_STRATEGY == PlatformGenerationStrategy.Ground) {

				chunkSize = Random.Range (world.GROUND_CHUNK_MIN, world.GROUND_CHUNK_MAX);

				float chunks = chunkSize;

				while (chunks > 0) {

					if (world.TRAVEL_DISTANCE >= world.FLOOR_PLATFORM_SIZE * ticks) {

						//print (chunks);

						FloorPlatformType type = FloorPlatformType.CENTER;

						//if (chunks > 1 && chunks < chunkSize)
						//	type = FloorPlatformType.CENTER;
						
						if (chunks <= 0)
							type = FloorPlatformType.RIGHT;

						//float position = ticks * world.FLOOR_PLATFORM_SIZE;

						float diff = world.TRAVEL_DISTANCE - world.FLOOR_PLATFORM_SIZE * ticks;

						print (diff);

						ActorComponent obstacle = objFactory.buildGroundPlatform (type, world.X_SPAWN - diff);

						ticks++;

						if (world.GENERATION_STRATEGY != PlatformGenerationStrategy.Ground) {
							chunks--;
						}
					}

					yield return null;

				}

			} else {
				yield return null;

			}
		}
	}

	// every 2 seconds perform the action
	private IEnumerator RoutineCreateAirPlatforms() {

		float airTicks = 0;
		//float floorTicks = 0;

		while (true) {
		
			if ( world.TRAVEL_DISTANCE >= world.OBSTACLE_FREQUENCY * airTicks){
				
				ActorComponent obstacle = objFactory.buildAirPlatform();
				AdjustToBounds (obstacle);

				airTicks += 1;
			}

			yield return null;

		}
	}

	private IEnumerator RoutineMultiply(float waitTime) {
		while (true) {
			yield return new WaitForSeconds(waitTime);

			world.BASE_SPEED *= world.DIFICULTY_MULTIPLIER;
			world.SPAWN_TIMER -= world.SPAWN_TIMER * world.DIFICULTY_MULTIPLIER;


		}
	}
	
	// every 2 seconds perform the action
    private IEnumerator RoutineCreateOther(float waitTime) {
        while (true) {
            yield return new WaitForSeconds(waitTime);

			ActorComponent obstacle = objFactory.buildActor();

			AdjustToBounds (obstacle);

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
}
