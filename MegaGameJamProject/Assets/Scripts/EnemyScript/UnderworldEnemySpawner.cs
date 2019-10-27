﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderworldEnemySpawner : MonoBehaviour
{
    
    public GameObject enemyPrefab;

    float severity = 0f;

    Player player;

    bool playerInUnderworld;
    bool flip = false;

    void Start() {
        player = Player.INSTANCE;
    }


    float timeTracker = 0.0f;

    float spawnTimer = 50f;

    void Update() {

        flip = false;

        if(playerInUnderworld != player.PlayerInUnderworld()) {
            playerInUnderworld = player.PlayerInUnderworld();
            flip = true;
        }

        if(playerInUnderworld) {

            if(flip) {
                timeTracker = 0;
            }

            timeTracker += Time.deltaTime;

            severity = (player.DistanceFromSpark() + timeTracker);

            Debug.Log("SEVERITY: " + severity + " --- DISTANCE: " + player.DistanceFromSpark() + " --- TIME: " + timeTracker);

            Spawner();

        }

    }

    void Spawner() {

        spawnTimer -= Time.deltaTime * severity;

        if(spawnTimer < 0f) {
            Vector3 randomVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized * 20f;
            Instantiate(enemyPrefab, player.transform.position + randomVector, Quaternion.identity);
            spawnTimer = 30f;
        }

    }

}