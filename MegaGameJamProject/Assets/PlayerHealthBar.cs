using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    private string spriteNames = "healthbar";
    public Sprite[] sprites;
    //Reference to player in Player
    Player player;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.INSTANCE;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        float healthRegulated = Mathf.Clamp(player.currentHealth, 0f, 100f);

        if(healthRegulated <= 100 && healthRegulated > 75)
        {
            spriteDecider(0);
        }
        else if(healthRegulated <= 75 && healthRegulated > 50)
        {
            spriteDecider(1);
        }
        else if(healthRegulated <= 50 && healthRegulated > 25)
        {
            spriteDecider(2);
        }
        else
        {
            spriteDecider(3);
        }


    }

    public void spriteDecider(int which)
    {
            spriteRenderer.sprite = sprites[which];
       
    }
}
