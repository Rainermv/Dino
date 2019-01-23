using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class InterfaceController : MonoBehaviour {

    private PlayerComponent playerAvatar;

    private World world;

    public GameObject UIJumps;
    public GameObject UIDistance;
    public GameObject UIStars;

    public Text UIStarsScore;
    public Text UIDistanceScore;

    public GameObject GameOverScreen;

    public Text UIStarsGameOver;
    public Text UIDistanceGameOver;

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

            UIJumps.BroadcastMessage("setJumps", jumps);
        };

        playerAvatar.playerDeath = delegate () {

            UIJumps.SetActive(false);
            UIStars.SetActive(false);
            UIDistance.SetActive(false);

            GameOverScreen.SetActive(true);

            //UIDistanceGameOver.text = world.TRAVEL_DISTANCE.ToString("N0") + " m travelled";
            UIStarsGameOver.text = world.STARS_PICKED.ToString();

            print(world.STARS_PICKED.ToString());
        };

        world.registerStarsPickedValueChange(() => {
            onStarsPickupValueChange();
        });

        playerAvatar.registerCharacterDiedCallback(() => {

            playerAvatar = null;

        });

        //playerAvatar.jumpChange

    }
	
	// Update is called once per frame
	void Update () {
		
		HandleTouches();

        UIDistanceScore.text = world.TRAVEL_DISTANCE.ToString("N0");

    }
	
	void HandleTouches(){
		
		if (Input.GetMouseButtonDown(0) && playerAvatar != null){
			
			Vector3 position = Input.mousePosition;
			playerAvatar.PlayerJump();

            
		}

	}

    void onStarsPickupValueChange() {

        UIStarsScore.text = world.STARS_PICKED.ToString("N0");

    }
}
