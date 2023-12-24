using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAIChaseStable : MonoBehaviour
{
    public float enemySpeed;
    public float withinTheRange;
    private Transform playerToFollow;

    private void Start()
    {
        playerToFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceFomPlayer = Vector2.Distance(playerToFollow.position, transform.position);

        if (distanceFomPlayer < withinTheRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, playerToFollow.position, enemySpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected() // when a player is within the range, the enemy follows.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, withinTheRange);
    }
}
