using UnityEngine;
using System;

public class RockBlocker : NVComponent {
    public bool contact = false;
    bool _active=false;
    public Collider2D solidCollider;

    public bool active {
        get{
            return _active;
        }
        set{
            solidCollider.enabled=value;
            _active=value;
        }
    }
    public void OnTriggerEnter2D(Collider2D collider){
        if(active){
            Rock r = collider.GetComponent<Rock>();
            if(r){
                print("contact " + r.rb.velocity.magnitude);
                contact=true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider){
        if(active){
            Rock r = collider.GetComponent<Rock>();
            if(r){
                contact=false;
            }
        }
    }
}