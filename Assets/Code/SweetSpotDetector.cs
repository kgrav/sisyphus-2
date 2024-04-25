using UnityEngine;

public class SweetSpotDetector : NVComponent{
    public bool sweetSpot;
public float sweetSpotRadius;
    public void OnTriggerEnter2D(Collider2D c){
        sweetSpot=true;
    }

    public void OnTriggerExit2D(Collider2D c){
        sweetSpot=false;
    }
}