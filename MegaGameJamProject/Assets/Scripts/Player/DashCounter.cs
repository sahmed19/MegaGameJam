using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCounter : MonoBehaviour
{
    
    public GameObject[] fireballs;


    Player player;

    void Start() {
        player = Player.INSTANCE;
    }

    void Update() {
        
        int dash = player.DashCount();

        fireballs[0].SetActive(dash > 0);
        fireballs[1].SetActive(dash > 1);
        fireballs[2].SetActive(dash > 2);

    }

}
