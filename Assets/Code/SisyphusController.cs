using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public enum SisyState { PAUSE, GAME, DEAD, SIT, WIN }
public class SisyphusController : NVComponent
{
    static SisyphusController _sisy;
    public static SisyphusController sisy
    {
        get { if (!_sisy) _sisy = FindObjectOfType<SisyphusController>(); return _sisy; }
    }
    RockDetector _rockDetector;
    public RockDetector rockDetector
    {
        get { if (!_rockDetector) _rockDetector = GetComponentInChildren<RockDetector>(); return _rockDetector; }
    }

    RockBlocker _rockBlocker;
    public RockBlocker rockBlocker
    {
        get { if (!_rockBlocker) _rockBlocker = GetComponentInChildren<RockBlocker>(); return _rockBlocker; }
    }


    public SisyphusGroundIntersector bloodTrig;

    InteractiveObjectDetector _iod;
    public InteractiveObjectDetector iod {get{if(!_iod) _iod=GetComponentInChildren<InteractiveObjectDetector>(); return _iod;}}
    public SisyState state;
    SpriteRenderer _spriteRenderer;
    SpriteRenderer spriteRenderer { get { if (!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>(); return _spriteRenderer; } }
    public SisyphusSpriteSheet[] spriteCycles;
    public SquiggleSprite blockSprite;
    public SquiggleSprite deathSprite;
    public SquiggleSprite sitSprite;
    public SquiggleSprite winSprite;
    public string walkSound,critSound;
    int pushptr = 0;
    int walkptr = 0;
    float impulse = 0;
    float cdTime = 0;
    float srgTime=0;
    Vector2 lastPos;
    Quaternion lastRot;
    public float cooldownTime;
    public float sRecoveryStanding, sRecoveryMoving;
    public float sBlockDrain, sPushDrain, sRegenDelay, sDepleteDelay;
    public float momentumScaleOnCrit;
    public AudioClip sweetHit;
    float _stamina;
    public float stamina { get { return _stamina; } private set {_stamina=value; }}
    void Awake()
    {
        _stamina = StatManager.statManager.maxStamina(false);
        lastPos = tform.position;
    }
    

    public override void Reset()
    {
        _stamina = StatManager.statManager.maxStamina(false);
        pushptr=0;
        walkptr=0;
        impulse=0;
        cdTime=0;
        srgTime=0;
        state=SisyState.GAME;
        tform.parent=null;
    }

    public void AButton(){
        if(state==SisyState.GAME && iod.current){
            iod.current.OnInteract();
        }
    }

    public void RBButton()
    {
        if (cdTime <= 0)
        {
            pushptr++;
            if (pushptr >= spriteCycles[walkptr].sprites.Length)
            {
                pushptr = 0;
            }
            if (rockDetector.rock)
            {
                float suse = sPushDrain;
                if(InputManager.inputManager.LBHold && rockBlocker.contact){
                    suse*=1.5f;
                }
                float vscale = StatManager.statManager.velocityKillOnHit(false);
                float strength = StatManager.statManager.pushStrength(false);
                float curveInfluence = StatManager.statManager.curveInfluence(false);
                if(rockDetector.critWindow){
                    if(UnityEngine.Random.Range(0.0f,300.0f) < StatManager.statManager.superCriticalChance(false)){
                        vscale=1;
                        curveInfluence=1.5f;
                        strength*=5;
                        debug("super crit");
                    }
                    else{
                    vscale*=10;
                    vscale = Mathf.Min(vscale, 1);
                    strength*=2;
                    curveInfluence*=1.5f;
                    debug("normal crit");
                    }
                    Sound(critSound);
                }
                
                if(DataManager.dataManager.saveData.hill!=0)
                DataManager.dataManager.saveData.seenPushPrompt=true;
                vscale = 1-Mathf.Max(vscale,0);
                debug("vscale: " + vscale + ", push strength: " + strength + ", curve influence: " + curveInfluence);
                rockDetector.rock.OnPush();
                rockDetector.rock.rb.velocity=vscale*rockDetector.rock.rb.velocity;
                rockDetector.rock.rb.AddForce(((Vector2.right + (curveInfluence*new Vector2(tform.right.x,tform.right.y)))/2).normalized * strength);
                stamina -= suse;
                cdTime=cooldownTime;
                if(stamina > 0){
                srgTime = sRegenDelay;
                }
                else{
                    srgTime=sDepleteDelay;
                }
            }
        }
    }
    public bool enableDebug;
    void debug(string s){
        if(enableDebug)
        print(s);
    }

    public void DPadR()
    {
        if ((state==SisyState.GAME || state==SisyState.SIT) && (impulse <= StatManager.statManager.statDict[SisyStats.AGILITY].value * 0.25f&& !InputManager.inputManager.LBHold))
        {
            if(state==SisyState.SIT){
                state=SisyState.GAME;
            }
            if(DataManager.dataManager.saveData.hill!=0)
            DataManager.dataManager.saveData.seenMovePrompt=true;
            walkptr++;
            Sound(walkSound);
            if (walkptr >= spriteCycles.Length)
            {
                walkptr = 0;
            }
            impulse = StatManager.statManager.strideLength(false);
        }
    }

    public void Kill(Rock r)
    {
        if(state != SisyState.DEAD){
            bloodTrig.Trigger();
        state = SisyState.DEAD;
        tform.parent = r.tform;
        
        if(rockDetector.rock)
        rockDetector.rock.attached=true;
        DataManager.dataManager.saveData.bloodstain=true;
        DataManager.dataManager.saveData.bloodstainhill=DataManager.dataManager.saveData.hill;
        DataManager.dataManager.saveData.bloodstainExperience=StatManager.statManager.curExp;
        DataManager.dataManager.saveData.bloodstainLocation=tform.position;
        DataManager.dataManager.saveData.bloodstainRotation=tform.rotation;
        
        StatManager.statManager.curExp=0;
        }
    }

    public float staminaOutCooldown;
    float sotime;
    
    bool staminaOut;


    void Update()
    {
        if (state == SisyState.GAME)
        {
            float srs = 0;
            if(srgTime <= 0)
            {
                srs = (impulse > 0) ? sRecoveryMoving : sRecoveryStanding;
            }
            else{
                srgTime -= Time.deltaTime;
            }
            if (InputManager.inputManager.LBHold && stamina > 0)
            {
                if(DataManager.dataManager.saveData.hill!=0)
                DataManager.dataManager.saveData.seenBlockPrompt=true;
                rockBlocker.active=true;
                if(rockBlocker.contact)
                {
                    srs = -sBlockDrain;
                    srgTime = sRegenDelay;
                }
                if(cdTime <=0){
                    spriteRenderer.sprite=blockSprite.curFrame;
                }else{
                    
                spriteRenderer.sprite = spriteCycles[walkptr].sprites[pushptr].curFrame;
                }
            }
            else{if(stamina<=0){
                srgTime=sDepleteDelay;
            }
                rockBlocker.active=false;
                spriteRenderer.sprite = spriteCycles[walkptr].sprites[pushptr].curFrame;
            }
            
            
                RaycastHit2D rch = Physics2D.Raycast(tform.position, -Vector2.up, 3f);
                Debug.DrawRay(tform.position, -Vector2.up*3f);
                if (rch.collider!=null)
                {
                    Vector2 goalUp = rch.normal;
                    Vector2 currentUp = tform.rotation * Vector2.up;
                    tform.rotation = Quaternion.FromToRotation(currentUp, goalUp) * tform.rotation;
                    //tform.position = new Vector2(rch.point.x, rch.point.y)+new Vector2(tform.up.x,tform.up.y)*0.8f;
                }
                if (cdTime > 0)
                {
                    cdTime -= Time.deltaTime;
                }
                tform.position += impulse * Time.deltaTime * tform.right;
                if (impulse > 0)
                {
                    impulse -= StatManager.statManager.statDict[SisyStats.CON].value * Time.deltaTime;
                }
                stamina += srs*Time.deltaTime;
                stamina=Mathf.Min(stamina,StatManager.statManager.maxStamina(false));
        }
        else if(state==SisyState.DEAD){
            spriteRenderer.sprite = deathSprite.curFrame;
        }
        else if(state==SisyState.SIT){
            spriteRenderer.sprite=sitSprite.curFrame;
        }
        else if(state==SisyState.WIN){
            spriteRenderer.sprite=winSprite.curFrame;
        }
    }

    [Serializable]
    public class SisyphusSpriteSheet
    {
        public SquiggleSprite[] sprites;
    }
}