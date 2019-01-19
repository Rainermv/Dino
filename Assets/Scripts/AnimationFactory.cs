using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.Animations;

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

	public AnimationClipPlayable loadAnimation(PlayableGraph playableGraph, string name, string key){

		AnimationClip clip = Resources.Load<AnimationClip> ("Animations/" + name + "/" + key );
		return AnimationClipPlayable.Create (playableGraph, clip);

	}

    public AnimationClip getAnimationClip(string name, string key)
    {
        return Resources.Load<AnimationClip>("Animations/" + name + "/" + key);
    }

}
