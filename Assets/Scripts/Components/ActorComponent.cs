using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Model;

public class ActorComponent : MonoBehaviour {
	
	protected Rigidbody2D RigidBody;
	protected Collider2D[] Colliders;

	protected World World;
	public Actor Actor;

    SpriteRenderer _spriteRenderer;

    //List<GameObject> childrendObjects = new List<GameObject>();


    public Vector2 GetSpriteSize(){

		Vector2 size = Vector2.zero;

		if (this._spriteRenderer != null) {

			Bounds b = _spriteRenderer.bounds;

			//size = new Vector2 (b.max.x - b.min.x, b.

		}

		return size;



	}

	//public Vector2 moveVector = Vector2.zero;
				
	protected virtual void Awake(){
		
		RigidBody = GetComponent<Rigidbody2D>();
		Colliders =  GetComponents<Collider2D>();

        _spriteRenderer = GetComponentsInChildren<SpriteRenderer>()[0];

        World = World.GetInstance();
	}

	
	// Use this for initialization
	protected virtual void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {

		if (transform.position.x < World.XRemove && !Actor.indestructable) {

			Destroy (gameObject);

		}

	}
	
	protected virtual void FixedUpdate ()
    {
        if (!Actor.isKinematic)
            return;

        if (Actor.hasRigidbody) {
            //moveVector = actor.velocity;

            RigidBody.MovePosition (RigidBody.position + (World.BaseSpeed * Actor.worldMovementMultiplier) * Time.fixedDeltaTime);
            //moveVector += world.BASE_SPEED;
            return;
        }

        Vector2 pos2d = transform.position;
        transform.position = pos2d + (World.BaseSpeed * Actor.worldMovementMultiplier) * Time.fixedDeltaTime;

        //transform.position = transform.position + (world.BASE_SPEED * actor.worldMovementMultiplier) * Time.fixedDeltaTime;



        //this.rb.velocity = velocityVector;
    }

	/*
	public void setSpeed( Vector2 moveVector){

		moveVector += world.BASE_SPEED;
		this.rb.velocity = moveVector;

	}
	*/
	protected virtual void OnCollisionEnter2D(Collision2D collision){

	}

	protected virtual void OnCollisionExit2D(Collision2D collision){

	}

	protected virtual void OnCollisionStay2D(Collision2D collision){

	}

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {

    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {

    }

    protected virtual void OnTriggerStay2D(Collider2D collider)
    {

    }


    public Bounds getBounds(){

		Bounds bounds = new Bounds();

		foreach (Collider2D coll in Colliders) {

			bounds.Encapsulate (coll.bounds);

		}

		return bounds;

	}

    public Bounds getRendererBounds() {

        return _spriteRenderer.bounds;

    }

	public Collider2D[] getColliders(){
		return Colliders;
	}


	/*
	public void ActionJump(){
		
		rb.AddForce(actor.jumpForce);
		
	}
	
	protected virtual void OnCollisionEnter2D(Collision2D collision){
		
		if (collision.gameObject.tag == "FLOOR"){
			isTouchingFloor = true;
		}
	}
	
	protected virtual void OnCollisionExit2D(Collision2D collision){
		
		if (collision.gameObject.tag == "FLOOR"){
			isTouchingFloor = false;
		}
	}
	
	public void setSpeed( Vector2 moveVector){
		
		moveVector += world.BASE_SPEED;
		this.rb.velocity = this.moveVector;
	
	}
	*/
}
