using UnityEngine;
using System.Collections;

public class EnemyComponent : CharacterComponent {

	public Enemy enemy;

	protected override void Start () {
		base.Start ();

		transform.localScale = new Vector3(
			transform.localScale.x * -1,
			transform.localScale.y,
			transform.localScale.z
		);

		enemy = actor as Enemy;

		SetState (CharacterAnimationType.RUN);
	}

	protected override void Update () {
		base.Update ();

		if (currentCharacterState.animationType != CharacterAnimationType.DEAD && enemy.isAlive == false) {

			//rb.AddForce (new Vector2(5000f, 0f));
			SetState (CharacterAnimationType.DEAD);
		}

	}

	public void Kill(){
		enemy.isAlive = false;
	}

	protected override void FixedUpdate () {
		base.FixedUpdate();


		if (currentCharacterState.animationType == CharacterAnimationType.IDLE ||
			currentCharacterState.animationType == CharacterAnimationType.DEAD) {
			
			dynamicVelocity = new Vector2 (0f, 0f);

		} else if (currentCharacterState.animationType == CharacterAnimationType.RUN) {
			
			dynamicVelocity = new Vector2 (-1f, 0f);

		}

		rb.velocity = new Vector2 (world.BASE_SPEED.x, rb.velocity.y) + dynamicVelocity;
	}

	protected override void OnCollisionEnter2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);
	}

	protected override void OnCollisionExit2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);
	}

	protected override void OnCollisionStay2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);
	}
}
