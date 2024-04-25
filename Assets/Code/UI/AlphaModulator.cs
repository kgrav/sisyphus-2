using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AlphaModulator : NVUIComponent {
    
    public float startAlpha=0;
    void Start(){
        SetAlphaFade(startAlpha,0);
    }
    public void SetAlphaFade(float alpha, float time){
        if(text)
        {
            text.CrossFadeAlpha(alpha, time, false);
        }
        if(image){

            image.CrossFadeAlpha(alpha,time,false);
        }
    }

    public void SetAlpha(float alpha){
        SetAlphaFade(alpha, 0);
    }

}