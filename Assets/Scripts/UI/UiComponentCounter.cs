using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UiComponentCounter : UIComponent
    {
        private readonly List<RawImage> children = new List<RawImage>();
        private Color _baseColor;
        private Color _spentColor;


        // Start is called before the first frame update
        public void Initialize()
        {
            
            foreach (var child in transform.GetComponentsInChildren<RawImage>())
            {
                children.Add(child);
            }

            _baseColor = children.First().color;
            _spentColor = new Color(_baseColor.r, _baseColor.g, _baseColor.b, 0.3f);
            
        }

        public void SetValue(int value, int maxValue, UIState uiState) {

            if (this.ActiveUIState != uiState) return;

            for (var i = 0; i < value; i++) {

                children[i].enabled = true;
                children[i].color = _baseColor;
            }

            for (var i = value; i < maxValue; i++) {

                children[i].enabled = true;
                children[i].color = _spentColor;
            }

            for (var i = maxValue; i < children.Count; i++)
            {
                children[i].enabled = false;
            }
        }

     
    }
}
