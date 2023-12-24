using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameObject target;
    public float ballSpeed;
    Rigidbody2D ballRigidbody;

    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * ballSpeed;
        ballRigidbody.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 2.5f);
    }
}
