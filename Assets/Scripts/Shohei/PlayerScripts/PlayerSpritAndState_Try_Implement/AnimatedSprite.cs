using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites; //
    public float framerate = 1f / 6f;

    private SpriteRenderer spriteRenderer;
    private int frame;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animation), framerate, framerate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animation()
    {
        frame++;
        if (frame == sprites.Length)
        {
            frame = 0;
        }

        if (frame > 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }
    }
}
