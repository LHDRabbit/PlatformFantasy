using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = 2f;
    private bool autoMove = true;
    private Rigidbody2D enemyrb1;

    public SpriteRenderer enemyRenderer;
    private float inputEnemy;
    private Vector2 velocity;

    void Start()
    {
        enemyrb1 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 position = enemyrb1.position;
        Vector2 positionPlayer = PlayerMovement.velocityStatic;
        position += velocity * Time.fixedDeltaTime;

        //velocity.x = Mathf.MoveTowards(velocity.x, positionPlayer.x, positionPlayer.x);
        enemyrb1.velocity = new Vector2(enemySpeed * Mathf.MoveTowards(velocity.x, positionPlayer.x, positionPlayer.x), enemyrb1.velocity.y);

        //if (autoMove == true)
        //{
        //    enemyrb1.velocity = new Vector2(-enemySpeed, enemyrb1.velocity.y);
        //}
        //else if(autoMove == false)
        //{
        //    enemyrb1.velocity = new Vector2(enemySpeed, enemyrb1.velocity.y);
        //}

    }

    public void ToggleMovingUp()
    {
        if (autoMove == true)
        {
            enemyrb1.velocity = new Vector2(-enemySpeed, enemyrb1.velocity.y);
        }
        else if(autoMove == false)
        {
            enemyrb1.velocity = new Vector2(enemySpeed, enemyrb1.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle") && autoMove == true)
        {
            autoMove = false;
        }

        if (other.gameObject.CompareTag("Obstacle") && autoMove == false)
        {
            autoMove = true;
        }
    }
}
