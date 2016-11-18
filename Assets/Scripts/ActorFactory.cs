using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Director;

public class ActorFactory  {

	private int id = 0;

	#region SINGLETON

	private static ActorFactory instance;
	private ActorFactory(){
	}
	public static ActorFactory getInstance(){	
		if (instance == null){
			instance = new ActorFactory();

		}	
		return instance;
	}

	#endregion

	#region BUILDERS

	public PlayerComponent buildPlayer(){

		Player playerModel = new Player();

		GameObject actorGameObject = createGameObject(playerModel);


		addAnimator (actorGameObject, playerModel);
		addSpriteRenderer(actorGameObject, playerModel);

		foreach (ColliderInfo coll in playerModel.colliders) {
			addCollider2D (actorGameObject, coll);
		}


		addRigidbody2D(actorGameObject, playerModel);



		return addPlayerComponent(actorGameObject, playerModel);
	}
		
	public ActorComponent buildActor(Actor actorModel){

		GameObject actorGameObject = createGameObject(actorModel);

		addSpriteRenderer(actorGameObject, actorModel);

		foreach (ColliderInfo coll in actorModel.colliders) {
			addCollider2D (actorGameObject, coll);
		}

		if (actorModel.hasRigidbody) {
			addRigidbody2D (actorGameObject, actorModel);
		}

		return addActorComponent(actorGameObject, actorModel);
			
	}

	public ActorComponent buildAirPlatform(){

		return buildPlatform(Platform.AerialPlatform());

	}

	public ActorComponent buildGroundPlatform(FloorPlatformType type, float xPosition){

		return buildPlatform(Platform.FloorPlatform(type, xPosition));
	}

	private ActorComponent buildPlatform(Platform platformModel){
				
		GameObject actorGameObject = createGameObject(platformModel);

		foreach (PlatformTile tile in platformModel.tiles){
			
			GameObject tileGameObject = new GameObject();
			tileGameObject.name = "platform tile - " + tile.tileId;

			SpriteRenderer sr = tileGameObject.AddComponent<SpriteRenderer>();
			sr.sprite = Resources.Load<Sprite> ("Sprites/Tiles/" + tile.tileId);
			sr.sortingOrder = platformModel.depth;

			tileGameObject.transform.parent = actorGameObject.transform;
			tileGameObject.transform.localPosition = tile.position;

		}

		foreach (ColliderInfo coll in platformModel.colliders) {

			addCollider2D(actorGameObject, coll);
		}

		Rigidbody2D rb = addRigidbody2D(actorGameObject, platformModel);

		return addActorComponent(actorGameObject, platformModel);

	}

	#endregion

	private GameObject createGameObject(Actor actor){

		GameObject actorGameObject = new GameObject();
		actorGameObject.name = actor.name + " - ID: " + id++;
		actorGameObject.tag = actor.tag;

		actorGameObject.layer = actor.layer;

		actorGameObject.transform.position = actor.startingPosition;
		actorGameObject.transform.localScale = actor.scale;

		return actorGameObject;
	}



	private SpriteRenderer addSpriteRenderer(GameObject obj, Actor actor){

		SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
		sr.sprite = Resources.Load<Sprite> ("Sprites/" + actor.spriteKey);
		//sr.sprite = null;
		sr.color = actor.tint;
		sr.sortingOrder = actor.depth;

		return sr;

	}

	private Collider2D addCollider2D(GameObject obj, ColliderInfo collInfo){

		Collider2D coll;

		if (collInfo.type == ColliderType.Box) {

			coll = obj.AddComponent<BoxCollider2D> ();
			(coll as BoxCollider2D).size = collInfo.size;

		} else if (collInfo.type == ColliderType.Circle) {

			coll = obj.AddComponent<CircleCollider2D> ();
			(coll as CircleCollider2D).radius = collInfo.size.x;
		} else {
			return null;
		}

		coll.offset = collInfo.offset;
		coll.sharedMaterial = Resources.Load<PhysicsMaterial2D> ("PhysicsMaterials/" + collInfo.materialKey);
	
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

	private Animator addAnimator(GameObject obj, Character character){

		Animator anim = obj.AddComponent<Animator> ();

		//AnimationClip clip = Resources.Load<AnimationClip> ("Animations/" + character.name + "/" + character.animationKeys[0] );

		//var clipPlayable = AnimationClipPlayable.Create (clip);

		//anim.Play (clipPlayable);

		return anim;

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
