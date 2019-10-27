using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSequence : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject tempLockPosition;
    [SerializeField] GameObject playerStandIn;
    [SerializeField] GameObject playerReal;
    [SerializeField] float targetX;
    [SerializeField] GameObject mask1;
    [SerializeField] GameObject mask2;
    bool isDone;

    // Start is called before the first frame update
    void Start()
    {
        playerStandIn.GetComponent<SpriteRenderer>().enabled = true;
        mainCamera.GetComponent<CameraFollow>().target = tempLockPosition.transform;
        isDone = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(playerStandIn != null)
        {
            if ((uint)playerStandIn.transform.position.x == (uint)targetX)
            {
                Debug.Log("hit pos");
                isDone = true;
                Destroy(mask1);
                Destroy(mask2);
                playerReal.transform.position = playerStandIn.transform.position;
                Destroy(playerStandIn);
                mainCamera.GetComponent<CameraFollow>().target = playerReal.transform;
            }

            if (!isDone)
            {
                playerStandIn.transform.position = new Vector3(playerStandIn.transform.position.x + 0.008f, playerStandIn.transform.position.y, 0);
            }
        }
    }
}
