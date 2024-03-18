using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotater : MonoBehaviour
{
    /*
    public float speed;
    public float cutOffSpeed;
    public float vMax = 2f;
    public float vMin = 0.5f;
    public float speedIncreaseRatio;
    public Rigidbody rb;

    private Transform rotateHelper;

    bool dropping = true;
    private Quaternion calibrationQuaternion;

    private Vector3 initialAccel;
    private Vector3 savedAccel;
    private Vector3 currentAccel;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
        rotateHelper = this.transform.parent.transform.GetChild(1);

        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;

    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState state) {
        if (state == GameState.GameOn) {
            if (this != null) {
                transform.position = new Vector3(0f, 1f, 0f);
            }
        }
    }

    private void Start() {
        transform.position = new Vector3(0f, 1f, 0f);
    }

    private void Update() {
        if (rb.velocity.y == 0 && dropping) { 
            dropping = false; // only run once 
            calibrateAccelerometer();
            if (calibrationQuaternion != null) {
                initialAccel = calibrationQuaternion * Input.acceleration;
                savedAccel = initialAccel;
                Debug.Log("initial Accel: " + initialAccel);
            }
        }

        if(calibrationQuaternion != null) {
            currentAccel = calibrationQuaternion * Input.acceleration;
            //Debug.Log("currentAccel: " + currentAccel);
        }
    } 

    private void FixedUpdate() {
        if (!dropping) {
            //update rotation
            float rInput = currentAccel.x;
            float rotatespeed = 1f;
            bool effectiverotation = EffectiveRotation();
            if (effectiverotation) {
                var eulerChange = PlayerRotate(rotateHelper, rInput, rotatespeed);
                //Debug.Log("anglechange" + eulerChange);
            }
            Vector3 dir = rotateHelper.forward;
            Debug.Log("direction" + dir);


            //add force
            float fInput = currentAccel.y - savedAccel.y;
            float inputRate = 1;
            float accelRate = 10;
            bool effectiveforce = EffectiveForce(dir);
            if (effectiveforce) {
                var addForce = PlayerAccelwForce(rb, dir, fInput, accelRate, ForceMode.Acceleration);

                Debug.Log("add force acceleration" + addForce);

            } else {
                rb.AddForce(dir * inputRate, ForceMode.Acceleration);
                Debug.Log("add constant force acceleration" + dir * inputRate);
            }

            savedAccel = currentAccel;

        }
    }

    private bool EffectiveRotation() {
        float rInput = currentAccel.x;
        float rNewChange = rInput - savedAccel.x;
        float rRelativeChange = rInput - initialAccel.x;

        bool reffctiveMove = rNewChange * rRelativeChange > 0;
        bool rinsideRange = Mathf.Abs(rNewChange) < 1;
        float ractiveRange = 0.1f;
        bool rintendMove = Mathf.Abs(rInput) > ractiveRange; 
        return rinsideRange && rintendMove && reffctiveMove;
    }

    public static Quaternion PlayerRotate(Transform trans, float horizantalChange, float rotatespeed) {
        //Vector3 eulerChange = new Vector3(0, horizantalChange * 90, 0);
        //rotateHelper.Rotate(eulerChange, Space.World);
        Quaternion updateRotation =  trans.rotation * Quaternion.AngleAxis(horizantalChange * 90, Vector3.up);
        trans.rotation= Quaternion.Lerp(trans.rotation, updateRotation, rotatespeed * Time.deltaTime);
        return trans.rotation;
    }

    private bool EffectiveForce(Vector3 direction) {
        float fInput = currentAccel.y;
        float fNewChange = fInput - savedAccel.y;
        float fRelativeChange = fInput - initialAccel.y;

        bool feffctiveMove = fNewChange * fRelativeChange > 0;
        bool finsideRange = Mathf.Abs(fNewChange) < 1;
        float factiveRange = 0.1f;
        bool fintendMove = Mathf.Abs(fInput) > factiveRange;

        //check vMax and vMin
        var v = rb.velocity;
        bool vCheck = true;
        if ((Mathf.Abs(v.z) >= vMax && v.z * direction.z > 0) || (Mathf.Abs(v.z) <= vMin && fInput < 0)) {
            direction.z = 0;
            vCheck = false;
        }
        if ((Mathf.Abs(v.x) >= vMax && v.x * direction.x > 0) || (Mathf.Abs(v.x) <= vMin && fInput < 0)) {
            direction.x = 0;
            vCheck = false;
        }
        Debug.Log("vCheck : " + vCheck + "velocity" + v);

        return feffctiveMove && finsideRange && fintendMove && vCheck;
    }

    /*
    private Vector3 PlayerDecelwV(Rigidbody rb, Vector3 direction, float inputChange, float decelRate) {
        var v = rb.velocity;
        v.z = Mathf.Lerp(v.z, 0, Time.fixedDeltaTime * decelRate * inputChange);
        v.x = Mathf.Lerp(v.x, 0, Time.fixedDeltaTime * decelRate * inputChange);
        rb.velocity = v;
        return rb.velocity;
    }
    

    public Vector3 PlayerAccelwForce(Rigidbody rig, Vector3 direction, float inputChange, float accelRate, ForceMode forceMode) {
        // Combine direction with rate to get final applied force
        Vector3 forceAddAtDirection = direction * inputChange;
        // Add a physical force to our Player rigidbody using added force 
        rig.AddForce(forceAddAtDirection * accelRate, forceMode);

        return forceAddAtDirection* accelRate;
    }

    // find the quaternion that used to calibrate into our initial input of accelerometer, times calibrationQuaternion with input v3 to get calibrated one
    private void calibrateAccelerometer() {
        Vector3 originalInitialacceleration = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), originalInitialacceleration);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    */
}


