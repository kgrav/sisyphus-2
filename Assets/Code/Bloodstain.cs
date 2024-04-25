using UnityEngine;
using System;

public class Bloodstain : InteractiveObject {

    static Bloodstain _bloodstain;
    public static Bloodstain bloodstain {get{if(!_bloodstain) _bloodstain = FindObjectOfType<Bloodstain>(); return _bloodstain;}}

    public int exp;
    public bool active = false;
    public void Spawn(Vector2 pos, Quaternion rot, int exp){
        tform.position = pos;
        tform.rotation=rot;
        this.exp=exp;
        active=true;
    }

    public void Despawn(){
        tform.position = new Vector2(-1000f,-1000f);
        DataManager.dataManager.saveData.bloodstain=false;
        DataManager.dataManager.saveData.bloodstainExperience=0;
        DataManager.dataManager.saveData.bloodstainLocation=Vector2.zero;
        active=false;
    }


    public override void OnInteract()
    {
        if(active && UIPane.activePane){
            UIPane.activePane.mFI.ShowMessage((int)InGameFadeMessages.GETSTAIN);
        StatManager.statManager.AddExperience(exp);
        Despawn();
        }
    }


}