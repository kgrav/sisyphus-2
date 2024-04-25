using UnityEngine;
using UnityEngine.UI;
using System;

public class BGMSliderAction : RowSelectAction {
        public Slider target;
    public AudioSource bgm;
    public string param;

    public int steps;
    float size;
    public void SetInitVol(float initVol){
        print("set init vol");
        target.value=initVol*0.1f;
        bgm.volume = initVol*0.1f;
        size = 1f;
    }

    public override bool OnDPL(){
        if(target.value > 0){
        target.value -= 0.1f;
        bgm.volume=target.value;
        return true;
        }
        return false;
    }

    public override bool OnDPR(){
        if(target.value < 1f){
            target.value += 0.1f;
            bgm.volume=target.value;
        return true;
        }
        return false;
    }
}