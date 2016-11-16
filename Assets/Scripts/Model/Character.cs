using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Character : Actor  {


	public Dictionary<CharacterAnimationState, string> animationKeys = new Dictionary<CharacterAnimationState,string>();
	public CharacterAnimationState initialState;

	public Vector2 jumpForce;
	public bool isTouchingFloor = true;

}
