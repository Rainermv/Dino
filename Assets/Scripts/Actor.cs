using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {
	
	protected Rigidbody2D rb;
	protected World world;
	
	protected Vector2 jumpForce;
	
	protected bool isTouchingFloor = true;
	
	protected virtual void Awake(){
		
		rb = GetComponent<Rigidbody2D>();
		world = World.getInstance();
	}
	
	// Use this for initialization
	protected virtual void Start () {
	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}
	
	protected virtual void FixedUpdate () {
	
	}
		
	public void ActionJump(){
		
		rb.AddForce(jumpForce);
		
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
}
