using UnityEngine;
using System;

public class OptionsMenuAction : RowSelectAction {
    public override bool OnConfirm()
    {
        GameController.gameController.SetGameState(GameState.OPTIONS);
        return true;
    }
}