using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComponentCounterStar : UIComponent
{

    private Text starCounter;


    // Start is called before the first frame update
    void Start()
    {
        starCounter = GetComponentInChildren<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SetValue(int value, float finishTime, UIState uiState) {

        if (this.ActiveUIState != uiState) return;

        if (finishTime == 0) {
            starCounter.text = value.ToString();
        } else {

            StartCoroutine(SetStarValueRoutine(value, finishTime));

        }
        
    }

    IEnumerator SetStarValueRoutine(int targetValue, float finishTime) {

        int starsCount = 0;

        while (starsCount < targetValue) {

            starsCount++;

            starCounter.text = starsCount.ToString();

            yield return new WaitForSeconds(finishTime / targetValue);

        }
    }
}
