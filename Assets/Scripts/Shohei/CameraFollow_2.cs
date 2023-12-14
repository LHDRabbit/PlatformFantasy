using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_2 : MonoBehaviour
{   // - coding version 2
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {   
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x); // Camera does not follow through the past screen
        transform.position = cameraPosition;
    }
}
