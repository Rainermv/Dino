using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class InterfaceController : MonoBehaviour {

    private PlayerComponent playerAvatar;

    private World world;

    public GameObject UIJump;
    public GameObject UIRun;

    public Text distanceScore;

    public GameObject GameOverScreen;

    public Text GameOverScore;

    public PlayerComponent PlayerAvatar {
		get {
			return playerAvatar;
		}
		set {
			playerAvatar = value;
		}
	}
	
	void Awake(){

        world = World.getInstance();
		
	}

	// Use this for initialization
	void Start () {

        playerAvatar.jumpChange = delegate (int jumps) {

            print(jumps);

            UIJump.BroadcastMessage("setJumps", jumps);
        };

        playerAvatar.playerDeath = delegate () {

            UIJump.SetActive(false);
            UIRun.SetActive(false);

            GameOverScreen.SetActive(true);

            GameOverScore.text = world.TRAVEL_DISTANCE.ToString("N0") + " m travelled";
        };

        //playerAvatar.jumpChange

    }
	
	// Update is called once per frame
	void Update () {
		
		HandleTouches();

        distanceScore.text = world.TRAVEL_DISTANCE.ToString("N0") + " m";

    }
	
	void HandleTouches(){
		
		if (Input.GetMouseButtonDown(0)){
			
			Vector3 position = Input.mousePosition;
			playerAvatar.PlayerJump();

            
		}

	}
}
