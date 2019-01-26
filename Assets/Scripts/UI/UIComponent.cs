using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIComponent : MonoBehaviour
{
    [SerializeField]
    private UIState activeUIState;

    
    public UIState ActiveUIState {
        get {
            return activeUIState;
        }

        set {
            activeUIState = value;
        }
    }

    public void SetActiveByState(UIState state) {

        gameObject.SetActive(this.activeUIState == state);

        
    }

}
