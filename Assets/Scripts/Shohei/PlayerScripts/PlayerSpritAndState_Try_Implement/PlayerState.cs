using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer largeRenderer;
    private LostAnimation lostAnimation;

    public bool big => largeRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool lost => lostAnimation.enabled;

    private void Awake()
    {
        lostAnimation = GetComponent<LostAnimation>();
    }

    public void Hit()
    {
        if (big)
        {
            Shrink();
        } else
        {
            Lost();
        }
    }

    private void Shrink()
    {

    }

    private void Lost()
    {
        smallRenderer.enabled = false;
        largeRenderer.enabled = false;
        lostAnimation.enabled = false;

        GameManager.Instance.ResetLevel(3f);
    }
}
