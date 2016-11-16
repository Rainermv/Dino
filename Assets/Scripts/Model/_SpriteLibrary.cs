using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _SpriteLibrary  {

	private Dictionary<string,Sprite> spriteDictionary = new Dictionary<string,Sprite>();

	
	// SINGLETON
	private static _SpriteLibrary instance;
	private _SpriteLibrary(){
		
	}
	public static _SpriteLibrary getInstance(){
		if (instance == null){
			instance = new _SpriteLibrary();
		}
		return instance;
	}
	
	public void set(Dictionary<string,Sprite> spriteDictionary){
		this.spriteDictionary = spriteDictionary;
	}
	
	public Sprite getSprite(string key){

		if (spriteDictionary.ContainsKey(key)){
			return spriteDictionary[key];
		}
		
		Debug.LogError("SpriteLibrary: image with key " + key + " not found!!");

		return null;
	
	}
}
