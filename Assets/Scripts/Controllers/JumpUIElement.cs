using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JumpUIElement : MonoBehaviour {

    public int Jumps;


    private bool state = true;
    private RawImage img;
    private Color startingColor;

    public void Awake() {
        img = this.GetComponent<RawImage>();
        startingColor = img.color;
    }

    public void setJumps(int value) {

  
        this.setState(value >= this.Jumps);

    }

    private void setState(bool state) {

      
        if (state) {
            img.color = startingColor;
        } else {
            img.color = new Color(1, 1, 1, 0.2f);
        }


    }
    
}
