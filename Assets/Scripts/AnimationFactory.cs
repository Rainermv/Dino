using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Director;

public class AnimationFactory  {

	private static AnimationFactory instance;

	private AnimationFactory(){

	}
	public static AnimationFactory getInstance(){
		if (instance == null){
			instance = new AnimationFactory();
		}
		return instance;
	}

	public AnimationClipPlayable loadAnimation(string name, string key){

		AnimationClip clip = Resources.Load<AnimationClip> ("Animations/" + name + "/" + key );
		return AnimationClipPlayable.Create (clip);

	}

}
