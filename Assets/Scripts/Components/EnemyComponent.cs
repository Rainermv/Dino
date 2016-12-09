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
	}

	protected override void Update () {
		base.Update ();
	}

	protected override void FixedUpdate () {
		base.FixedUpdate();
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
