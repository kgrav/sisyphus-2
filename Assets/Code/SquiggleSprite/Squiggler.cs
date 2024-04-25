using UnityEngine;
using System;

public class Squiggler : NVComponent{
    SpriteRenderer _sprender;
    public SpriteRenderer sprender {get{if(!_sprender) _sprender=GetComponent<SpriteRenderer>(); return _sprender;}}
    public SquiggleSprite squig;

    void Update(){
        sprender.sprite=squig.curFrame;
    }
}