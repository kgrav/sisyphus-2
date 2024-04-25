using UnityEngine;
using System;

public class NVComponent : MonoBehaviour {

    Transform _tform;
    public Transform tform {get{if(!_tform) _tform = transform; return _tform;}}

    public AudioSource _aud;
    public AudioSource aud {get{if(!_aud) _aud=GetComponent<AudioSource>(); return _aud;}}
    protected void Sound(string sound){
        if(aud)
            aud.PlayOneShot(AudioManager.GetClip(sound));
    }
    public virtual void Reset(){}
}