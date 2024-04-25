using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class VolumeSliderAction : RowSelectAction {

    public Slider target;
    public AudioMixer mixer;
    public string param;

    public int steps;
    float size;
    public void SetInitVol(int initVol){
        target.value=initVol;
        mixer.SetFloat(param,initVol*10);
        size = target.maxValue-target.minValue;
    }

    public override bool OnDPL(){
        if(target.value > target.minValue){
        target.value -= size/steps;
        mixer.SetFloat(param, target.value);
        return true;
        }
        return false;
    }

    public override bool OnDPR(){
        if(target.value < target.maxValue){
            target.value += size/steps;
            mixer.SetFloat(param,target.value*10);
        return true;
        }
        return false;
    }
}