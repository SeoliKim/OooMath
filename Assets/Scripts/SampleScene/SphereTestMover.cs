using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTestMover : MonoBehaviour
{
    public float speed;
    public float vMax;
    public float cutOffSpeed;
    public float speedIncreaseRatio;
    public Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        speed = 2f;
        vMax = 10f;
        cutOffSpeed = 200f;
    }

    void FixedUpdate() {
        if (Input.GetKey("w")) {
            Vector3 addForce = RigAddForce(rb, new Vector3(0, 0, 1), speed, vMax, cutOffSpeed, ForceMode.Force);
            rb.AddForce(addForce, ForceMode.Force);
        }
        if (Input.GetKey("a")) {
            Vector3 addForce = RigAddForce(rb, new Vector3(-1, 0, 0), speed, vMax, cutOffSpeed, ForceMode.Force);
            rb.AddForce(addForce, ForceMode.Force);
        }
        if (Input.GetKey("s")) {
            Vector3 addForce = RigAddForce(rb, new Vector3(0, 0, -1), speed, vMax, cutOffSpeed, ForceMode.Force);
            rb.AddForce(addForce, ForceMode.Force);
        }
        if (Input.GetKey("d")) {
            Vector3 addForce = RigAddForce(rb, new Vector3(1, 0, 0), speed, vMax, cutOffSpeed, ForceMode.Force);
            rb.AddForce(addForce, ForceMode.Force);
        }
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
        //rig.AddForce(forceAdd * speed, forceMode);
        return forceAdd * speed;
        //return rawInput;
    }
}
