using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	private PlayerComponent playerAvatar;

	public PlayerComponent PlayerAvatar {
		get {
			return playerAvatar;
		}
		set {
			playerAvatar = value;
		}
	}
	
	void Awake(){
		
		// No futuro, criar o personagem dinamicamente e guardar referência do seu model
		//playerAvatar = GameObject.FindWithTag("PLAYER").GetComponent<PlayerComponent>();
		
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
