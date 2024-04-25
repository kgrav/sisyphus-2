using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;


public enum GameState { WAIT, TITLE, FIREUI, INGAME, PREPGAME, PRETITLE, OPTIONS }
public class GameController : NVComponent
{

    static GameController _gameController;
    int hill = 1;
    public float squiggleTimer = 0;
    float sqTime, respawnTime;
    int _squiggleCount = 0;
    public string msgSound;
    public int loadCode;
    public MessageFadeIn loadFade;
    public bool titleMode;
    public SaveData defaultSaveData;
    public int squiggleCount { get { return _squiggleCount; } }
    GameState _gameState = GameState.WAIT;
    public GameState gameState {get{return _gameState;} private set{_gameState=value;}}
    public static GameController gameController
    {
        get { if (!_gameController) _gameController = FindObjectOfType<GameController>(); return _gameController; }
    }
    void SetTitle()
    {
    }
    public void SetGameState(GameState state)
    {
        if (gameState == state)
            return;
        gameState = state;

        switch (state)
        {
            case GameState.OPTIONS:
                
                foreach (UIPane p in paneDict.Values)
                {
                    p.OnDeactivate();
                }
                    paneDict[PaneType.OPTIONS].OnActivate();
                    break;
            case GameState.PRETITLE:
                AudioManager.SetBGM(Songs.PUSHUP);
                if(!DataManager.dataManager.LoadGame())
                {
                    DataManager.dataManager.CreateFile();
                }
                DontDestroyOnLoad(this.gameObject);
                AudioManager.audioManager.vsaSfx.SetInitVol(DataManager.dataManager.saveData.sfxLevel);
                print("pretitle");
                AudioManager.audioManager.vsaMusic.SetInitVol(DataManager.dataManager.saveData.musicLevel);
                StatManager.statManager.ConsumeSaveData(DataManager.dataManager.saveData);
                foreach (UIPane p in paneDict.Values)
                {
                    p.OnDeactivate();
                }
                paneDict[PaneType.PRETITLE].OnActivate();
                UIPane.activePane.mFI.ShowMessage((int)TitleFadeMessages.CONREQ);
                break;
            case GameState.TITLE:
                foreach (Spawner s in FindObjectsOfType<Spawner>())
                {
                    if(!(DataManager.dataManager.saveData.hill==0 && s.rockSpawner))
                    s.Spawn();
                }
                foreach (UIPane p in paneDict.Values)
                {
                    p.OnDeactivate();
                }
                InputManager.inputManager.uiMode = true;
                paneDict[PaneType.TITLE].OnActivate();
                break;
            case GameState.PREPGAME:
                if(DataManager.dataManager.saveData.hill==0){
                    foreach (Spawner s in FindObjectsOfType<Spawner>())
                {
                    if(s.rockSpawner)
                    s.Spawn();
                }
                SetGameState(GameState.INGAME);
                }
                else{
                    ResetAll();
                    SetGameState(GameState.INGAME);
                }
                break;
            case GameState.INGAME:
                foreach (UIPane p in paneDict.Values)
                {
                    p.OnDeactivate();
                }
                DataManager.dataManager.SaveGame();
                AudioManager.SetBGM(FindObjectOfType<HillData>().bgm);
                paneDict[PaneType.INGAME].OnActivate();
                InputManager.inputManager.uiMode = false;
                break;
            case GameState.FIREUI:
                InputManager.inputManager.uiMode = true;
                print("fireui");
                foreach (UIPane p in paneDict.Values)
                {
                    p.OnDeactivate();
                }
                paneDict[PaneType.LEVELUP].OnActivate();
                StatManager.slb = new StatLevelBean();
                SisyphusController.sisy.state = SisyState.SIT;
                break;
        }
    }




    public void NewGame()
    {
        if(DataManager.dataManager.saveData.hill==0){
            Sound(msgSound);
            SetGameState(GameState.PREPGAME);
        }
        else{
            DataManager.dataManager.saveData.hill=1;
            Sound(msgSound);
            ContinueGame();
        }
    }

    public void ContinueGame()
    {
        SetGameState(GameState.PREPGAME);
    }

    public void QuitGame()
    {
        print("quit");
        Application.Quit();
    }

    void ResetAll()
    {
        sqTime = squiggleTimer;
        _squiggleCount = 0;
        FindObjectOfType<DeathCatcher>().Reset();
        print("stain hill: " + DataManager.dataManager.saveData.bloodstainhill + ", main hill: " + DataManager.dataManager.saveData.hill);
        if (DataManager.dataManager.saveData.bloodstain && DataManager.dataManager.saveData.bloodstainhill==DataManager.dataManager.saveData.hill)
        {
            Bloodstain.bloodstain.Spawn(DataManager.dataManager.saveData.bloodstainLocation,
                                        DataManager.dataManager.saveData.bloodstainRotation,
                                        DataManager.dataManager.saveData.bloodstainExperience);
        }
        foreach (Spawner s in FindObjectsOfType<Spawner>())
        {
            s.Spawn();
        }
    }

    void Update()
    {
        sqTime -= Time.deltaTime;
        if (sqTime <= 0)
        {
            sqTime += squiggleTimer;
            _squiggleCount++;
            if (_squiggleCount == 100)
            {
                _squiggleCount = 0;
            }
        }
    }

    public void OnHoldMessage(int msg){
        if(msg==loadCode){
            SceneManager.LoadScene("Scenes/hill" + DataManager.dataManager.saveData.hill, LoadSceneMode.Single);
        }
    }
    public void FinishMessage(int msg)
    {
        if(msg==loadCode){
            SetGameState(GameState.PREPGAME);
        }
        if (gameState == GameState.INGAME)
        {
            if (msg == (int)InGameFadeMessages.DEATH)
            {
                if(DataManager.dataManager.saveData.hill == 0){
                    DataManager.dataManager.saveData.hill = 1;
                    loadFade.ShowMessage(loadCode);
                }
                else{
                ResetAll();
                }
                //auto-save
            }
            if (msg == (int)InGameFadeMessages.SUCCESS)
            {
                loadFade.ShowMessage(loadCode);
                //auto-save
            }
        }
        else if (gameState == GameState.PRETITLE)
        {
            if (msg == (int)TitleFadeMessages.CONREQ)
            {
                SceneManager.LoadScene("Scenes/hill" + DataManager.dataManager.saveData.hill, LoadSceneMode.Single);
                print("setting credits");
                UIPane.activePane.mFI.ShowMessage((int)TitleFadeMessages.CREDS);
            }
            else if (msg == (int)TitleFadeMessages.CREDS)
            {
                SetGameState(GameState.TITLE);
            }
        }
    }
    public void OnDeathCatch()
    {
        if (UIPane.activePane && UIPane.activePane.mFI)
        {    UIPane.activePane.mFI.ShowMessage((int)InGameFadeMessages.DEATH);
        Sound(msgSound);}
    }

    public void OnVictoryCatch(int nexthill)
    {
        DataManager.dataManager.saveData.hill=nexthill;
        SisyphusController.sisy.state=SisyState.WIN;

        if (UIPane.activePane && UIPane.activePane.mFI)
        {    UIPane.activePane.mFI.ShowMessage((int)InGameFadeMessages.SUCCESS);
        Sound(msgSound);}
    }

    public PaneDef[] paneSerial;
    Dictionary<PaneType, UIPane> _paneDict = null;
    public Dictionary<PaneType, UIPane> paneDict
    {
        get
        {
            if (_paneDict == null)
            {
                _paneDict = new Dictionary<PaneType, UIPane>();
                foreach (PaneDef pd in paneSerial)
                {
                    _paneDict.Add(pd.state, pd.pane);
                }
            }
            return _paneDict;
        }
    }
    [Serializable]
    public class PaneDef
    {
        public PaneType state;
        public UIPane pane;
    }
}