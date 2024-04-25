using UnityEngine;
using System;

public class Spawner : NVComponent {
    public GameObject prefab;
    GameObject spawned;
    public bool rockSpawner;

    public void Spawn(){
        Kill();
        spawned = Instantiate(prefab, tform.position, prefab.transform.rotation);
    }

    public void Kill(){
        if(spawned){
            Destroy(spawned);
        }
    }
}