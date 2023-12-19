using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHorizontal : MonoBehaviour
{
    //[SerializeField] private Transform platform;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed = 3f;
    private Vector2 targetPosition;

    void Start()
    {
        targetPosition = endPoint.position;              
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, startPoint.position) < 0.1f)
        {
            targetPosition = endPoint.position;
        }

        if (Vector2.Distance(transform.position, endPoint.position) < 0.1f)
        {
            targetPosition = startPoint.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
