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


        spriteRenderer.color = player.PlayerInUnderworld()? Color.red : Color.white;
        int healthRegulated = (int) (4 - player.currentHealth);
        healthRegulated = Mathf.Clamp(healthRegulated, 0, 3);
        spriteRenderer.sprite = sprites[healthRegulated];

    }
}
