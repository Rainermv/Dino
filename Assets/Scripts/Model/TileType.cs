using System;
using UnityEngine;

public class TileType
{
	public int id;
	public Vector2 size;

	public static TileType SNOW_SURFACE_CENTER = new TileType(0,1,1);

	public static TileType SNOW_PLATFORM_LEFT = new TileType(14,1,0.8f);
	public static TileType SNOW_PLATFORM_CENTER = new TileType(15,1,0.8f);
	public static TileType SNOW_PLATFORM_RIGHT = new TileType(16,1,0.8f);

	public static TileType ICE = new TileType(20,1,1);

	public TileType (int id, float w, float h)
	{
		this.id = id;
		size = new Vector2 (w, h);
	}
}


