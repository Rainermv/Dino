using UnityEngine;
using System.Collections;

public class EnemyComponent : CharacterComponent {

	public Character enemy;

	private AI enemyAI;

	public AI EnemyAI {
		set { enemyAI = value; }
	}

	/*
	public void setAI(AI enemyAI){
		this.enemyAI = enemyAI;
	}
	*/

	protected override void Start () {
		base.Start ();

		enemy = actor as Character;

		SetState (CharacterAnimationType.RUN);

		enemyAI.StartAI ( this );
	}

	protected override void Update () {
		base.Update ();

		enemyAI.UpdateAI (this);



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
