using UnityEngine;
using System;


public class CompStatBean {
    public string label;
    public float value;
    public float slbValue;
    public string suffix;
    public float scale;
    public CompStatBean(string label, float value, float slbValue, string suffix, float scale){
        this.label=label;
        this.value=value;
        this.slbValue=slbValue;
        this.suffix=suffix;
        this.scale=scale;
    }
}