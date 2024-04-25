using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Squiggle Sound")]
public class SquiggleSound : ScriptableObject {
    public AudioClip[] squigs;

    public AudioClip curFrame {get{return squigs[GameController.gameController.squiggleCount%squigs.Length];}}
}