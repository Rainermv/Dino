using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (InterfaceController))]
public class GameController : MonoBehaviour {
	
	private ActorFactory objFactory;
	private World world;
	private InterfaceController playerController;

	private ActorComponent[] backgroundArray;

	public bool createAirPlatforms = true;
	public bool manageStrategies = true;
	public bool increaseDifficulty = true;

	//float multiplier = 0.1f;
	
	//private IEnumerator routineCreateObstacles;
	//private IEnumerator routineMultiply;

	void Awake(){
	
		objFactory = ActorFactory.getInstance();
		world = World.getInstance();

		playerController = GetComponent<InterfaceController> ();
	
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
		backgroundArray[1] = objFactory.buildActor (new Background ());
		backgroundArray[2] = objFactory.buildActor (new Background ());

        StartCoroutine(RoutineParallaxBackground());

		CreateInitialPlatforms ();

        
	
	}
	
	// Update is called once per frame
	void Update () {

		


	
	}

    void onStarsPickupChange(int value) {



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

						ActorComponent platform = objFactory.buildGroundPlatform (type, world.X_SPAWN - diff);

                        //if (Random.Range(0, 1) <= world.PROP_SPAWN_CHANCE_FLOOR) {

                        SpawnProp(platform, objFactory.buildProp(new Prop()));

                        if (Random.Range(0, 1f) <= world.ENEMY_SPAWN_CHANCE_GROUND) {

                            float translate = Random.Range(-world.FLOOR_PLATFORM_SIZE *0.5f, world.FLOOR_PLATFORM_SIZE *0.5f);
                            SpawnActor(platform, objFactory.buildEnemy("BUMPER"), translate);

                        }
                        if (Random.Range(0, 1f) <= world.PICKUP_SPAWN_CHANCE_GROUND) {

                            float translate = Random.Range(-world.FLOOR_PLATFORM_SIZE * 0.5f, world.FLOOR_PLATFORM_SIZE * 0.5f);
                            SpawnActor(platform, objFactory.buildPickup(PickupEffectType.STAR), translate);

                        }

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

    private IEnumerator RoutineCreateAirPlatforms() {

        float airTicks = 0;
        //float floorTicks = 0;

        while (true) {

            if (world.TRAVEL_DISTANCE >= world.PLATFORM_FREQUENCY * airTicks) {

                if (createAirPlatforms) {
                    ActorComponent platform = objFactory.buildAirPlatform();
                    AdjustToBounds(platform);

                    float platformSizeX = platform.getRendererBounds().size.x;

                    if (Random.Range(0, 1f) <= world.ENEMY_SPAWN_CHANCE_PLATFORMS) {

                        float translate = Random.Range(-platformSizeX * 0.5f, platformSizeX * 0.5f);
                        SpawnActor(platform, objFactory.buildEnemy("BUMPER"), translate);

                    }
                    if (Random.Range(0, 1f) <= world.PICKUP_SPAWN_CHANCE_PLATFORMS) {

                        //float translate = Random.Range(-platformSizeX * 0.5f, platformSizeX * 0.5f);
                        SpawnActor(platform, objFactory.buildPickup(PickupEffectType.STAR), 0);

                    }

                }

                airTicks += 1;
            }

            yield return null;

        }
    }

    private IEnumerator RoutineParallaxBackground(){

        //float PARALAX_SPEED = 0.05f;

        float sizeX = backgroundArray[0].getRendererBounds().size.x;

        //float ticks = 0;

        float p = world.SCREEN_LEFT;
		foreach (ActorComponent backgroundActor in backgroundArray) {

			Transform t = backgroundActor.transform;
			Background bg = backgroundActor.actor as Background;

            t.position = bg.startingPosition + new Vector2(p, t.position.y);

            p += sizeX;
		}


        /*
		while (true) {

            foreach (ActorComponent backgroundActor in backgroundArray) {

                Transform t = backgroundActor.transform;
                Background bg = backgroundActor.actor as Background;

                if (t.position.x <= world.SCREEN_LEFT - (sizeX / 2)) {
                    t.position = new Vector2(world.SCREEN_RIGHT + (sizeX / 2), t.position.y);

                    
                } else {
                    // float newX = t.position.x + (world.BASE_SPEED.x * PARALAX_RATE);
                    float newX = t.position.x - (PARALAX_SPEED * Time.deltaTime);
                    t.position = new Vector2(newX, t.position.y);
                }

                print(t.position);



            }
        

            //if ( world.TRAVEL_DISTANCE >= world.PLATFORM_FREQUENCY * ticks){

			//    ticks += 1;
			//}

			yield return null;

		}
        */

        yield return null;

    }

    

    private void SpawnActor(ActorComponent platform, ActorComponent actor, float translateX)
    {

        //float platformSizeY = (platform.getColliders()[0] as BoxCollider2D).size.y;
        float platformSizeY = platform.getRendererBounds().size.y;

        BoxCollider2D collider = actor.getColliders()[0] as BoxCollider2D;
        float sizeY = collider.size.y;

        actor.transform.position = platform.transform.position;
        actor.transform.Translate(
            translateX, 
            platformSizeY * 0.5f + sizeY * 0.5f - collider.offset.y , 
            0);

    }

    private void SpawnProp(ActorComponent platform, ActorComponent prop) {

        float platformSizeY = platform.getRendererBounds().size.y;
        float propSizeY = prop.getRendererBounds().size.y;

        prop.transform.position = platform.transform.position;
        prop.transform.Translate(0, platformSizeY * 0.5f + propSizeY * 0.5f, 0);

    }

	private IEnumerator RoutineIncreaseDifficulty() {
		while (true) {

			yield return new WaitForSeconds(world.DIFICULTY_INCREASE_TIME);

			if (increaseDifficulty){

                float x = world.BASE_SPEED.x - world.DIFFICULTY_INCREASE;
                float y = world.BASE_SPEED.y;

                world.BASE_SPEED = new Vector2(x, y);
				world.SPAWN_TIMER -= world.SPAWN_TIMER - world.DIFFICULTY_INCREASE;
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

	


}
