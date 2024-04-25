using UnityEngine;
using System;

public class UISquiggler : NVUIComponent {
    public SquiggleSprite squiggler;

    void Update(){
        image.sprite=squiggler.curFrame;
    }
}