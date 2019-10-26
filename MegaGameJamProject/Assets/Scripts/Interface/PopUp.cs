using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] Text popUpText;
    [SerializeField] Vector3 offset;

    void Start()
    {
        TextTransparencyReset();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        popUpText.transform.position = collision.gameObject.transform.position + offset;
        //popUpText.transform.parent = collision.gameObject.transform;
        popUpText.transform.SetParent(collision.gameObject.transform);
        popUpText.CrossFadeAlpha(100, 100f, false);
    }

    void TextTransparencyReset()
    {
        popUpText.CrossFadeAlpha(0, 0f, false);
    }
}

