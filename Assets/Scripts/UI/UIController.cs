using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using System;

public class UIController : MonoBehaviour {

    private UIState currentUIState;

    private UIComponentCounterJump[]     uiComponentCounterJumpArray;
    private UIComponentCounterStar[]     uiComponentCounterStarArray;
    private UIComponentCounterDistance[] uiComponentCounterDistanceArray;

    World world;

    public UIState CurrentUIState {
        get {
            return currentUIState;
        }
    }


    // Use this for initialization
    void Start () {

        ChangeUIState(UIState.Game);

        world = World.GetInstance();

        uiComponentCounterJumpArray     = GetComponentsInChildren<UIComponentCounterJump>();
        uiComponentCounterStarArray     = GetComponentsInChildren<UIComponentCounterStar>();
        uiComponentCounterDistanceArray = GetComponentsInChildren<UIComponentCounterDistance>();

        world.registerStarsPickedValueChange(UpdateStars);

        //world.registerOnLevelFinished(finishLevel);


        //audioListener = Camera.current.GetComponent<AudioListener>();

    }

    // Update is called once per frame
    void Update() {


        foreach (UIComponentCounterDistance uiComponentCounterDistance in uiComponentCounterDistanceArray) {
            uiComponentCounterDistance.SetValue(world.DistanceTargetRatio, 0, currentUIState);
        }

        if (world.LevelFinished) {
            ChangeUIState(UIState.FinishedLevel);
        }
        
        // UIDistanceTargetSlider.value = world.DistanceTargetRatio;

    }

    #region BUTTONS

    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel(bool nextLevel) {

        //print ("click");

        SceneManager.LoadScene(1);
        //Application.LoadLevel(Application.loadedLevel);
        World.Restart(nextLevel);
    }

    public void ToggleSound(bool value) {

        print(value);

        PlayerPrefs.SetFloat("volMaster", value ? 1 : 0);

    }
    /*
    public void finishLevel() {

        //FinishLevelScreen.SetActive(true);
        //FinishLevelScreen.SendMessage("finishLevel", world);

    }*/

    #endregion BUTTONS

    #region UI ELEMENTS

    public void UpdateJumps(int value) {

        foreach (UIComponentCounterJump uiComponentCounterJump in uiComponentCounterJumpArray) {
            uiComponentCounterJump.SetValue(value, currentUIState);
        }

    }

    void UpdateStars() {

        int value = world.STARS_PICKED;

        foreach (UIComponentCounterStar uiComponentCounterStar in uiComponentCounterStarArray) {
            uiComponentCounterStar.SetValue(value, 0, currentUIState);
        }

    }

    public void OnPlayerDeath() {

        ChangeUIState(UIState.Dead);

        /*
        foreach (GameObject UIElement in UIdisableOnEnd) {

            UIElement.SetActive(false);

            GameOverScreen.SetActive(true);

            //UIDistanceGameOver.text = world.TRAVEL_DISTANCE.ToString("N0") + " m travelled";
            UIStarsGameOver.text = world.STARS_PICKED.ToString();

        }
        */
    }

    private void ChangeUIState(UIState newState) {

        this.currentUIState = newState;

        foreach (UIComponent uiComponent in Resources.FindObjectsOfTypeAll<UIComponent>()) {

            uiComponent.SetActiveByState(newState);
        }
    }

    #endregion UI ELEMENTS



}
