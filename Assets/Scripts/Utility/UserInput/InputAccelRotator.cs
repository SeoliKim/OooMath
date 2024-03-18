using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAccelRotator : MonoBehaviour
{
    public float rotatespeed;
    private bool firstDone = false;
    private Quaternion calibrationQuaternion;

    private Vector3 savedAccel;
    private Vector3 currentAccel;

    
    private void FixedUpdate() {
        if (!firstDone) {
            calibrateAccelerometer();
            firstDone = true;
        }
        
        if (calibrationQuaternion != null) {
            currentAccel = calibrationQuaternion * Input.acceleration;
            //Debug.Log("currentAccel: " + currentAccel);
        }

        float rInput = currentAccel.x;
        bool effectiverotation = EffectiveRotation();
        if (effectiverotation) {
            var eulerChange = ObjectRotate(this.transform, rInput, rotatespeed);
            //Debug.Log("anglechange" + eulerChange);
        }
        
        savedAccel = currentAccel;
        
    }

    
    
    

    private bool EffectiveRotation() {
        float rInput = currentAccel.x;
        float rNewChange = rInput - savedAccel.x;
        //Debug.Log("relative change: " + rNewChange);
        //float rRelativeChange = rInput - initialAccel.x;

        bool reffctiveMove = rNewChange * rInput > 0;
        bool rinsideRange = Mathf.Abs(rNewChange) < 1;
        float ractiveRange = 0.005f;
        bool rintendMove = Mathf.Abs(rNewChange) > ractiveRange;
        //Debug.Log("check pass" + reffctiveMove + rinsideRange + rintendMove);
        return rinsideRange && reffctiveMove && rintendMove;
    }

    public static Quaternion ObjectRotate(Transform trans, float horizantalChange, float rotatespeed) {
        //Vector3 eulerChange = new Vector3(0, horizantalChange * 90, 0);
        //rotateHelper.Rotate(eulerChange, Space.World);
        Quaternion updateRotation = trans.rotation * Quaternion.AngleAxis(horizantalChange * 150, Vector3.up);
        trans.rotation = Quaternion.Slerp(trans.rotation, updateRotation, rotatespeed* Time.fixedDeltaTime);
        return trans.rotation;
    }

    private void calibrateAccelerometer() {
        Vector3 originalInitialacceleration = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), originalInitialacceleration);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }   
}
