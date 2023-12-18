using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationBase : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement movement; //(A1)

    public Sprite idleSprite;
    public Sprite jumpSprite;
    public Sprite climbSprite;
    public Animation runSprite;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<PlayerMovement>();//(A2)
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        runSprite.enabled = movement.isRunning;

        if (movement.isIdling) //(A3)
        {
            spriteRenderer.sprite = idleSprite;
        }
        else if (movement.isClimbing) //(A4)
        {
            spriteRenderer.sprite = climbSprite;
        }
        else if (!movement.isRunning)
        {
            spriteRenderer.sprite = idleSprite;
        }

    }
}
