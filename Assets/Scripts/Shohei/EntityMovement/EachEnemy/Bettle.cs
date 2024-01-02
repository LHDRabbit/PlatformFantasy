using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bettle : MonoBehaviour
{
    //public Sprite flattenSprite;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("GroundPosition"))
        {
            Destroy(gameObject);
        }
    }
}
