using UnityEngine;
using System;

public class EXPDisplayDataSource : UIDataSource {

    public override float GetFloat()
    {   
        return ((float)StatManager.statManager.curExp/(float)StatManager.statManager.expPerLevel);
    }

    public override int GetInt()
    {
        return StatManager.statManager.freeSkillPoints;
    }
}