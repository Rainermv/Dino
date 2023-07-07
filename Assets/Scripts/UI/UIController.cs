using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class UIController : MonoBehaviour {

        private UIState currentUIState;

        public UiComponentCounter JumperCounter;
        public UiComponentCounter SlamCounter;
        public UIComponentCounterStar StarCounter;
        public UIComponentCounterDistance DistanceCounter;

        private World _world;
        
        public UIState CurrentUIState => currentUIState;


        // Use this for initialization
        public void Initialize(World world)
        {

            _world = world;
            _world.OnWorldFinishedAction += () => ChangeUIState(UIState.FinishedLevel);


            ChangeUIState(UIState.Game);
       
            _world.RegisterStarsPickedValueChange(UpdateStars);

            //world.registerOnLevelFinished(finishLevel);

            //audioListener = Camera.current.GetComponent<AudioListener>();

            JumperCounter.Initialize();
            SlamCounter.Initialize();

        }

        // Update is called once per frame
        void Update() {


            DistanceCounter.SetValue(_world.DistanceTargetRatio, 0, currentUIState);

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

        public void UpdateJumps(int value, int maxValue) {

            JumperCounter.SetValue(value, maxValue, currentUIState);

        }

        void UpdateStars() {

            int value = _world.StarsPicked;

            StarCounter.SetValue(value, 0, currentUIState);
            

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

            foreach (UIComponent uiComponent in FindObjectsOfType<UIComponent>()) {

                uiComponent.SetActiveByState(newState);
            }
        }

        #endregion UI ELEMENTS



    }
}
