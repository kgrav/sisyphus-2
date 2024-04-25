using UnityEngine;
using System;
public class OptionsMenuResetSaveOption : RowSelectAction {
    public override bool OnConfirm()
    {
        DataManager.dataManager.DeleteSaveFile();
        GameController.gameController.SetGameState(GameState.PRETITLE);
        return true;
    }
}