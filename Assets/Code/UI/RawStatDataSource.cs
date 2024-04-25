using UnityEngine;
using System;

public class RawStatDataSource : UIDataSource {

    public SisyStats stat;
    public override float GetFloat()
    {
        StatManager.Stat st = StatManager.statManager.statDict[stat];
        return st.value*st.displayScale;
    }
}