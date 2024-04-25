using UnityEngine;
using System;

public class CompStatDisplay : NVUIComponent {
    
    public CSTATS stat;
    public NVUIComponent label;
    public NVUIComponent value;
    public NVUIComponent slbAdd;

    void Update(){
        CompStatBean csb = StatManager.statManager.GetCStatBean(stat);
        label.text.text=csb.label;
        value.text.text="- " + (csb.value*csb.scale) + csb.suffix;
        if(csb.value!=csb.slbValue){
            slbAdd.text.text = "-> " + csb.slbValue;
        }
        else{
            slbAdd.text.text="";
        }
    }

}