﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (SpriteRenderer))]
public class ActorComponent : MonoBehaviour {
	
	protected Rigidbody2D rb;
	protected Collider2D coll;

	protected World world;
	public Actor actor;

	private Vector3 oldPosition;

	public Vector3 velocity;

	public Vector2 moveVector = Vector2.zero;
				
	protected virtual void Awake(){
		
		rb = GetComponent<Rigidbody2D>();
		coll =  GetComponent<Collider2D>();

		world = World.getInstance();
	}

	
	// Use this for initialization
	protected virtual void Start () {

		oldPosition = transform.position;
	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		calculateVelocity ();
	}

	private void calculateVelocity(){

		velocity = (transform.position - oldPosition) * Time.deltaTime;
		oldPosition = transform.position;
	}
	
	protected virtual void FixedUpdate () {

		if (actor.affectedByWorldMovement) {
		//moveVector = actor.velocity;

			rb.MovePosition (rb.position + moveVector + world.BASE_SPEED * Time.fixedDeltaTime);
			//moveVector += world.BASE_SPEED;
		}
			

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


	public Bounds getBounds(){

		return coll.bounds;

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