using UnityEngine;
using System;

public class NewGameAction : RowSelectAction{
    public override bool OnConfirm()
    {
        GameController.gameController.NewGame();
        return true;
    }
}