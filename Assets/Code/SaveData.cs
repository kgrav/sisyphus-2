using UnityEngine;
using System;

[System.Serializable]
public class SaveData {
    public int hill;
    public int strlvl;
    public int aglvl;
    public int stamlvl;
    public int lucklvl;
    public int conlvl;
    public int exp;
    public int expPerLevel;
    public int skillPoints;
    public bool bloodstain;
    public Vector2 bloodstainLocation;
    public int bloodstainhill;
    public Quaternion bloodstainRotation;
    public int bloodstainExperience;
    public int sfxLevel;
    public int musicLevel;
    public bool seenMovePrompt;
    public bool seenPushPrompt;
    public bool seenBlockPrompt;
}