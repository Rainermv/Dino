using UnityEngine;
using System.Collections;
using Assets.Scripts.Model;

//using UnityEngine.Director;

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

    public PickupComponent buildPickup(PickupEffectType effectType)
    {
        Pickup actorPickup = new Pickup(effectType);

        GameObject actorGameObject = createGameObject(actorPickup);
        addRigidbody2D(actorGameObject, actorPickup);

        foreach (ColliderInfo coll in actorPickup.colliders) {
            addCollider2D(actorGameObject, coll);
        }

        GameObject actorView = createChild(actorGameObject, "Pickup view", Vector2.zero);
        addSpriteRenderer(actorView, actorPickup);

        return addPickupComponent(actorGameObject, actorPickup);
    }

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

	public EnemyComponent buildEnemy(string type){

        EnemyComponent enemy = null;

        switch (type) {
            case "ZOMBIE":
                enemy = buildEnemyZombie();
                break;

            case "BUMPER":
                enemy = buildEnemyBumper();
                break;

            default: 
                enemy = buildEnemyBumper();
                break;
        }

        return enemy;
		
	}

    public EnemyComponent buildEnemyZombie() {

        Zombie enemyModel = new Zombie();

        GameObject actorGameObject = createGameObject(enemyModel);
        GameObject actorView = createSprites(actorGameObject, enemyModel);
        //GameObject actorView = createChild(actorGameObject, "Enemy view", Vector2.zero);

        addAnimator(actorView, enemyModel);
       // addSpriteRenderer(actorView, enemyModel);

        foreach (ColliderInfo coll in enemyModel.colliders) {
            addCollider2D(actorGameObject, coll);
        }

        addRigidbody2D(actorGameObject, enemyModel);

        EnemyComponent enemyComponent = addEnemyComponent(actorGameObject, enemyModel);

        AI enemyAI = AILoader.getInstance().GetAI(enemyModel.AIKey);
        enemyComponent.EnemyAI = enemyAI;

        return enemyComponent;

    }

    public EnemyComponent buildEnemyBumper() {

        Bumper bumperModel = new Bumper();

        GameObject actorGameObject = createGameObject(bumperModel);
        GameObject actorView = createSprites(actorGameObject, bumperModel);

        addAnimator(actorView, bumperModel);

        foreach (ColliderInfo coll in bumperModel.colliders) {
            addCollider2D(actorGameObject, coll);
        }

        addRigidbody2D(actorGameObject, bumperModel);

        EnemyComponent enemyComponent = addEnemyComponent(actorGameObject, bumperModel);

        AI enemyAI = AILoader.getInstance().GetAI(bumperModel.AIKey);
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

    public ActorComponent buildBackground(Background backgroundModel) {

        ActorComponent actor = this.buildActor(backgroundModel);

        return actor;

    }

    public ActorComponent buildProp(Prop propModel) {

        GameObject actorGameObject = createGameObject(propModel);

        addSpriteRenderer(actorGameObject, propModel);

        return addActorComponent(actorGameObject, propModel);

    }

	public ActorComponent buildAirPlatform(PlatformGenerationStrategy platformGenerationStrategy){

		return buildPlatform(Platform.AerialPlatform(platformGenerationStrategy));

	}

	public ActorComponent buildGroundPlatform(FloorPlatformType type, float xPosition){

        var platform = this.buildPlatform(Platform.FloorPlatform(type, xPosition));

        return platform;
	}

	private ActorComponent buildPlatform(Platform platformModel){
				
		var actorGameObject = createGameObject(platformModel);

		foreach (var tile in platformModel.tiles){
			
			var tileGameObject = new GameObject();
			tileGameObject.name = "platform tile - " + tile.tileId;

			var sr = tileGameObject.AddComponent<SpriteRenderer>();
			sr.sprite = Resources.Load<Sprite> ("Sprites/Tiles/" + tile.tileId);
			sr.sortingOrder = platformModel.sortingOrder;

			tileGameObject.transform.parent = actorGameObject.transform;
			tileGameObject.transform.localPosition = tile.position;

		}

		foreach (ColliderInfo coll in platformModel.colliders) {

			addCollider2D(actorGameObject, coll);
		}

        foreach (EffectorInfo effector in platformModel.effectors) {
            addEffector2D(actorGameObject, effector);
        }

		var rb = addRigidbody2D(actorGameObject, platformModel);

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

    private GameObject createSprites(GameObject parent, Actor actorModel ) {

        GameObject baseSprite = this.createChild(parent, actorModel.name, actorModel.startingPosition);
        addSpriteRenderer(baseSprite, actorModel);

        foreach (SpriteComposition sprite in actorModel.childrenSprites) {
            GameObject childSprite = this.createChild(parent, sprite.name, sprite.startingPosition);
            SpriteRenderer sprRend = addSpriteRenderer(childSprite, sprite);

            sprite.onChangeSpriteKey = delegate () {

                sprRend.sprite = getSprite(sprite.spriteKey);

            };

        }

        return baseSprite;

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

        sr.sprite = this.getSprite(actor.spriteKey);

		//sr.sprite = null;
		sr.color = actor.tint;
		sr.sortingOrder = actor.sortingOrder;

		return sr;

	}

    public Sprite getSprite(string spriteKey) {

        string spritePath = "Sprites/" + spriteKey;
        Sprite sprite = Resources.Load<Sprite>(spritePath);

        if (sprite == null) {
            Debug.LogWarning("Sprite " + spritePath + " not found");
        }

        return sprite;

    }

    private Effector2D addEffector2D(GameObject obj, EffectorInfo effector) {

        switch (effector.type){

            case EffectorType.Platform:
                PlatformEffector2D eff2D;
                eff2D = obj.AddComponent<PlatformEffector2D>();
                eff2D.useOneWay = effector.oneWay;
                eff2D.useColliderMask = false;
                eff2D.surfaceArc = effector.surfaceArc;
                break;

        }


        return null;
 
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
        coll.isTrigger = collInfo.trigger;
        coll.usedByEffector = collInfo.usedByEffector;
	
		return coll;

	}

	private Rigidbody2D addRigidbody2D(GameObject obj, Actor actor){

		Rigidbody2D rb = obj.AddComponent<Rigidbody2D> ();
		rb.isKinematic = actor.isKinematic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
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
		act.Actor = actor;
		//act.actor.id = id++;

		return act;

	}

	private EnemyComponent addEnemyComponent(GameObject obj, Character actorEnemy){

		EnemyComponent ec = obj.AddComponent<EnemyComponent>();
		ec.Actor = actorEnemy;
		//el.actor.id = id++;

		return ec;

	}

    private PickupComponent addPickupComponent(GameObject obj, Pickup actorPickup) {

        PickupComponent pickupComponent = obj.AddComponent<PickupComponent>();
        pickupComponent.Actor = actorPickup;

        return pickupComponent;
    }

	private PlayerComponent addPlayerComponent(GameObject obj, Player actorPlayer){

		PlayerComponent pl = obj.AddComponent<PlayerComponent>();
		pl.Actor = actorPlayer;

		return pl;

	}
	
	
}
