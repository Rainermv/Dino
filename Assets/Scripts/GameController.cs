using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class GameController : MonoBehaviour {
	
	private ObjectFactory objFactory;
	private World world;
	
	private IEnumerator routineCreateObstacles;

	void Awake(){
	
		objFactory = ObjectFactory.getInstance();
		world = World.getInstance();
	
	}
		
	// Use this for initialization
	void Start () {
		
		routineCreateObstacles = RoutineCreateObstacles(2);
		StartCoroutine(routineCreateObstacles);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}
	
	// every 2 seconds perform the action
    private IEnumerator RoutineCreateObstacles(float waitTime) {
        while (true) {
            yield return new WaitForSeconds(waitTime);
			
			float x = world.X_SPAWN;
			float y = Random.Range(world.SCREEN_BOTTOM + 1.5f , world.SCREEN_TOP);
			InstantiateObject(new Vector3 (x,y,0));

        }
    }
	
	void InstantiateObject(Vector3 position){
		
		GameObject box = objFactory.buildBox(position);
		
	}
}
