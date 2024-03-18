using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string _goal;
    private GameObject goal;
    [SerializeField] private float x, y, z, ax, ay, az;
    private void Awake() {
        goal = GameObject.Find(_goal);
        this.transform.rotation = Quaternion.Euler(ax, ay, az);
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if(goal != null) {
            transform.position = goal.transform.position + new Vector3(x, y, z);
            
        }
    }
}
