using UnityEngine;
using System;

public enum InGameFadeMessages{
    NONE=-1,DEATH=0, SUCCESS=1, GETSTAIN=2,CHECKPOINT=3
}
public enum TitleFadeMessages{
    NONE=-1,CONREQ=0,CREDS=1
}
public enum FadeState{
    HIDDEN, FADEIN, HOLD, FADEOUT
}
public class MessageFadeIn : NVUIComponent {
    public AlphaModulator bg;
    
    int cMessage = -1;
    public AlphaModulator[] messages;
    FadeState state=FadeState.HIDDEN;
    bool fading=false;
    public float fadeinTime,holdTime,fadeoutTime;
    float tptr = 0;


    public void ForwardMessage(){
        if(state==FadeState.HIDDEN){
        messages[cMessage].SetAlphaFade(0, fadeoutTime*0.75f);
        tptr=0;
        state=FadeState.FADEOUT;
        }
    }
    public void ShowMessage(int msg){
        if (cMessage != -1)
         {   messages[cMessage].SetAlphaFade(0,0);
            
         }
        bg.SetAlpha(1);
        cMessage=msg;
        
            messages[cMessage].SetAlphaFade(1,fadeinTime);
        state=FadeState.FADEIN;
        tptr=0;
    }
    void Update(){
        float deltaTime = Time.deltaTime;
        if(state==FadeState.HIDDEN){
            bg.SetAlpha(0);
            foreach(AlphaModulator m in messages){
                if(m){
                m.SetAlpha(0);
                }
            }
        }
        else if (cMessage != -1){
            tptr+=deltaTime;
            switch(state){
                case FadeState.FADEIN:
                    float a = Mathf.Lerp(0,1,tptr/fadeinTime);
                    if(tptr > fadeinTime){
                        tptr-=fadeinTime;
                        state=FadeState.HOLD;
                        GameController.gameController.OnHoldMessage(cMessage);
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