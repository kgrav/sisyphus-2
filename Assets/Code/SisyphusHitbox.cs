using UnityEngine;
using System;

public class SisyphusHitbox : NVComponent {
    public RockDetector r2;
    public void OnTriggerEnter2D(Collider2D c){
        Rock r = c.GetComponent<Rock>();
        if(r){
            SisyphusController.sisy.Kill(r);
        }
    }
}