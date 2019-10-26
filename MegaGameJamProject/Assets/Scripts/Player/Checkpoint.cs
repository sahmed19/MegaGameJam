using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    //Declares Player
    Player player;
    public static GameObject currentCheckpoint;

    //Set Current Check Point
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentCheckpoint = gameObject;
            Debug.Log("Checkpoint!");
        }
    }

    // Calls most recent Checkpoint
    public static void RespawnPlayer()
    {
        Debug.Log("Player Respawn");
        Player.INSTANCE.transform.position = currentCheckpoint.transform.position;
    }
}
