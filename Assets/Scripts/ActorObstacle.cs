using UnityEngine;
using System.Collections;

public class ActorObstacle : Actor {

	private Vector2 moveVector;

	protected override void Awake(){
		base.Awake();	
				
	}
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
	
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
							
	}
	
	protected override void FixedUpdate () {
		base.FixedUpdate();
		
		
	}
	
	public void setSpeed( Vector2 moveVector){
		
		this.moveVector = moveVector + world.BASE_SPEED;
		this.rb.velocity = this.moveVector;
	
	}
	
}
