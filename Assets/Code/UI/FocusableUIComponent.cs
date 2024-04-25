using UnityEngine;
using System;

public class FocusableUIComponent : NVUIComponent {

    static FocusableUIComponent _focus = null;

    public static FocusableUIComponent focus {get{return _focus;} private set {_focus=value;}}

    public FocusableUIComponent subFocus;
    public bool focused;

    public static void SetFocus(FocusableUIComponent newFocus){
        if(focus)
        {
            focus.OnLoseFocus();
        }
        focus=newFocus;
        if(focus){
        focus.OnFocus();
        }
    }

    protected virtual void OnFocus(){

    }

    protected virtual void OnLoseFocus(){

    }

    public virtual void DPU(){}
    public virtual void DPL(){}
    public virtual void DPD(){}
    public virtual void DPR(){}
    public virtual void AButton(){}
    public virtual void BButton(){}
}