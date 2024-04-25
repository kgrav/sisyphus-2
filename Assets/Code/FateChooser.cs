using UnityEngine;
using System;

public class FateChooser : InteractiveObject {
    public Collider2D collid1,collid2;
    public SpriteRenderer sprender;
    public string sound;
    public override void OnInteract()
    {
        Sound(sound);
        collid1.enabled=true;
        sprender.enabled=true;
        collid2.enabled=true;
    }
}