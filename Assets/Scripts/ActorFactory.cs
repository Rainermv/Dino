﻿using UnityEngine;
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
		GameObject actorView = createChild (actorGameObject, "Player view", Vector2.zero);

		addAnimator (actorView, playerModel);
		addSpriteRenderer(actorView, playerModel);

		foreach (ColliderInfo coll in playerModel.colliders) {
			addCollider2D (actorGameObject, coll);
		}
			
		addRigidbody2D(actorGameObject, playerModel);

		return addPlayerComponent(actorGameObject, playerModel);
	}

	public EnemyComponent buildEnemy(){

		Enemy enemyModel = new Enemy();

		GameObject actorGameObject = createGameObject(enemyModel);
		GameObject actorView = createChild (actorGameObject, "Enemy view", Vector2.zero);

		addAnimator (actorView, enemyModel);
		addSpriteRenderer(actorView, enemyModel);

		foreach (ColliderInfo coll in enemyModel.colliders) {
			addCollider2D (actorGameObject, coll);
		}

		addRigidbody2D(actorGameObject, enemyModel);

		EnemyComponent enemyComponent = addEnemyComponent(actorGameObject, enemyModel);

		AI enemyAI = AILoader.getInstance ().GetAI (enemyModel.AIKey);
		enemyComponent.EnemyAI = enemyAI;

		return enemyComponent;
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

		actor.id = id++;
		actorGameObject.name = actor.name + " - ID: " + actor.id;

		actorGameObject.tag = actor.tag;
		actorGameObject.layer = actor.layer;

		actorGameObject.transform.position = actor.startingPosition;
		actorGameObject.transform.localScale = actor.scale;

		return actorGameObject;
	}

	
	private GameObject createChild( GameObject parentGameObject, string name,  Vector2 localPosition ){

		GameObject child = new GameObject();

		child.name = name;

		child.tag = parentGameObject.tag;
		child.layer = parentGameObject.layer;

		child.transform.parent = parentGameObject.transform;

		child.transform.localPosition = localPosition;

		return child;

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
		}
		else if (collInfo.type == ColliderType.Edge) {

			coll = obj.AddComponent<EdgeCollider2D> ();

			Vector2[] points = new Vector2[2];
			points [0] = new Vector2 (-collInfo.size.x /2, 0);
			points [1] = new Vector2 ( collInfo.size.x /2, 0);

			(coll as EdgeCollider2D).points = points;

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
		//act.actor.id = id++;

		return act;

	}

	private EnemyComponent addEnemyComponent(GameObject obj, Enemy actorEnemy){

		EnemyComponent el = obj.AddComponent<EnemyComponent>();
		el.actor = actorEnemy;
		//el.actor.id = id++;

		return el;

	}

	private PlayerComponent addPlayerComponent(GameObject obj, Player actorPlayer){

		PlayerComponent pl = obj.AddComponent<PlayerComponent>();
		pl.actor = actorPlayer;
		//pl.actor.id = id++;

		return pl;

	}
	
	
}
