using UnityEngine;
using System;

public class StatScreenBackAction : RowSelectAction {
    public override bool OnConfirm()
    {StatManager.slb=new StatLevelBean();
        GameController.gameController.SetGameState(GameState.INGAME);
        return true;
    }
}