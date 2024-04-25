using UnityEngine;
using UnityEngine.UI;
using System;

public class EasedSlider : MonoBehaviour {
    public Slider topLayer,delayLayer;
    public float delayTime, lerpTime;
    float tptrt, tptrd, dptr;
    float opos,gpos;
    float lastPos;

    UIDataSource _dataSource;
    UIDataSource dataSource {
        get{if(!_dataSource) _dataSource = GetComponent<UIDataSource>(); return _dataSource;}
    }

    void Awake(){
        float stam = dataSource.GetFloat();
        opos= stam;
        gpos=stam;
        lastPos=stam;
    }

    void Update(){
        if(dataSource.GetFloat() < lastPos){
            opos = lastPos;
            lastPos = dataSource.GetFloat();
            gpos = lastPos;
            tptrt = 0;
            tptrd = 0;
            dptr = delayTime;

        }
        if(dataSource.GetFloat() > lastPos){
            
        float stam = dataSource.GetFloat();
        opos= stam;
        gpos=stam;
        lastPos=stam;
        topLayer.value=stam;
        delayLayer.value=stam;
        }
        if(tptrt < lerpTime){
            tptrt += Time.deltaTime;
            topLayer.value = UnityEngine.Mathf.Lerp(opos,gpos,tptrt/lerpTime);
        }
        if(dptr > 0){
            dptr -= Time.deltaTime;
        }
        else{
            tptrd += Time.deltaTime;
            delayLayer.value = UnityEngine.Mathf.Lerp(opos,gpos,tptrd/delayTime);
        }

    }
}