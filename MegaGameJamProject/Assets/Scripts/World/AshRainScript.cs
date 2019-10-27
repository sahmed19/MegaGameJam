using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshRainScript : MonoBehaviour
{
    Player player;

    void Start() {
        player = Player.INSTANCE;
    }

    void Update() {
        if(player.PlayerInUnderworld()) {
            transform.position = player.transform.position + new Vector3(-2f, -2f, 0f);
        }
    }

}
