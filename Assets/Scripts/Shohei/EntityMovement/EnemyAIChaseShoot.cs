using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIChaseShoot : MonoBehaviour
{
    public float enemySpeed;
    public float withinTheRange;
    public float shootingRange;
    public float releaseRate = 0.5f;
    private float nextReleaseTime;
    public GameObject ball;
    public GameObject releasePoint;

    private Transform playerToFollow;

    private void Start()
    {
        playerToFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceFomPlayer = Vector2.Distance(playerToFollow.position, transform.position);

        if (distanceFomPlayer < withinTheRange && distanceFomPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, playerToFollow.position, enemySpeed * Time.deltaTime);
        }
        else if (distanceFomPlayer <= shootingRange && nextReleaseTime < Time.time)
        {
            Instantiate(ball, releasePoint.transform.position, Quaternion.identity);
            nextReleaseTime = Time.time + releaseRate;
        }
    }

    private void OnDrawGizmosSelected() // when a player is within the range, the enemy follows.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, withinTheRange);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
