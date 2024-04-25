using UnityEngine;
using System;

public class ScrollCam : NVComponent {
    public Vector2 scrollThresh;
    public float lerpTime;
    Vector2 lastPosS;
    Vector2 lerpInit,lerpGoal;
    float ltime;
    public bool initIsIdeal;

    Vector2 initDist;
    
    void Start(){
    }

    void Update(){
        if(ltime < lerpTime){
            ltime+=Time.deltaTime;
            tform.position = Vector2.Lerp(lerpInit,lerpGoal,ltime/lerpTime);
        }
            //if(SisyphusController.sisy.tform.position )
    }
}