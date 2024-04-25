using UnityEngine;
using System;

public class ScreenFadeIn : NVUIComponent {
    static MessageFadeIn _mFI;
    public static MessageFadeIn mFI {get{if(!_mFI) _mFI = FindObjectOfType<MessageFadeIn>(); return _mFI;}}
    public AlphaModulator bg;
    InGameFadeMessages cMessage = InGameFadeMessages.NONE;
    public AlphaModulator[] messages;
    FadeState state=FadeState.HIDDEN;
    bool fading=false;
    public float fadeinTime,holdTime,fadeoutTime;
    float tptr = 0;


    public void ShowMessage(InGameFadeMessages msg){
        if (cMessage != InGameFadeMessages.NONE)
         {   messages[(int)cMessage].SetAlphaFade(0,0);
            
         }
        bg.SetAlpha(1);
        cMessage=msg;
        
            messages[(int)cMessage].SetAlphaFade(1,fadeinTime);
        state=FadeState.FADEIN;
        tptr=0;
    }
    void Update(){
        float deltaTime = Time.deltaTime;
        if(state==FadeState.HIDDEN){
            bg.SetAlpha(0);
            foreach(AlphaModulator m in messages){
                m.SetAlpha(0);
            }
        }
        else if (cMessage != InGameFadeMessages.NONE){
            tptr+=deltaTime;
            switch(state){
                case FadeState.FADEIN:
                    float a = Mathf.Lerp(0,1,tptr/fadeinTime);
                    if(tptr > fadeinTime){
                        tptr-=fadeinTime;
                        state=FadeState.HOLD;
                    }
                break;
                case FadeState.HOLD:
                    if(tptr > holdTime){
                    messages[(int)cMessage].SetAlphaFade(0, fadeoutTime);
                        tptr-=holdTime;
                        state=FadeState.FADEOUT;
                    }
                break;
                case FadeState.FADEOUT:
                    if(tptr > fadeoutTime){
                        state=FadeState.HIDDEN;
                        GameController.gameController.FinishMessage((int)cMessage);
                    }
                break;
            }
        }
    }
}