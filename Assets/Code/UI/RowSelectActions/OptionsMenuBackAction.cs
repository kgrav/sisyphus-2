using UnityEngine;
using System;

public class OptionsMenuBackAction : RowSelectAction {
    public override bool OnConfirm()
    {
        DataManager.dataManager.SaveGame();
        GameController.gameController.SetGameState(GameState.TITLE);
        return true;
    }
}