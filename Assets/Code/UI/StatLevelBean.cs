using UnityEngine;
using System;
using System.Collections.Generic;

public class StatLevelBean {
    public Dictionary<SisyStats, int> statLevels;


    public int totalPoints {get{int r = 0; foreach(int i in statLevels.Values){r+=i;} return r;}}


    public StatLevelBean(){
        statLevels=new Dictionary<SisyStats, int>();
        statLevels.Add(SisyStats.AGILITY,0);
    statLevels.Add(SisyStats.CON,0);
    statLevels.Add(SisyStats.LUCK,0);
    statLevels.Add(SisyStats.STAMINA,0);
    statLevels.Add(SisyStats.STRENGTH,0);
    }
}