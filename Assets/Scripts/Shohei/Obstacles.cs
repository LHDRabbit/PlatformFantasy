using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacles : MonoBehaviour
{
    public UnityEvent OnEntitiesCollided;

    // tryout1-0 OnCollisionEnter2D or OnTriggerEnter2D EnntitiesColidedWithEnemy -> change direction
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Obstacle"))
        {
            Actions.OnEntitiesCollided?.Invoke();
        } 
    }
}
