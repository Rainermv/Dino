using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteComposition : Actor {

    //private string _currentSpriteKey;

    private Dictionary<CharacterAnimationType, string> spriteKeyStates = new Dictionary<CharacterAnimationType, string>();

    public SpriteComposition(string defaultSpriteKey, int depth) {

        this.addState(CharacterAnimationType.IDLE, defaultSpriteKey);
        //_currentSpriteKey = defaultSpriteKey;
        spriteKey = defaultSpriteKey;

        this.depth = depth;
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