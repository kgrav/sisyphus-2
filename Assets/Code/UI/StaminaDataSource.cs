using UnityEngine;
using System;

public class StaminaDataSource : UIDataSource {
    public override float GetFloat()
    {
        if(!SisyphusController.sisy)
            return 0f;
        float maxStam = StatManager.statManager.maxStamina(false);
        float cStam = SisyphusController.sisy.stamina;
        print("cstam/maxstam:" + cStam/maxStam);
        return cStam/maxStam;
    }
}