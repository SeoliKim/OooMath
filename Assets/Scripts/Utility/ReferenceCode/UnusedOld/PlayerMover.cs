using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    /*
    public float speed;
    public float vMax;
    public float cutOffSpeed;
    public float speedIncreaseRatio;
    public Rigidbody rb;

    private Vector3 savedAccel;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        speed = 10f;
        vMax = 1.5f;
        cutOffSpeed = 50f;
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        savedAccel = new Vector3();
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }
    private void GameManager_OnGameStateChanged(GameState state) {
        if(state == GameState.GameOn) {
            if(this != null) {
                transform.position = new Vector3(0f, 1f, 0f);
            }
        }
    }

    private void Start() {
        transform.position = new Vector3(0f, 1f, 0f);
        
}

    private void FixedUpdate() {

        var inputAccel = Input.acceleration;
        Vector3 accel = new Vector3();
        accel.x = inputAccel.x;
        accel.z = 0.9f + inputAccel.y;
        accel.y = 0;

        //RigAddForce(_rig, accel, 1, 0.15f, 5, ForceMode.Force);
        
        Vector3 force= RigAddForce(rb, accel, speed, vMax, cutOffSpeed, ForceMode.Acceleration);
        Debug.Log("input: " + inputAccel+ "saved: " + savedAccel + "accel" + accel+ " force:" + force);
        //savedAccel = inputAccel;

    }
    


    public static Vector3 RigAddForce(Rigidbody rig, Vector3 accel, float speed, float vMax, float cutOffSpeed, ForceMode forceMode) {
        // Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
        Vector3 forceAdd = new Vector3(accel.x, accel.y, accel.z);
        var rawInput = forceAdd;

        var v = rig.velocity;
        // var va = _rig.angularVelocity;
        // if (Mathf.Abs(rawInput.x) < Mathf.Abs(_lastInput.x)) {
        if (v.x * forceAdd.x < 0) {
            v.x = Mathf.Lerp(v.x, 0, Time.fixedDeltaTime * cutOffSpeed);
            // va.x = 0;
        }
        // if (Mathf.Abs(rawInput.z) < Mathf.Abs(_lastInput.z)) {
        if (v.z * forceAdd.z < 0) {
            v.z = Mathf.Lerp(v.z, 0, Time.fixedDeltaTime * cutOffSpeed);
            // va.z = 0;
        }
        rig.velocity = v;

        v = rig.velocity;
        if (Mathf.Abs(v.x) >= vMax && v.x * forceAdd.x > 0)
            forceAdd.x = 0;
        if (Mathf.Abs(v.z) >= vMax && v.z * forceAdd.z > 0)
            forceAdd.z = 0;

        // Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
        // multiplying it by 'speed' - our public player speed that appears in the inspector
        rig.AddForce(forceAdd * speed, forceMode);
        //Debug.Log("v:" + v);

        return rawInput;
    }
    */

}
