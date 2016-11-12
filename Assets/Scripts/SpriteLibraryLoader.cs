using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct SpriteLoader{

	public string key;
	public Sprite sprite;

}

public class SpriteLibraryLoader : MonoBehaviour {

	public SpriteLoader[] spriteLoaderList;
	
	void Awake(){
	
	
		Dictionary<string,Sprite> spriteLibrary = new Dictionary<string,Sprite>();
	
		foreach (SpriteLoader spr in spriteLoaderList){
			spriteLibrary.Add(spr.key, spr.sprite);
		}
		
		SpriteLibrary.getInstance().set(spriteLibrary);
	
	}

	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
