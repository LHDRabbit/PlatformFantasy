using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Range(0, 8)]
    public float speed;

    [Range(0, 4)]
    public float idlingTime;

    public Vector3 targetPosition;

    public GameObject movingLines;
    public Transform[] movingLinePoints;

    public int pointIndex;
    public int pointCount;
    int direction = 1;


    public int speedMultiplier = 1;

    private void Awake()
    {
        movingLinePoints = new Transform[movingLines.transform.childCount];
        for (int i = 0; i < movingLines.transform.childCount; i++)
        {
            movingLinePoints[i] = movingLines.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointCount = movingLinePoints.Length;
        pointIndex = 1;
        targetPosition = movingLinePoints[pointIndex].transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMultiplier*speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            NextPoint();
        }
    }

    public void NextPoint()
    {
        if (pointIndex == pointCount - 1)
        {
            direction = -1;
        }

        if (pointIndex == 0)
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPosition = movingLinePoints[pointIndex].transform.position;
        StartCoroutine(IdlingNextPoint());
    } 

    IEnumerator IdlingNextPoint()
    {
        speedMultiplier = 0;
        yield return new WaitForSeconds(idlingTime);
        speedMultiplier = 1;
    }
}
