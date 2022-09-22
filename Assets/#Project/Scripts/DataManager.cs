using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

[Serializable]
class GameData{
    public string sceneName;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;

    public static string dataPath {
        get {return Application.persistentDataPath + "/gameSave.dat";}
    }

    public static bool saveFileExists{
        get {return File.Exists(dataPath);}
    }

    private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this) {
            Destroy(gameObject);
        }
    }

    public static void Save(){
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataPath);

        GameData data = new GameData();
        data.sceneName = SceneManager.GetActiveScene().name;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log($"Save file {dataPath} as been created.");
    }

    public static void Load(){
        if(!saveFileExists) return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(dataPath, FileMode.Open);
        GameData data = bf.Deserialize(file) as GameData;
        SceneManager.LoadScene(data.sceneName);
    }
}
