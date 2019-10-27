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
    public GameObject playerOverlayCamera;

    public Material material;

    float eqMagnitude;

    void Awake() {
        INSTANCE = this;
    }

    void Update() {
        eqMagnitude -= Time.deltaTime * 3.0f;
        if(eqMagnitude < 0) {
            eqMagnitude = 0f;
        }
    }

	private void LateUpdate() {

        if(flipChanged) {
            proxyPosition += Vector3.up * (flipped? -1f : 1f) * 100f;
            laggingCamera.transform.localPosition = Vector3.down * 100f * (flipped? -1f : 1f);
            flipChanged = false;
        }

		proxyPosition = Vector3.SmoothDamp(proxyPosition, target.position, ref velocity, dampingTime);

        Vector3 elictedProxyPosition = proxyPosition;

        elictedProxyPosition += new Vector3(Mathf.Sin(Time.time * 13.0f) * eqMagnitude, Random.Range(-1f, 1f) * eqMagnitude) * .02f;

		transform.position = new Vector3(

		Mathf.Round(elictedProxyPosition.x * PPU)/PPU,
		Mathf.Round(elictedProxyPosition.y * PPU)/PPU,
		-10f

		);
	}

    public void FlipWorld() {
        flipped = !flipped;
        flipChanged = true;
        StartCoroutine(MaterialShift());
    }

    public void ShakeScreen(float power) {
        eqMagnitude += power;
    }

    IEnumerator MaterialShift() {

        playerOverlayCamera.SetActive(true);

        for(int i = 0; i < 30; i++) {

            material.SetFloat("_Cutoff", 1f - (i / 30.0f));
            yield return new WaitForSeconds(.01f);

        }

        playerOverlayCamera.SetActive(false);
        material.SetFloat("_Cutoff", 0f);

    }

}