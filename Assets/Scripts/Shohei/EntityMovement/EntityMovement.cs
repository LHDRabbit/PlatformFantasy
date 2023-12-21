using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.Windows;

public class EntityMovement : MonoBehaviour
{
    public float enemySpeed = 1.5f;
    public Vector2 direction = Vector2.left;
    public float gravity = -9f;

    private Rigidbody2D enemyRigidbody;
    private Vector2 velocity;

    // (W) Change in a facing sprite direction - 
    private float input;
    public SpriteRenderer enemySpriteRenderer;

    // Enemy pre-movement setup - START //

    private void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enabled = false; // Make sure the entity does not move right away
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        enemyRigidbody.velocity = Vector2.zero; // when a player hits an enemy, this disables the movement and could start showing a lost animation.
        enemyRigidbody.WakeUp();
    }

    private void OnDisable()
    {
        enemyRigidbody.Sleep();
    }
    // Enemy pre-movement setup - END //

    // Enemy movement scripts - START //
    private void Update() // FixedUpdate can be used.
    {
        SpriteDirection();
        HorizontalMovement();
    }

    // (W) A method that changes a facing charactor direction
    private void SpriteDirection()
    {
        if (input < 0)
        {
            enemySpriteRenderer.flipX = true;
        }
        else if (input > 0)
        {
            enemySpriteRenderer.flipX = false;
        }
    }

    private void HorizontalMovement()
    {
        velocity.x = direction.x * enemySpeed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        enemyRigidbody.MovePosition(enemyRigidbody.position + velocity * Time.fixedDeltaTime);
    }



}
