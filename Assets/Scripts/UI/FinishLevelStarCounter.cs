using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;

public class FinishLevelStarCounter : UIComponent
{

    int stars = 0;

    float COUNT_TIME = 1;

    Text starsText;

    // Start is called before the first frame update
    void Start()
    {
        starsText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStarCount() {

        StartCoroutine(UpdateStarCountRoutine());

    }

    IEnumerator UpdateStarCountRoutine() {

        print("update star count routine");

        World world = World.GetInstance();

        while (stars < world.StarsPicked) {

            stars++;

            starsText.text = stars.ToString();

            yield return new WaitForSeconds(COUNT_TIME / world.StarsPicked);

        }

        yield return false;
    }


}
