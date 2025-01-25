using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject Player;
    public float yThreshold;
    private float yMid;

    private void Start()
    {
        yMid = transform.position.y;
    }

    void Update()
    {
        var position = transform.position;
        position.x = Player.transform.position.x;
        var currentY = Player.transform.position.y;
        if (currentY < yMid - yThreshold)
        {
            yMid -= yMid - yThreshold - currentY;
            position.y = yMid;
        }
        else if (currentY > yMid + yThreshold)
        {
            yMid += currentY - yMid - yThreshold;
            position.y = yMid;
        }
        transform.position = position;
    }
}