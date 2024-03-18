using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionController : MonoBehaviour
{

    // Update is called once per frame

    void Update()
    {
        Transform playerBall= this.transform.GetChild(1);
        this.transform.position = playerBall.position;

    }
}
