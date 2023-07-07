using UnityEngine;
using System.Collections;
using Assets.Scripts.Model;

public class UIComponentGameOverScreen : UIComponent {

    public float DistanceUpdateTime = 1f;

    // Use this for initialization
    void Start() {

        //StartCoroutine(GameOverRoutine());
        float distanceRatio = World.GetInstance().DistanceTargetRatio;
        GetComponentInChildren<UIComponentCounterDistance>().SetValue(distanceRatio, DistanceUpdateTime, ActiveUIState);

    }


    /*
    IEnumerator GameOverRoutine() {

        

    }
    */
}
