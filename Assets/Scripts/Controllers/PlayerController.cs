using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerController : MonoBehaviour {

    private PlayerComponent playerAvatar;

    private UIController uiController;

    private World world;

    /*
   public GameObject UIJumps;


   public GameObject[] UIdisableOnEnd;

   public Text UIStarsScore;
   public Text UIDistanceScore;

   public GameObject GameOverScreen;
   public GameObject FinishLevelScreen;

   public Text UIStarsGameOver;
   public Text UIDistanceGameOver;

   public Slider UIDistanceTargetSlider;
   */

    public PlayerComponent PlayerAvatar {
		get {
			return playerAvatar;
		}
		set {
			playerAvatar = value;
		}
	}
	
	void Awake(){

        world = World.GetInstance();

        uiController = FindObjectOfType<UIController>();


    }

	// Use this for initialization
	void Start () {

        playerAvatar.jumpChange += delegate (int jumps) { 
            uiController.UpdateJumps(jumps);
        };

        playerAvatar.playerDeath += delegate () {
            uiController.OnPlayerDeath();
        };

        playerAvatar.registerCharacterDiedCallback(() => {
            playerAvatar = null;
        });


    }
	
	// Update is called once per frame
	void Update () {
		
		HandleTouches();
    }
	
	void HandleTouches(){
		
		if (Input.GetMouseButtonDown(0) && playerAvatar != null && !world.LevelCompleted){
			
			Vector3 position = Input.mousePosition;
			playerAvatar.PlayerJump();
		}
	}



    
}
