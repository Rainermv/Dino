using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIComponentCounterDistance : UIComponent {

    Slider slider;

    // Use this for initialization
    void Awake() {

        slider = GetComponent<Slider>();

    }

    public void SetValue(float r, float time, UIState uiState) {

        if (this.ActiveUIState != uiState) return;

        slider.value = r;

        /*
        if (time == 0) {
            slider.value = r;
        } else {
            //StartCoroutine(SetSliderValueRoutine(r, time));
        }
        */


            
    }

    IEnumerator SetSliderValueRoutine(float targetValue, float time) {

        while (slider.value < targetValue) {

            slider.value = targetValue / time;

            yield return new WaitForSeconds(time / targetValue);


        }


    }
}
