using UnityEngine;
using System;
using System.Collections.Generic;

public class StatLevelAction : RowSelectAction{
    public SisyStats stat;
    public Color highlightColor;
    public Color defaultColor;
    public NVUIComponent label, value, addit;

    public override bool OnDPL()
    {
        if(StatManager.slb.statLevels[stat] > 0){
            StatManager.slb.statLevels[stat]--;
            return true;
        }
        return false;
    }

    public override bool OnDPR()
    {
        if(StatManager.slb.totalPoints < StatManager.statManager.freeSkillPoints){
            StatManager.slb.statLevels[stat]++;
            return true;
        }
        return false;
    }

    void Update(){
        if(StatManager.slb.statLevels[stat] > 0){
            //label.text.color=highlightColor;
            //value.text.color=highlightColor;
            addit.SetActive(true);
            addit.text.text = "+" + StatManager.slb.statLevels[stat];
        }
        else{
            //label.text.color=defaultColor;
            //value.text.color=defaultColor;
            addit.SetActive(false);
        }
        value.text.text = ""+(StatManager.statManager.statDict[stat].level+StatManager.slb.statLevels[stat]);
    } 
}