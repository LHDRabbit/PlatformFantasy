using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIChase : MonoBehaviour
{
    public GameObject playerToBeChased;
    public float enemyrSpeed = 1.1f;

    private float distance = 5f;
    void Start()
    {
        
    }


    void Update()
    {
        EnemyChase();
    }

    private void EnemyChase()
    {
        distance = Vector2.Distance(transform.position, playerToBeChased.transform.position); // take the difference between two positions
        Vector2 direction = playerToBeChased.transform.position - transform.position;

        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < 6) // if the distance btw player and enemy is less than 4m
        {
            transform.position = Vector2.MoveTowards(this.transform.position, playerToBeChased.transform.position, enemyrSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}
