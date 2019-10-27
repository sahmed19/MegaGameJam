using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderworldEnemySpawner : MonoBehaviour
{
    
    public GameObject enemyPrefab;

    float severity = 0f;

    Player player;

    void Start() {
        player = Player.INSTANCE;
    }

    void Update() {

        

        Debug.Log("SEVERITY: " + severity);


    }

}
