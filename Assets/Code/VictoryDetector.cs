using UnityEngine;
using System;

public class VictoryDetector : NVComponent {
    public int nexthill;
    public void OnTriggerEnter2D(Collider2D rock){
        Rock r = rock.GetComponent<Rock>();
        if(r){
            r.rb.velocity=Vector2.zero;
            GameController.gameController.OnVictoryCatch(nexthill);
        }
    }
}