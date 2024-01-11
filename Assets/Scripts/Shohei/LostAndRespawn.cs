using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostAndRespawn : MonoBehaviour
{
    public Vector2 startPosition;
    public SpriteRenderer spriteRenderer;
    public bool wasLost = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Enemy"))
        {
            Lost();
            //SceneController.instance.ResetLevel();
        }
        
    }

    public void Lost()
    {
        StartCoroutine(Respawn(0.4f));
        //Respawn();
    }

    IEnumerator Respawn(float duration)
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = startPosition;
        spriteRenderer.enabled = true;
        wasLost = false;
    }
}
