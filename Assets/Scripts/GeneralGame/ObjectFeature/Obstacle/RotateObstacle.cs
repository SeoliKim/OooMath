using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacle : MonoBehaviour
{
    public float rotatingSpeed;
    public bool verticalRotation, horizantalRotation;
    private int xValue, yValue;

    // Start is called before the first frame update
    public void Awake() {
        if (verticalRotation) {
            xValue = 1;
        } else {
            xValue = 0;
        }

        if (horizantalRotation) {
            yValue = 1;
        } else {
            yValue = 0;
        }
    }
    public void SetRotateObstacle(float rotatingSpeed, bool verticalRotation, bool horizantalRotation ) {
        this.rotatingSpeed = rotatingSpeed;
        if (verticalRotation) {
            xValue = 1;
        } else {
            xValue = 0;
        }

        if (horizantalRotation) {
            yValue = 1;
        } else {
            yValue = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xValue * rotatingSpeed * Time.deltaTime, yValue * rotatingSpeed * Time.deltaTime, 0, Space.Self);
    }
}
