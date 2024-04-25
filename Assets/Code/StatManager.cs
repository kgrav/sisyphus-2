using UnityEngine;
using System;
using System.Collections.Generic;

public enum SisyStats {STRENGTH, AGILITY, STAMINA, LUCK, CON,EXP}
public enum CSTATS {SSSIZE,PSTR,MSTAM,BSTAM,SCRIT,DRAG,VKILL,CURVE,STRIDE}
public class StatManager : NVComponent {

    
    public static StatLevelBean _slb = null;
    public static StatLevelBean slb {get{if(_slb==null) _slb=new StatLevelBean(); return _slb;} set{_slb=value;}}
    public void LockInBean(){
        foreach(SisyStats s in slb.statLevels.Keys){
            statDict[s].level += slb.statLevels[s];
        }
        freeSkillPoints -= slb.totalPoints;
        SaveStats();
        slb=new StatLevelBean();
    }
    static StatManager _statManager;
    public static StatManager statManager{get{if(!_statManager) _statManager = FindObjectOfType<StatManager>(); return _statManager;}}
    public Stat[] statsSerial;
    public int totalLevel;
    public int freeSkillPoints;
    public int expPerLevel;
    public int curExp;
    public int expScale;
    public float expPerLevelScale;
    public string levelupsound;

public CompStatBean GetCStatBean(CSTATS stat){
        switch(stat){
            case CSTATS.SSSIZE:
                return new CompStatBean("Crit Window Size",sweetSpotSize(false),sweetSpotSize(true),"",1);
            break;
            case CSTATS.PSTR:
                return new CompStatBean("Push Strength", pushStrength(false),pushStrength(true),"",1);
            break;
            case CSTATS.MSTAM:
                return new CompStatBean("Max Stamina", maxStamina(false),maxStamina(true),"",1);
            break;
            case CSTATS.BSTAM:
                return new CompStatBean("Block Stamina Mult.", BlockStaminaMultiplier(false), BlockStaminaMultiplier(true),"%",100);
            break;
            case CSTATS.SCRIT:
                return new CompStatBean("Super Critical Chance", superCriticalChance(false),superCriticalChance(true),"%",50f);
            break;
            case CSTATS.DRAG:
                return new CompStatBean("Step Weight", conDrag(false),conDrag(true),"",1);
            break;
            case CSTATS.VKILL:
                return new CompStatBean("Velocity Absorbtion", velocityKillOnHit(false),velocityKillOnHit(true),"%",100);
            break;
            case CSTATS.CURVE:
                return new CompStatBean("Curve Influence", curveInfluence(false),curveInfluence(true),"",1);
            break;
            case CSTATS.STRIDE:
                return new CompStatBean("Step Force", strideLength(false),strideLength(true),"",1);
            break;
        }
        return null;
    }
    //computed stats:
    public float sweetSpotSize(bool uslb){
        int luck=statDict[SisyStats.AGILITY].level + (uslb ? slb.statLevels[SisyStats.AGILITY] : 0);
        float ss = Mathf.Min(luck,25)*0.05f;
        luck -= 25;
        if(luck > 0)
            ss += luck*0.02f;
        return ss;
    }

    public float pushStrength(bool uslb){
        int str = statDict[SisyStats.STRENGTH].level + (uslb ? slb.statLevels[SisyStats.STRENGTH] : 0);
        float ps = Mathf.Min(str, 10)*5;
        str -= 10;
        if(str > 0){
            ps+=Mathf.Min(str,15)*2.5f;
            str-= 15;
            if(str > 0){
                ps += str*1;
            }
        }
        return ps;
    }

    public float maxStamina(bool uslb){
        int stam = statDict[SisyStats.STAMINA].level + (uslb ? slb.statLevels[SisyStats.STAMINA] : 0);
        float ms = Mathf.Min(stam, 15)*5;
        stam-=15;
        if(stam > 0){
            ms += Mathf.Min(stam, 10)*4;
            stam -= 10;
            if(stam > 0){
                ms += stam*(2 + stam%2);
            }
        }
        return ms;
    }

    public float BlockStaminaMultiplier(bool uslb){
        return 1f-(statDict[SisyStats.CON].level + (uslb ? slb.statLevels[SisyStats.CON] : 0))*0.002f;
    }

    public float superCriticalChance(bool uslb){
        return (float)((statDict[SisyStats.LUCK].level+ (uslb ? slb.statLevels[SisyStats.LUCK] : 0))/200f);
    }

    public float conDrag(bool uslb){
        return (float)((statDict[SisyStats.CON].level + (uslb ? slb.statLevels[SisyStats.CON] : 0))*0.25f + 2f);
    }

    public float velocityKillOnHit(bool uslb){
        int con = statDict[SisyStats.CON].level + (uslb ? slb.statLevels[SisyStats.CON] : 0);
        float vk = Mathf.Min(con,5)*0.01f;
        con -= 5;
        if(con > 0){
            vk += Mathf.Min(con, 10)*0.003f;
            con-=10;
            if(con>0){
                vk+=con*0.001f;
            }
        }
        return vk;
    }

    public float curveInfluence(bool uslb){
        int con = statDict[SisyStats.CON].level;
        return con*0.02f+0.5f;
    }

    public float strideLength(bool uslb){
        return statDict[SisyStats.AGILITY].level*0.25f;
    }

    //end comp stats

    public List<CompStatBean> GetCompStats(bool useSlb){
        return new List<CompStatBean>();
    }
    Dictionary<SisyStats, Stat> _statDict = null;
    public Dictionary<SisyStats, Stat> statDict {
        get{
            if(_statDict==null){
                _statDict = new Dictionary<SisyStats, Stat>();
                foreach(Stat s in statsSerial){
                    _statDict.Add(s.stat, s);
                }
            }
            return _statDict;
        }
    }

    public void ConsumeSaveData(SaveData s){
        statDict[SisyStats.STRENGTH].level=s.strlvl;
        statDict[SisyStats.AGILITY].level=s.aglvl;
        statDict[SisyStats.STAMINA].level=s.stamlvl;
        statDict[SisyStats.CON].level=s.conlvl;
        statDict[SisyStats.LUCK].level=s.lucklvl;
        expPerLevel=s.expPerLevel;
        curExp=s.exp;
        freeSkillPoints=s.skillPoints;
    }
    public void SaveStats(){
        DataManager.dataManager.saveData.strlvl=statDict[SisyStats.STRENGTH].level;
        DataManager.dataManager.saveData.aglvl=statDict[SisyStats.AGILITY].level;
        DataManager.dataManager.saveData.strlvl=statDict[SisyStats.STRENGTH].level;
        DataManager.dataManager.saveData.strlvl=statDict[SisyStats.STRENGTH].level;
        DataManager.dataManager.saveData.strlvl=statDict[SisyStats.STRENGTH].level;
        
    }


    public void AddExperience(int amount){
        curExp+=amount;
        if(curExp>expPerLevel){
            curExp-=expPerLevel;
            freeSkillPoints++;
            expPerLevel=(int)(expPerLevel*expPerLevelScale);
            Sound(levelupsound);
        DataManager.dataManager.saveData.exp=curExp;
        DataManager.dataManager.saveData.expPerLevel=expPerLevel;
        DataManager.dataManager.saveData.skillPoints=freeSkillPoints;
            DataManager.dataManager.SaveGame();
        }
        DataManager.dataManager.saveData.exp=curExp;
        DataManager.dataManager.saveData.skillPoints=freeSkillPoints;

    }

    [Serializable]
    public class Stat {
        public SisyStats stat;
        public float initValue;
        public int level;
        public float increment;
        public float displayScale;

        public float value {
            get{return initValue + (level*increment);}
        }

        public float GetTotal(){
            return value + (level*increment);
        }
    }
}