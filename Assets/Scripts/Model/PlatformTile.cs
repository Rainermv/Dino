using System;
using UnityEngine;



public class PlatformTile
{
	public int tileId;

	public Vector2 position = new Vector2(0,0);
	public Vector2 size = new Vector2(0,0);

	public PlatformTile (TileType tileType)
	{
		this.tileId = tileType.id;
		this.size = tileType.size;

	}

}

