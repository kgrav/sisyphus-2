using UnityEngine;
using System;

public class QuitGameAction : RowSelectAction {
    public override bool OnConfirm()
    {
        GameController.gameController.QuitGame();
        return true;
    }
}