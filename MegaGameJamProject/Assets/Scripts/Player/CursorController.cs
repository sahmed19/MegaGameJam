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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        SetPositionToMousePosition();
    }

    void SetPositionToMousePosition() {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position += Vector3.forward * 10f;

    }

}
