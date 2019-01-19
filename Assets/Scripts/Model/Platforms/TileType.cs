using System;
using UnityEngine;

public class TileType
{
	public int id;
	public Vector2 size;
	public Vector2 offset;

	public static TileType SURFACE_LEFT =   new TileType(1,  1,1);
	public static TileType SURFACE_CENTER = new TileType(2,  1,1);
	public static TileType SURFACE_RIGHT =  new TileType(3,  1,1);

	public static TileType PLATFORM_LEFT =   new TileType	(13,  1, 0.7f,  0, -0.15f);
	public static TileType PLATFORM_CENTER = new TileType	(14,  1, 0.7f,  0, -0.15f);
	public static TileType PLATFORM_RIGHT =  new TileType	(15,  1, 0.7f,  0, -0.15f);

	public static TileType BLOCKER = new TileType(21,  1,1);

	public TileType (int id, float w, float h, float x, float y)
	{
		this.id = id;
		size = new Vector2 (w, h);
		offset = new Vector2 (x, y);
	}

	public TileType (int id, float w, float h)
	{
		this.id = id;
		size = new Vector2 (w, h);
		offset = new Vector2 (0, 0);
	}
}


