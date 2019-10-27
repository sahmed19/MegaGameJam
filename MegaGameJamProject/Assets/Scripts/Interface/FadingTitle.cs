using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FadingTitle : MonoBehaviour
{

    TextMeshProUGUI titleText;

    Color startColor = Color.black;
    Color endColor = Color.white;

    public float fadedTime = 4.0f;

    private void Start()
    {
        titleText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeColor());
    }

    IEnumerator FadeColor()
    {
        int iterations = 100;

        for (int i = 0; i < iterations; i++)
        {
            float t = (i * 1.00f) / iterations;
            Color midColor = Color.Lerp(startColor, endColor, t);
            titleText.color = midColor;

            yield return new WaitForSeconds(fadedTime / iterations);

        }

    }
}
