using UnityEngine;
using System.Collections;

public class CharacterComponent : ActorComponent {

	LayerMask mask = 9; // Character layer
	RaycastHit2D[] raycasts;

	Character character;

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		character = actor as Character;

	}

	public bool isTouchingFloor(){

		RaycastHit2D cast = Physics2D.Linecast (transform.position, getBounds ().min * 1.1f, mask.value);

		if (cast.collider.tag == "FLOOR") {
			print ("colliding");
			return true;
		}

		return false;


	}
		
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();


	
	}

	public void ActionJump(){

		rb.AddForce( (actor as Character).jumpForce);

	}

	protected override void OnCollisionEnter2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);

		if (collision.gameObject.tag == "FLOOR"){
			character.isTouchingFloor = this.isTouchingFloor();
		}
	}

	protected override void OnCollisionExit2D(Collision2D collision){
		base.OnCollisionExit2D(collision);

		if (collision.gameObject.tag == "FLOOR"){
			character.isTouchingFloor = this.isTouchingFloor();
		}
	}


}
