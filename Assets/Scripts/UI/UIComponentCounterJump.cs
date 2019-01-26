using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComponentCounterJump : UIComponent
{

    public Color initialColor;
    public Color shadedColor;

    private List<RawImage> children = new List<RawImage>();


    // Start is called before the first frame update
    void Start()
    {
        foreach (RawImage child in transform.GetComponentsInChildren<RawImage>()) {

            children.Add(child);
        }

        
    }

    public void SetValue(int value, UIState uiState) {

        if (this.ActiveUIState != uiState) return;

        for (int i = 0; i < value; i++) {

            children[i].color = initialColor;
        }

        for (int i = value; i < children.Count; i++) {

            children[i].color = shadedColor;
        }

    }

}
