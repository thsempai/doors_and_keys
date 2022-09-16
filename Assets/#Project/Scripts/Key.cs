using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    [HideInInspector] public LevelManager manager;
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            gameObject.SetActive(false);
            manager.KeyIsReached();
    }
   }

}
