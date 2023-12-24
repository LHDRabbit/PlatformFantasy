using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFlog : MonoBehaviour
{
    public float movementSpeed;

    public Transform groundCheck;
    public float gcRadius = 0.1f;
    public LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * movementSpeed * Vector2.left);
        if (!Physics2D.OverlapCircle(groundCheck.position, gcRadius, groundLayer))
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        movementSpeed *= -1;
    }
}
