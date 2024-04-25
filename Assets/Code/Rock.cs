using UnityEngine;
using System;

public class Rock : NVComponent {

    Rigidbody2D _rb;
    int pushes = 0;
    Vector2 lastMaxX;
    public Rigidbody2D rb {
        get{if(!_rb) _rb = GetComponent<Rigidbody2D>(); return _rb;}
    }
    bool activeDeathTrigger = true;
    public bool attached = false;
    public SquiggleSprite squiggler;
    SpriteRenderer _spriteRenderer;
    public string rollSound;
    public SpriteRenderer spriteRenderer {get{if(!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>(); return _spriteRenderer;}}

    public override void Reset()
    {
        rb.velocity=Vector2.zero;
        pushes=0;
        lastMaxX = tform.position;
    }

    void FixedUpdate(){
        spriteRenderer.sprite=squiggler.curFrame;
        if(pushes > 0){
            if(tform.position.x > lastMaxX.x){
                float exp = Vector2.Distance(tform.position, lastMaxX);
                StatManager.statManager.AddExperience((int)MathF.Ceiling(exp*StatManager.statManager.expScale));
                lastMaxX = tform.position;
            }
        }
        if(attached && activeDeathTrigger && rb.velocity.magnitude < 0.01f){
            activeDeathTrigger=false;
            GameController.gameController.OnDeathCatch();
        }
    }
    public void OnPush(){
        Sound(rollSound);
        if(pushes==0){
            lastMaxX=tform.position;
        }
        pushes++;
    }
}