using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	private ActorPlayer playerAvatar;
	
	
	
	void Awake(){
		
		playerAvatar = GameObject.FindWithTag("PLAYER").GetComponent<ActorPlayer>();
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		HandleTouches();
	
	}
	
	void HandleTouches(){
		
		if (Input.GetMouseButtonDown(0)){
			
			Vector3 position = Input.mousePosition;
			playerAvatar.PlayerJump();
		}
		
		
				
		
		
		
	}
}
