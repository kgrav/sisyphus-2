using UnityEngine;
using System;

public class MessageForwarder : FocusableUIComponent {
    public override void AButton()
    {
        if(UIPane.activePane && UIPane.activePane.mFI){
            UIPane.activePane.mFI.ForwardMessage();
        }
    }
}