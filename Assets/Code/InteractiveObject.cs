using UnityEngine;
using System;

public class InteractiveObject : NVComponent {
    
    public SpriteRenderer prompt;
    public virtual void OnDetect(){
        prompt.enabled=true;
    }
    public virtual void OnLose(){
        prompt.enabled=false;
    }
    public virtual void OnInteract(){

    }
}