using UnityEngine;
using System;
using TMPro;
public class ExpDisplay : NVUIComponent {

    void Update(){
        int sp = StatManager.statManager.freeSkillPoints;
        text.text = (sp < 10 ? "0" : "") + sp;

    }
}