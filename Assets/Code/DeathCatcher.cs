using UnityEngine;
using System;

public class DeathCatcher : NVComponent {

    bool hasSisy=false, hasRock=false;
    bool active=true;

    void OnTriggerEnter2D(Collider2D collid){
        if(active){
        if(!hasRock && collid.GetComponent<Rock>()){
            hasRock=true;
        }
        if(!hasSisy && collid.GetComponent<SisyphusController>()){
            hasSisy=true;
        }
        }
    }

    public void Reset(){
        hasSisy=false;
        hasRock=false;
        active=true;
    }

    void Update(){
        if(active && hasSisy && hasRock){
            print("deathcatch");
            GameController.gameController.OnDeathCatch();
            active=false;
        }
    }
}