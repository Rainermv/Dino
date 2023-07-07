using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.Model;

public class UIComponentFinishLevelScreen : UIComponent {

    public float StarUpdateTime = 3;

    public Transform targetPosition;
    public Transform targetPositionOffset;

    public float velocity = 0.1f;

    private void Start() {

        StartCoroutine(finishLevelRoutine());

    }

    void finishLevel(World world) {

        
        //StartCoroutine(finishLevelRoutine(world));

    }

    IEnumerator finishLevelRoutine() {

        float t = 0;

        Transform screen = transform.GetChild(0);

        while (Vector2.Distance(screen.position, targetPositionOffset.position) > 0.5f) {

            t += velocity * Time.deltaTime;
            moveTo(t, screen, targetPositionOffset);
            yield return null;
        }

        t = 0f;

        while (Vector2.Distance(screen.position, targetPosition.position) > 0.5f) {

            t += velocity * Time.deltaTime;
            moveTo(t, screen, targetPosition);
            yield return null;
        }

        GetComponentInChildren<UIComponentCounterStar>().SetValue(World.GetInstance().StarsPicked, StarUpdateTime, ActiveUIState);

        yield return null;

    }

    void moveTo(float t, Transform screen, Transform target) {

        float y = Mathf.Lerp(screen.position.y, target.position.y, t);
        float x = Mathf.Lerp(screen.position.x, target.position.x, t);
        screen.transform.position = new Vector2(x, y);
    }
}
