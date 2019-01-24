using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLevelStarCounter : MonoBehaviour
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

        World world = World.getInstance();

        while (stars < world.STARS_PICKED) {

            stars++;

            starsText.text = stars.ToString();

            yield return new WaitForSeconds(COUNT_TIME / world.STARS_PICKED);

        }

        yield return false;
    }


}
