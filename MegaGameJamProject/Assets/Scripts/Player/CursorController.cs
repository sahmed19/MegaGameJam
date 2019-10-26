using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    public static CursorController instance;

    void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        SetPositionToMousePosition();
    }

    void SetPositionToMousePosition() {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

}
