using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitfallDetector : MonoBehaviour
{
    
    bool inPitfall;

    public bool IsInPitfall() {
        return inPitfall;
    }

    void OnTriggerEnter2D(Collider2D other) {

        Debug.Log("Entered Pitfall");        
        
        inPitfall = true;

    }

    void OnTriggerStay2D(Collider2D other) {

        Debug.Log("Entered Pitfall");        
        
        inPitfall = true;

    }

    void OnTriggerExit2D(Collider2D other) {
                Debug.Log("Exit Pitfall");
        inPitfall = false;
    }

}
