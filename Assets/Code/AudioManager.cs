using UnityEngine;
using System;
using System.Collections.Generic;

public enum Songs {NONE,PUSHUP,SYSY,SLOW1,SLOW2,SLOW3,SYSYREV}
public class AudioManager : NVComponent {
    public static AudioManager _audioManager;
    public static AudioManager audioManager {get{if(!_audioManager)_audioManager=FindObjectOfType<AudioManager>(); return _audioManager;}}
    public VolumeSliderAction vsaSfx;
    public BGMSliderAction vsaMusic;
    public static AudioSource BGM {get{return audioManager.localBGM;}}
    public AudioSource localBGM;
    public static AudioClip GetClip(string clip){
        return audioManager.clipDict[clip].curFrame;
    }

    public static void SetBGM(Songs newBGM){
        if(AudioManager.audioManager.curBGM!=newBGM){
            if(newBGM==Songs.NONE){
                BGM.Stop();
            }
            else{
            AudioManager.audioManager.curBGM=newBGM;
            BGM.clip = audioManager.songDict[newBGM];
            BGM.Play();
            }
        }
    }
    
    public AudioPage[] clips;
    public SongPage[] songs;
    
    Dictionary<string, SquiggleSound> _clipDict=null;
    public Dictionary<string,SquiggleSound> clipDict {
        get{
            if(_clipDict==null){
                _clipDict=new Dictionary<string, SquiggleSound>();
                foreach(AudioPage a in clips){
                    _clipDict.Add(a.label, a.clip);
                }
            }
            return _clipDict;
        }
    }
    Songs curBGM=Songs.NONE;
    Dictionary<Songs, AudioClip> _songDict=null;
    Dictionary<Songs,AudioClip> songDict {
        get{
            if(_songDict==null){
                _songDict=new Dictionary<Songs, AudioClip>();
                foreach(SongPage a in songs){
                    _songDict.Add(a.label, a.song);
                }
            }
            return _songDict;
        }
    }




    [Serializable]
    public class AudioPage{
        public SquiggleSound clip;
        public string label;
    }
    [Serializable]
    public class SongPage{
        public AudioClip song;
        public Songs label;
    }
}