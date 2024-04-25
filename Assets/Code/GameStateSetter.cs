using UnityEngine;
using System;

public class GameStateSetter : MonoBehaviour {
    public GameState state;
    void Awake(){
        GameController.gameController.SetGameState(state);
        Destroy(gameObject);
    }
}