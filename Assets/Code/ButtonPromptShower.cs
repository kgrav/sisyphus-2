using UnityEngine;
using System;

public class ButtonPromptShower : NVComponent {


    public SpriteRenderer move,push,block;


    void Start(){
        DeactivateAll();
    }
    void Update(){
        if(GameController.gameController.gameState==GameState.INGAME && DataManager.dataManager.saveData.hill != 0){
            if(SisyphusController.sisy.rockDetector.rock){
                if(SisyphusController.sisy.rockDetector.rock.rb.velocity.x < -0.5f && !DataManager.dataManager.saveData.seenBlockPrompt){
                    block.enabled=true;
                    push.enabled=false;
                    move.enabled=false;
                }
                else if(!DataManager.dataManager.saveData.seenPushPrompt){
                    block.enabled=false;
                    push.enabled=true;
                    move.enabled=false;
                }
                else{
                    DeactivateAll();
                }
            }
            else if(!DataManager.dataManager.saveData.seenMovePrompt){
                block.enabled=false;
                push.enabled=false;
                move.enabled=true;
            }
            else{
                DeactivateAll();
            }
        }
        else{
            DeactivateAll();
        }
    }
    void DeactivateAll(){

        move.enabled=false;
        push.enabled=false;
        block.enabled=false;
    }
}