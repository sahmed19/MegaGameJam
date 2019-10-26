﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectMainCamera : MonoBehaviour {

    [SerializeField]
    public int PPU = 64;
    [SerializeField]
    public int screenHeight = 480;
    [SerializeField]
    public float orthoSize = 7.5f;

	private Camera pixelCamera;

	void Start() {
		UpdateOrthoSize();
        ApplyOrthoSize();
	}


    public void UpdateOrthoSize()
    {
        orthoSize = (screenHeight  * .5f) / PPU;
    }

    public void ApplyOrthoSize()
    {
		pixelCamera = GetComponent<Camera> ();
		pixelCamera.rect = new Rect (0, 0, 1, 1);

        Camera.main.orthographicSize = orthoSize;
		float screenRatio = (float) pixelCamera.aspect;
		float targetRatio = 4f / 3f;

		Debug.Log (screenRatio + " " + targetRatio);

		float screenWidth = targetRatio/screenRatio;

		Rect rect = new Rect ((1-screenWidth)/2, 0, screenWidth, 1);

		pixelCamera.rect = rect;

    } 

}