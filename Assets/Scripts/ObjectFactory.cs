using UnityEngine;
using System.Collections;

public class ObjectFactory  {

	private int id = 0;
	private SpriteLibrary spriteLibrary;


	#region SINGLETON

	private static ObjectFactory instance;
	private ObjectFactory(){
		spriteLibrary = SpriteLibrary.getInstance();
	}
	public static ObjectFactory getInstance(){	
		if (instance == null){
			instance = new ObjectFactory();

		}	
		return instance;
	}

	#endregion

	#region BUILDERS

	public PlayerComponent buildPlayer(){

		Player playerModel = new Player();

		GameObject actorGameObject = createGameObject(playerModel);

		SpriteRenderer spriteRendererComponent = addSpriteRenderer(actorGameObject, playerModel);
		BoxCollider2D collider = addBoxCollider2D(actorGameObject, playerModel);
		Rigidbody2D rb = addRigidbody2D(actorGameObject, playerModel);

		return addPlayerComponent(actorGameObject, playerModel);
	}


	
	public ActorComponent buildActor(){

		Actor actorModel = new ObstacleBox();

		GameObject actorGameObject = createGameObject(actorModel);

		SpriteRenderer spriteRendererComponent = addSpriteRenderer(actorGameObject, actorModel);
		BoxCollider2D collider = addBoxCollider2D(actorGameObject, actorModel);
		Rigidbody2D rb = addRigidbody2D(actorGameObject, actorModel);

		return addActorComponent(actorGameObject, actorModel);
			
	}

	#endregion

	private GameObject createGameObject(Actor actor){

		GameObject actorGameObject = new GameObject();
		actorGameObject.name = actor.name + " - ID: " + id++;
		actorGameObject.tag = actor.tag;

		actorGameObject.layer = actor.layer;

		actorGameObject.transform.position = actor.startingPosition;
		actorGameObject.transform.localScale = actor.size;

		return actorGameObject;
	}



	private SpriteRenderer addSpriteRenderer(GameObject obj, Actor actor){


		SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
		sr.sprite = Resources.Load<Sprite> ("Sprites/" + actor.spriteKey);
		sr.color = actor.tint;

		return sr;

	}

	private BoxCollider2D addBoxCollider2D(GameObject obj, Actor actor){

		BoxCollider2D coll = obj.AddComponent<BoxCollider2D>();
		coll.size = new Vector2 (1,1);		
		coll.sharedMaterial = Resources.Load<PhysicsMaterial2D> ("PhysicsMaterial/" + actor.physicsMaterialKey);

		return coll;

	}

	private Rigidbody2D addRigidbody2D(GameObject obj, Actor actor){

		Rigidbody2D rb = obj.AddComponent<Rigidbody2D> ();
		rb.isKinematic = actor.isKinematic;
		//rb.

		if (actor.constrainMovement){
			rb.constraints = rb.constraints ^ RigidbodyConstraints2D.FreezePositionX;
		}
		if (actor.constrainRotation){
			rb.constraints = rb.constraints ^ RigidbodyConstraints2D.FreezeRotation;
		}
		


		return rb;
	}

	private ActorComponent addActorComponent(GameObject obj, Actor actor){

		ActorComponent act = obj.AddComponent<ActorComponent>();
		act.actor = actor;
		act.actor.id = id++;

		return act;

	}

	private PlayerComponent addPlayerComponent(GameObject obj, Player actorPlayer){

		PlayerComponent pl = obj.AddComponent<PlayerComponent>();
		pl.actor = actorPlayer;
		pl.actor.id = id++;

		return pl;

	}
	
	
}
