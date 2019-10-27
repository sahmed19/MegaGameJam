using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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

    public PostProcessVolume volume;  

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

        elictedProxyPosition += new Vector3(
            Mathf.Sin(Time.time * 13.0f) * eqMagnitude, 
            Random.Range(-1f, 1f) * eqMagnitude) * .05f;

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

        LensDistortion fisheye;

        bool fisheyeWorked = volume.profile.TryGetSettings<LensDistortion> (out fisheye);

        Camera overlayCamera = playerOverlayCamera.GetComponent<Camera>();

        float f = overlayCamera.orthographicSize;

        eqMagnitude = 2.0f;

        for(int i = 0; i < 30; i++) {

            if(fisheyeWorked) {
                fisheye.intensity.value = Mathf.Lerp(-100f, 0f,  i / 30.0f);
            }

            overlayCamera.orthographicSize = Mathf.Lerp(f * 3f, f, (i/30.0f) % 1f);//2 * Mathf.Abs((.5f - (i/30.0f))));

            material.SetFloat("_Cutoff", 1f - (i / 30.0f));
            yield return new WaitForSeconds(.01f);

        }

        if(fisheyeWorked) {
            fisheye.intensity.value = 0f;
        }

        overlayCamera.orthographicSize = f;

        playerOverlayCamera.SetActive(false);
        material.SetFloat("_Cutoff", 0f);

    }

}