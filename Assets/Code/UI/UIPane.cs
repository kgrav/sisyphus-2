using UnityEngine;
using System;
public enum PaneType {INGAME, TITLE, LEVELUP, PRETITLE, OPTIONS}
public enum INGAMEFADES {}
public enum TITLEFADES {}
public enum PRETITLEFADES {}

public class UIPane : NVUIComponent {
    public static UIPane activePane = null;
    public FocusableUIComponent defaultFocus;

    public MessageFadeIn mFI;
    


    public void OnActivate(){
        NVUIComponent[] nvuis = GetComponentsInChildren<NVUIComponent>();
        activePane=this;
        foreach(NVUIComponent n in nvuis){
            n.SetActive(true);
        }
        FocusableUIComponent.SetFocus(defaultFocus);
    }

    public void OnDeactivate(){
        NVUIComponent[] nvuis = GetComponentsInChildren<NVUIComponent>();
        foreach(NVUIComponent n in nvuis){
            n.SetActive(false);
        }
    }
    

}