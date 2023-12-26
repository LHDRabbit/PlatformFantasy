using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite lostSprite;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }

    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;

        if (lostSprite != null)
        {
            spriteRenderer.sprite = lostSprite;
        }
    }

    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        GetComponent<Rigidbody2D>().isKinematic = true;

        // Need to organize -- Start
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();
        EnemyControllerLeftRight enemyControllerLeftRight = GetComponent<EnemyControllerLeftRight>();
        EnemyAIChase enemyAIChase = GetComponent<EnemyAIChase>();
        EnemyAIChaseShoot enemyAIChaseShoot = GetComponent<EnemyAIChaseShoot>();
        EnemyAIChaseStable enemyAIChaseStable = GetComponent<EnemyAIChaseStable>();
        // Need to organize -- End

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (entityMovement != null)
        {
            entityMovement.enabled = false;
        }

        if (enemyControllerLeftRight != null)
        {
            enemyControllerLeftRight.enabled = false;
        }

        if (enemyAIChase != null)
        {
            enemyAIChase.enabled = false;
        }

        if (enemyAIChaseShoot != null)
        {
            enemyAIChaseShoot.enabled = false;
        }

        if (enemyAIChaseStable != null)
        {
            enemyAIChaseStable.enabled = false;
        }
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 3f;

        float jumpVelocity = 10f;
        float gravity = -1f;

        Vector3 velocity = Vector3.up * jumpVelocity;

        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
