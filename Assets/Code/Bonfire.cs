using UnityEngine;
using System;

public class Bonfire : InteractiveObject {
    public override void OnInteract()
    {
        GameController.gameController.SetGameState(GameState.FIREUI);
    }
}