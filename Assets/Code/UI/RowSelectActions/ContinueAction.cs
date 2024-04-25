using UnityEngine;
using System;

public class ContinueAction : RowSelectAction {

    
    void Update(){
        if(GameController.gameController.gameState==GameState.TITLE){
            if(DataManager.dataManager.saveData.hill==0){
                disabled=true;
                SetActive(false);
            }
            else{
                disabled=false;
                SetActive(true);
            }
        }
    }

    public override bool OnConfirm()
    {
        GameController.gameController.ContinueGame();
        return true;
    }
}