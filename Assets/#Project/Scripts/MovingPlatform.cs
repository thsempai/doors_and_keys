using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class MovingPlatform : MonoBehaviour
{

    public Vector2 translation = Vector2.up;

    [Range(0f,10f)]
    public float timeToMove = 1f;
    private bool reverse = false;

    private Vector3 start;
    private Vector3 end;
    private bool inTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        end = transform.position + (Vector3)translation;
        StartCoroutine(Move());
    }

    private IEnumerator Move(){
        float time = 0f;
        float ratio = 0f;
        while(true){

            while(ratio < 1){
                time += Time.deltaTime;
                ratio = time/timeToMove;

                if(reverse)transform.position = Vector3.Lerp(end, start, ratio);
                else transform.position = Vector3.Lerp(start, end, ratio);

                yield return null;
            }
            time = 0f;
            ratio = 0f;
            reverse = !reverse;
        }

    } 
    
    private void OnTriggerStay2D(Collider2D other){
        if(inTrigger) return;
        if(other.CompareTag("Player")){
            if(other.GetComponent<PlatformerCharacter2D>().m_Grounded){
                inTrigger = true;
                other.transform.parent = transform;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            other.transform.parent = null;
            inTrigger = false;
        }
    }

    private void OnDrawGizmos(){
        Collider2D collider = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.red;
        Gizmos.DrawCube(collider.bounds.center + (Vector3) translation, collider.bounds.size);
    }
}
