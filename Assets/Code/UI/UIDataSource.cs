using UnityEngine;
using System;

public class UIDataSource : NVComponent {
    public virtual float GetFloat(){
        return 0f;
    }
    public virtual int GetInt(){
        return 0;
    }
}