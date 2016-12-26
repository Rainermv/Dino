using System;
using UnityEngine;

public class CharacterRay
{
	public RaycastHit2D raycast;
	public Vector2 origin;
	public Vector2 direction;
	public Color debugColor;

	public CharacterRay(Vector2 origin,Vector2 direction, Color debugColor ){

		this.raycast = new RaycastHit2D();
		this.origin = origin;
		this.direction = direction;
		this.debugColor = debugColor;
	}
}

