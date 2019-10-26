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

    void Start()
    {
        TextTransparencyReset();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && destroyOnClick)
        {
            DestroyText();
        }

        if(key != "")
        {
            if (Input.GetKeyDown(key) && destroyOnKey)
            {
                DestroyText();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        popUpText.transform.position = collision.gameObject.transform.position + offset;
        popUpText.CrossFadeAlpha(100, fadeSpeed, false);
        popUpText.transform.SetParent(collision.gameObject.transform);
    }

    void TextTransparencyReset()
    {
        popUpText.CrossFadeAlpha(0, 0f, false);
    }

    void DestroyText()
    {
        if(popUpText != null)
        {
            popUpText.transform.parent = null;
            Destroy(popUpText);
        }
    }
}

