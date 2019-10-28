using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthScript : MonoBehaviour
{
    
    public GameObject healthbar;

    public EnemyHP hP;

    public void SetHPRatio(float r) {
        healthbar.transform.localScale = Vector3.up + Vector3.forward + (Vector3.right * r);
    }

    void Update() {

        float r = hP.currentHealth / 20f;
        SetHPRatio(r);

    }

}
