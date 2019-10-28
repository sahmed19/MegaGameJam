using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    public static CursorController instance;

    void Awake() {
        instance = this;
    }

    void Start() {
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SetPositionToMousePosition();
    }

    void SetPositionToMousePosition() {

        
        Vector2 correctedMousePosition = new Vector2(
            320f * Input.mousePosition.x / Screen.width,
            240f * Input.mousePosition.y / Screen.height);

        transform.position = Camera.main.ScreenToWorldPoint(correctedMousePosition);
        transform.position += Vector3.forward * 10f + (Player.INSTANCE.PlayerInUnderworld()? (Vector3.down * 100f) : Vector3.zero);

    }

}
