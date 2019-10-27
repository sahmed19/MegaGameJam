using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkTracker : MonoBehaviour
{
    
    public GameObject spark;

    public GameObject tracker;

    Player player;

    void Start() {
        player = Player.INSTANCE;
    }

    void Update() {
        
        if(!player.PlayerInUnderworld()) {
            tracker.SetActive(false);
            return;
        }

        Vector3 delta = spark.transform.position - player.transform.position;


        transform.eulerAngles = new Vector3(0f, 0f, 
            Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg
        );



        tracker.SetActive(delta.sqrMagnitude > 3f);
        tracker.transform.localPosition = Vector3.right * (.4f + delta.sqrMagnitude * .02f);

    }

}
