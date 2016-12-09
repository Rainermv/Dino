using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Character : Actor  {

	public Dictionary<CharacterAnimationState, string> animationKeys = new Dictionary<CharacterAnimationState,string>();
	public CharacterAnimationState initialState;

	public Vector2 jumpForce;
	public bool isTouchingFloor = true;

	protected void setCollider(Vector2 size, Vector2 colliderOffset){

		ColliderInfo boxCollider = new ColliderInfo ();
		boxCollider.type = ColliderType.Box;
		//boxCollider.size = new Vector2(width,height);
		boxCollider.size = size;
		//boxCollider.offset = new Vector2(offset_x, -0.1f);
		boxCollider.offset =colliderOffset;
		boxCollider.materialKey = "Player";
		colliders.Add (boxCollider);

	}

}
