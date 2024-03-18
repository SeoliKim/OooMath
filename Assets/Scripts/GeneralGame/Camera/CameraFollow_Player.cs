using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_Player : CameraFollow
{
    private Transform player;
    [SerializeField] private float x, y, z, ax, ay, az;

    private void Awake() {
        player = GameObject.FindGameObjectsWithTag("player")[0].transform;
        x = 0;
        y = 2.5f;
        z = -4;
        ax = 30;
        ay = 0;
        az = 0;
        this.transform.rotation = Quaternion.Euler(ax, ay, az);
        
    }


    void Update() {
        if (player != null) {
            transform.position = player.transform.position + new Vector3(x, y, z);
        }
    }
}
