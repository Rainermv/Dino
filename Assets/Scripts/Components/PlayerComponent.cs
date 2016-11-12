using UnityEngine;
using System.Collections;

public class PlayerComponent : CharacterComponent {

	private Player player;
			
	protected override void Awake(){
		base.Awake();	
				
	}
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
		
		rb = GetComponent<Rigidbody2D>();

		this.player = new Player();
		this.actor = player;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
							
	}
	
	public void PlayerJump(){
		
		if (player.jumps > 0){
			rb.velocity = Vector2.zero;
			rb.AddForce(player.jumpForce);
			
			player.jumps--;
		}

		
	}
	
	protected override void OnCollisionEnter2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);
			
		if (collision.gameObject.tag == "FLOOR"){
			
			foreach (ContactPoint2D contact in collision.contacts) {
				
				// Get a jump if the player jump on the top of a floor
				if (contact.normal.y >= 0.75){
					player.jumps = player.maxJumps;
				}
			}
		
			
		}
	}
	
}
