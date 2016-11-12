using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteLibrary  {

	private Dictionary<string,Sprite> spriteDictionary = new Dictionary<string,Sprite>();

	
	// SINGLETON
	private static SpriteLibrary instance;
	private SpriteLibrary(){
		
	}
	public static SpriteLibrary getInstance(){
		if (instance == null){
			instance = new SpriteLibrary();
		}
		return instance;
	}
	
	public void set(Dictionary<string,Sprite> spriteDictionary){
		this.spriteDictionary = spriteDictionary;
	}
	
	public Sprite getSprite(string key){
		
		return spriteDictionary[key];
	
	}
}
