using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	private ObjectFactory objFactory;
	private World world;

	float multiplier = 0.1f;
	
	private IEnumerator routineCreateObstacles;
	private IEnumerator routineMultiply;

	void Awake(){
	
		objFactory = ObjectFactory.getInstance();
		world = World.getInstance();
	
	}
		
	// Use this for initialization
	void Start () {
		
		routineCreateObstacles = RoutineCreateObstacles(0.1f);
		StartCoroutine(routineCreateObstacles);

		routineMultiply = RoutineMultiply(world.MULT_TIMER);
		StartCoroutine(routineMultiply);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}

	void FixedUpdate(){

		world.update ();



		//world.TRAVEL_DISTANCE.x world.OBSTACLE_FREQUENCY);

	}

	// every 2 seconds perform the action
	private IEnumerator RoutineCreateObstacles(float waitTime) {

		float ticks = 0;

		while (true) {

			yield return new WaitForSeconds(waitTime);

			print (world.TRAVEL_DISTANCE);

			if ( world.TRAVEL_DISTANCE >= world.OBSTACLE_FREQUENCY * ticks){
				float x = world.X_SPAWN;
				float y = Random.Range(world.SCREEN_BOTTOM, world.SCREEN_TOP);
				//float y = Random.Range(0, world.SCREEN_TOP);
				ActorComponent obstacle = InstantiateActor(new Vector3 (x,y,0));

				AdjustToBounds (obstacle);

				ticks += 1;
			}
		}
	}

	private IEnumerator RoutineMultiply(float waitTime) {
		while (true) {
			yield return new WaitForSeconds(waitTime);

			world.BASE_SPEED *= world.MULTIPLIER;
			world.SPAWN_TIMER -= world.SPAWN_TIMER * world.MULTIPLIER;


		}
	}
	
	// every 2 seconds perform the action
    private IEnumerator RoutineCreateOther(float waitTime) {
        while (true) {
            yield return new WaitForSeconds(waitTime);
			
			float x = world.X_SPAWN;
			float y = Random.Range(world.SCREEN_BOTTOM, world.SCREEN_TOP);
			//float y = Random.Range(0, world.SCREEN_TOP);
			ActorComponent obstacle = InstantiateActor(new Vector3 (x,y,0));

			AdjustToBounds (obstacle);

        }
    }

	ActorComponent InstantiateActor(Vector3 position){
		
		return objFactory.buildActor(position);

	}

	void AdjustToBounds(ActorComponent obj){

		float max = obj.getBounds().max.y;
		float min = obj.getBounds().min.y;


		float diff = 0;
		if (max > world.SCREEN_TOP) {
			diff = world.SCREEN_TOP - max;
		}
		else if (min < world.SCREEN_BOTTOM){
			diff = world.SCREEN_BOTTOM - min;
		}

		obj.transform.Translate (0, diff, 0);
	}
}
