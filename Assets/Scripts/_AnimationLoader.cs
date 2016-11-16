using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct AnimationControllerLoaderInstance {

	public string key;
	public AnimationClip clip;

}

public class _AnimationLoader : MonoBehaviour {

	public AnimationControllerLoaderInstance[] animationControllers;

	// Use this for initialization
	void Start () {

		Dictionary<string, AnimationClip> dictionary = new Dictionary<string, AnimationClip> ();

		foreach (AnimationControllerLoaderInstance anim in animationControllers){

			dictionary.Add (anim.key, anim.clip);

		}

		_AnimationLibrary.getInstance ().animations = dictionary;

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
