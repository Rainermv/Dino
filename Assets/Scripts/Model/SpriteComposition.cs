using System.Collections.Generic;
using Assets.Scripts.Model;
using UnityEngine;
using UnityEditor;

public class SpriteComposition : Actor {

    //private string _currentSpriteKey;

    private Dictionary<CharacterAnimationType, string> spriteKeyStates = new Dictionary<CharacterAnimationType, string>();

    public SpriteComposition(string defaultSpriteKey, int sortingOrder) {

        Debug.Log(defaultSpriteKey);

        this.addState(CharacterAnimationType.Idling, defaultSpriteKey);
        //_currentSpriteKey = defaultSpriteKey;
        spriteKey = defaultSpriteKey;

        this.sortingOrder = sortingOrder;
    }

    /*
    public string CurrentSpriteKey {
        get {
            return _currentSpriteKey;
        }
    }
    */

    public void addState(CharacterAnimationType type, string value) {

        spriteKeyStates.Add(type, value);

    }

    public void changeState(CharacterAnimationType type) {

        if (spriteKeyStates.ContainsKey(type)) {
            spriteKey =  spriteKeyStates[type];

            if (onChangeSpriteKey != null) {
                onChangeSpriteKey();
            }
            
        }

    }


}