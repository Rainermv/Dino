using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _AnimationLibrary {

	private static _AnimationLibrary instance;

	public Dictionary<string, AnimationClip> animations = new Dictionary<string, AnimationClip> ();

	private _AnimationLibrary(){
		
	}
	public static _AnimationLibrary getInstance(){
		if (instance == null){
			instance = new _AnimationLibrary();
		}
		return instance;
	}
}
