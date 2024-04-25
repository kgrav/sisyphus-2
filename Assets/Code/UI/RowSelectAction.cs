using UnityEngine;
using System;

public class RowSelectAction : NVUIComponent {
    public bool disabled;
    public virtual bool OnConfirm(){
        return false;
    }

    public virtual bool OnDPL(){
        return false;
    }

    public virtual bool OnDPR(){
        return false;
    }
}