using UnityEngine;
using System;

public class InteractiveObjectDetector : NVComponent {
    
    InteractiveObject _current;
    public InteractiveObject current {get{return _current;} set{_current=value;}}
    
    public void OnTriggerEnter2D(Collider2D c){
        InteractiveObject o = c.GetComponent<InteractiveObject>();
        if(o){
            if(current){
                current.OnLose();
            }
            current=o;
            current.OnDetect();
        }
    }
    public void OnTriggerExit2D(Collider2D c){
        InteractiveObject o = c.GetComponent<InteractiveObject>();
        if(o && o.GetHashCode() == current.GetHashCode()){
            current.OnLose();
            current=null;
        }
    }
}