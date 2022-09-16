using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Key key;
    public Door door;
    public string nextLevel;


    void Start()
    {
        if(key == null){
            Debug.LogError("LevelManger need a key.", gameObject);
        }
        else{
            key.manager = this;
        }

        if(door == null){
            Debug.LogError("LevelManger need a door.", gameObject);
        }
        else{
            door.manager = this;
        }
        
    }

    public void KeyIsReached(){
        door.Opening();
    }

    public void DoorIsReached(){
        SceneManager.LoadScene(nextLevel);
    }


}
