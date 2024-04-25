using UnityEngine;
using System;

public class SisyphusGroundIntersector : NVComponent{
    
    ParticleSystem _psys;
    public ParticleSystem psys {get{if(!_psys) _psys = GetComponent<ParticleSystem>(); return _psys;}}
    public Vector2 emitRange;
    public string crunchSound;
    public void Trigger(){
        Sound(crunchSound);
        psys.Emit(new ParticleSystem.EmitParams(),(int)UnityEngine.Random.Range(emitRange.x,emitRange.y));
    }
    public void OnTriggerEnter2D(Collider2D c){
        if(SisyphusController.sisy.state==SisyState.DEAD)
        Trigger();       
    }
}