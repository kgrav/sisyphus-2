using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CeilFloatDisplay : NVUIComponent {
    void Update(){
        text.text = (int)Mathf.Ceil(dataSource.GetFloat()) + "";
    }
}