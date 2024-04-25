using UnityEngine;
using System;

public class SkillPointsDisplay : NVUIComponent{
    public NVUIComponent value, addit;
    void Update(){
        if(StatManager.slb.totalPoints > 0){
            addit.SetActive(true);
            addit.text.text = "-" + StatManager.slb.totalPoints;   
        }
        else{
            addit.SetActive(false);
        }
        value.text.text=""+(StatManager.statManager.freeSkillPoints - StatManager.slb.totalPoints);
    }
}