using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject Player;

    void Update()
    {
        var position = transform.position;
        position.x = Player.transform.position.x;
        transform.position = position;
    }
}