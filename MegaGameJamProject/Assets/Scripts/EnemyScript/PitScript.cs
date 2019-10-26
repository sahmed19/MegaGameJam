using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player.INSTANCE.Death();
    }
}
