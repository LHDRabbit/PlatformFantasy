using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingTriangle : MonoBehaviour
{
    public float triangleSpeed = 4;
    Transform playerToFollow;
    // Start is called before the first frame update
    void Start()
    {
        playerToFollow = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(this.gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerToFollow.position, triangleSpeed * Time.deltaTime);
    }
}
