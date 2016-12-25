using System;
using UnityEngine;

public class CharacterAnimationState
{
	public CharacterAnimationType animationType;
	public string animationKey;
	public Vector2 animationOffset;

	public CharacterAnimationState (CharacterAnimationType animationType,  string animationKey, Vector2 animationOffset)
	{
		this.animationType = animationType;
		this.animationKey = animationKey;
		this.animationOffset = animationOffset;

	}
}


