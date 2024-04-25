using UnityEngine;
using System;


public class RowSelector : FocusableUIComponent {
    //ARRANGE TOP-BOTTOM
    public RowSelectAction[] elements;
    int ptr = 0;
    public string confirm,move;
    public bool wrap;
    public bool returnToGameOnCancel;
    public float moveTime;
    bool lerping;
    float lerpTime;
    Vector2 lerpInit,lerpGoal;


    void Update(){
        if(elements[ptr].disabled){
            DPD();
        }
        if(lerping){
            lerpTime+=Time.deltaTime;
            tform.position=Vector2.Lerp(lerpInit, lerpGoal,Mathf.Min(lerpTime,moveTime)/moveTime);
            if(lerpTime >= moveTime){
                lerping=false;
            }
        }
    }
    protected override void OnFocus(){
        for(int i = 0; i < elements.Length; ++i){
            if(!elements[i].disabled){
                ptr = i;
                break;
            }
        }
        tform.position=elements[ptr].tform.position;
    }

    public override void AButton(){
        if(elements[ptr].OnConfirm())
            Sound(confirm);
    }
    public override void DPU(){
        int preptr=ptr;
        ptr--;
        if(ptr < 0){
            if(wrap){
                ptr=elements.Length-1;
            }
            else{
                ptr=0;
            }
        }
        if(preptr!=ptr){
        lerping=true;
        lerpTime=0;
        lerpInit=tform.position;
        lerpGoal=elements[ptr].tform.position;
        if(elements[ptr].disabled)
            DPU();
        else
            Sound(move);
        }
    }
    public override void DPD(){
        int preptr=ptr;
        ptr++;
        if(ptr == elements.Length){
            if(wrap){
                ptr=0;
            }
            else{
                ptr=elements.Length-1;
            }
        }
        if(preptr!=ptr){
        lerping=true;
        lerpTime=0;
        lerpInit=tform.position;
        lerpGoal=elements[ptr].tform.position;
        if(elements[ptr].disabled)
            DPD();
        else
            Sound(move);
        }
    }
}