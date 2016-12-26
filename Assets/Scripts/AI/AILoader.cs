using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AILoader {

	#region SINGLETON

	private static AILoader instance;
	private AILoader(){
	}
	public static AILoader getInstance(){	
		if (instance == null){
			instance = new AILoader();
			instance.Setup ();
		}	
		return instance;
	}

	#endregion

	private Dictionary<string, AI> AIDictionary = new Dictionary<string, AI> ();

	private void Setup(){

		AIDictionary.Add ("Zombie", new ZombieAI ()); 

	}

	public AI GetAI(string key){

		if (!AIDictionary.ContainsKey(key)){

			Debug.LogError(key + " not found in AILoader");
			return null;
		}

		return AIDictionary[ key ];

	}


}
