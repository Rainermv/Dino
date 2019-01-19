using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void RestartGame() {

        //print ("click");

        SceneManager.LoadScene(1);
        //Application.LoadLevel(Application.loadedLevel);
        World.restart();
    }



}
