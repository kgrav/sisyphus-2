using UnityEngine;
using System;

public class RockDetector : NVComponent {
    public Rock rock = null;
    public AudioClip critSound;
    SweetSpotDetector _ssd;

    public SweetSpotDetector ssd {
        get{if(!_ssd) _ssd = GetComponentInChildren<SweetSpotDetector>(); return _ssd;}
    }
    public void OnTriggerEnter2D(Collider2D c){
        Rock r = c.GetComponent<Rock>();
        if(r){
            rock = r;
            if(critWindow){
                AudioSource.PlayClipAtPoint(critSound, tform.position);
            }
        }
    }
    
    

    public bool critWindow {get{return rock && 
    (Mathf.Abs(rock.tform.position.x-ssd.tform.position.x) < StatManager.statManager.sweetSpotSize(false));}}

    public void OnTriggerExit2D(Collider2D c){
        Rock r = c.GetComponent<Rock>();
        if(r){
            rock=null;
        }
    }
}