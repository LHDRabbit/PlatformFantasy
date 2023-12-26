using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAIJumping : MonoBehaviour
{
    [Header("Patrolling")]
    [SerializeField] private float enemySpeed;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float enemyRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask obstacleLayer;

    private float enemyDirection = -1;
    private bool facingLeft = true;

    private bool checkingGround;
    private bool checkingObstacle;

    private Rigidbody2D enemyRigidbody;

    [Header("Jumping")]
    [SerializeField] private float jumpHight;
    [SerializeField] private Transform playerToReach;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 jumpingRadius;
    private bool isGrounded;
    public float withinTheRange;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, enemyRadius, groundLayer);
        checkingObstacle = Physics2D.OverlapCircle(wallCheckPoint.position, enemyRadius, obstacleLayer);

        isGrounded = Physics2D.OverlapBox(groundCheck.position, jumpingRadius, 0, groundLayer);

        Patroll();
        Jump();        
    }

    private void Patroll()
    {
        if (!checkingGround || checkingObstacle)
        {
            if(facingLeft)
            {
                Flip();
            }
            else if (!facingLeft)
            {
                Flip();
            }
        }
        enemyRigidbody.velocity = new Vector2(enemySpeed * enemyDirection, enemyRigidbody.velocity.y);
    }

    private void Jump()
    {
        float distanceFromPlayer = Vector2.Distance(playerToReach.position, transform.position);
        //float distanceFromPlayer = playerToReach.position.x - transform.position.x; // Detect how far the enemys is from player and which direction is from enemy to player

        if (isGrounded && distanceFromPlayer <= withinTheRange)
        {
            enemyRigidbody.AddForce(new Vector2(distanceFromPlayer, jumpHight),ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        enemySpeed *= -1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, enemyRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, enemyRadius);

        Gizmos.DrawWireSphere(transform.position, withinTheRange); //
    }
}
