using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (PlayerController))]
public class GameController : MonoBehaviour {
	
	private ObjectFactory objFactory;
	private World world;
	private PlayerController playerController;

	//float multiplier = 0.1f;
	
	private IEnumerator routineCreateObstacles;
	private IEnumerator routineMultiply;

	void Awake(){
	
		objFactory = ObjectFactory.getInstance();
		world = World.getInstance();

		playerController = GetComponent<PlayerController> ();
	
	}
		
	// Use this for initialization
	void Start () {
		
		routineCreateObstacles = RoutineCreatePlatforms(0.1f);
		StartCoroutine(routineCreateObstacles);

		routineMultiply = RoutineMultiply(world.DIFICULTY_MULTIPLIER_TIME);
		StartCoroutine(routineMultiply);

		Physics2D.gravity = world.GRAVITY;

		playerController.PlayerAvatar = objFactory.buildPlayer ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}

	void FixedUpdate(){

		world.update ();

	}

	// every 2 seconds perform the action
	private IEnumerator RoutineCreatePlatforms(float waitTime) {

		float ticks = 0;

		while (true) {

			yield return new WaitForSeconds(waitTime);

			//print (world.TRAVEL_DISTANCE);
		
			if ( world.TRAVEL_DISTANCE >= world.OBSTACLE_FREQUENCY * ticks){
				
				ActorComponent obstacle = objFactory.buildAerialPlatform();
				AdjustToBounds (obstacle);

				ticks += 1;
			}
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
