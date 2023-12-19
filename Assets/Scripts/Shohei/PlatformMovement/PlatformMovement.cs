using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform platform;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed = 0.3f;
       
    private int direction = 1;
    void Start()
    {
        
    }


    void Update()
    {
        Vector2 target = CurrentTarget();

        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector2)platform.position).magnitude;

        if (distance <= 0.1f)
        {
            direction *= -1;
        }

    }

    Vector2 CurrentTarget()
    {
        if (direction == 1)
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position;        
        }
    }

    private void OnDrawGizmos()
    {
        // this is for debug visualization
        if (platform != null && startPoint!=null && endPoint!=null)
        {
            Gizmos.DrawLine(platform.transform.position, startPoint.transform.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.transform.position);
        }

    }
}
