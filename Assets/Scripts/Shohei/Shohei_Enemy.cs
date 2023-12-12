using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

public class Shohei_Enemy : MonoBehaviour
{
    public float enemySpeed = 2f;
    private bool autoMove = true;
    private Rigidbody2D enemyrb1;

    public SpriteRenderer enemyRenderer;
    public float inputEnemy;

    void Start()
    {
        enemyrb1 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (autoMove)
        {
            enemyrb1.velocity = new Vector2(-enemySpeed, enemyrb1.velocity.y);
        }
        else
        {
            enemyrb1.velocity = new Vector2(enemySpeed, enemyrb1.velocity.y);
        }

        // Flip the face of charactor 
        inputEnemy = Input.GetAxisRaw("Horizontal");
        if (inputEnemy < 0)
        {
            enemyRenderer.flipX = true;
        }
        else if (inputEnemy > 0)
        {
            enemyRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            autoMove = !autoMove;
        }

        if (other.gameObject.CompareTag("GroundPosition"))
        {
            Destroy(gameObject);
        }
    }
}
