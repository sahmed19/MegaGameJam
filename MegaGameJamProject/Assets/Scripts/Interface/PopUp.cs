using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] Text popUpText;
    [SerializeField] Vector3 offset;
    [SerializeField] GameObject player;
    [SerializeField] float fadeSpeed;
    [SerializeField] bool destroyOnClick;
    [SerializeField] bool destroyOnKey;
    [SerializeField] string key;
    bool poppedUp;

    void Start()
    {
        TextTransparencyReset();
        poppedUp = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && destroyOnClick && poppedUp)
        {
            DestroyText();
        }

        if(key != "")
        {
            if (Input.GetKeyDown(key) && destroyOnKey && poppedUp)
            {
                DestroyText();
            }
        }

        if(poppedUp == true)
        {
            popUpText.transform.position = player.gameObject.transform.position + offset;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        poppedUp = true;
        Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        popUpText.transform.position = collision.gameObject.transform.position + offset;
        popUpText.CrossFadeAlpha(100, fadeSpeed, false);
    }

    void TextTransparencyReset()
    {
        popUpText.CrossFadeAlpha(0, 0f, false);
    }

    void DestroyText()
    {
        if(popUpText != null)
        {
            poppedUp = false;
            Destroy(popUpText);
        }
    }
}

