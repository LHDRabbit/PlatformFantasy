using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatingGround : MonoBehaviour
{
    public Transform groundElevator;
    private Vector2 velocity;
    public float speed = 1f;
    public float maxNumber = 8;
    public float minNumber = 3;
    private bool movingUp = true;
    private bool movingDown = false;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (movingUp && velocity.x < maxNumber)
        {
            MovingUp();
        }

    }

    public void MovingUp()
    {
        velocity.x = Mathf.Lerp(velocity.x, maxNumber, speed * Time.deltaTime);
    }

    public void MovingDown()
    {
        velocity.x = Mathf.Lerp(velocity.x, minNumber, speed * Time.deltaTime);
    }



    //public Transform groundElevator;
    //public float lerpValue;
    //public float duration = 3;

    //public float timeElapsed = 0;
    //public float start = 0;
    //public float end = 0;

    // Update is called once per frame
    //void Update()
    //{
    //    if (timeElapsed < duration)
    //    {
    //        float t = timeElapsed / duration;
    //        lerpValue = Mathf.Lerp(start, end, t);
    //        timeElapsed += Time.deltaTime;
    //    }
    //    else
    //    {
    //        lerpValue = end;
    //    }
    //}
}
