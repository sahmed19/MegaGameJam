using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float dampingTime = 1f;
	public float PPU = 64f; //Pixels per unit

	private Vector3 velocity;
	private Vector3 proxyPosition;

    public static CameraFollow INSTANCE;

    private bool flipped = false;
    private bool flipChanged = false;

    public Transform laggingCamera;

    public Material material;

    void Awake() {
        INSTANCE = this;
    }

	private void LateUpdate() {

        if(flipChanged) {
            proxyPosition += Vector3.up * (flipped? -1f : 1f) * 100f;
            laggingCamera.transform.localPosition = Vector3.down * 100f * (flipped? -1f : 1f);
            flipChanged = false;
        }

		proxyPosition = Vector3.SmoothDamp(proxyPosition, target.position, ref velocity, dampingTime);


		transform.position = new Vector3(

		//14.79243058

		Mathf.Round(proxyPosition.x * PPU)/PPU,
		Mathf.Round(proxyPosition.y * PPU)/PPU,
		-10f

		);
	}

    public void FlipWorld() {
        flipped = !flipped;
        flipChanged = true;
        StartCoroutine(MaterialShift());
    }

    IEnumerator MaterialShift() {
        for(int i = 0; i < 30; i++) {

            material.SetFloat("_Cutoff", 1f - (i / 30.0f));
            yield return new WaitForSeconds(.01f);

        }

        material.SetFloat("_Cutoff", 0f);

    }

}