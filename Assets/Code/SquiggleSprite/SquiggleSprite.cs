using UnityEngine;
using System;
[CreateAssetMenu(menuName = "Squiggle Sprite")]
public class SquiggleSprite : ScriptableObject {
    public Sprite[] sprites;
    public Sprite curFrame {
        get{
            return sprites[GameController.gameController.squiggleCount%sprites.Length];
        }
    }
}