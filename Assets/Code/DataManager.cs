using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class DataManager : NVComponent {
    static DataManager _dataManager;
    public static DataManager dataManager {
        get{
            if(!_dataManager){
                _dataManager=FindObjectOfType<DataManager>();
            }
            return _dataManager;
        }
    }


    public bool useDummySave;
    SaveData _saveData;

    string saveFile;

    void Awake(){
        saveFile=Application.persistentDataPath + "/sisys.json";
    }

    public SaveData saveData {get{return _saveData;} private set {_saveData=value;}}


    public void SaveGame(){
        if(!useDummySave){
        string jsonString = JsonUtility.ToJson(saveData);
        if(saveData.hill < 8){
            File.WriteAllText(saveFile,jsonString);
        }
        else{
            File.Delete(saveFile);
        }
        }
    }

    public void DeleteSaveFile(){
        File.Delete(saveFile);
    }

    public void CreateFile(){
        saveData=GameController.gameController.defaultSaveData;
        SaveGame();
    }

    public bool LoadGame(){
        if(useDummySave){
            saveData=GameController.gameController.defaultSaveData;
            return true;
        }
        else if(File.Exists(saveFile)){
            string fileContents = File.ReadAllText(saveFile);
            saveData = JsonUtility.FromJson<SaveData>(fileContents);
            return true;
        }
        return false;
    }
}