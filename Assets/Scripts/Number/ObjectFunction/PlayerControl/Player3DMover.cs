using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Number {
    public class Player3DMover : MonoBehaviour {
        public float vMax, constantRate, accelRate;

        public Rigidbody rb;
        public Transform _Rotator;

        bool dropping = true;
        private Quaternion calibrationQuaternion;

        private Vector3 dir;
        private Vector3 initialAccel;
        private Vector3 savedAccel;
        private Vector3 currentAccel;


        private void Awake() {
            rb = GetComponent<Rigidbody>();

            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;

        }

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        }

        private void GameManager_OnGameStateChanged(GameState state) {
            if (state == GameState.GameOn) {
                Debug.Log("player receive Game null");
            }
        }


        private void Update() {
            if (rb.velocity.y == 0 && dropping) {
                dropping = false; // only run once 
                calibrateAccelerometer();
                if (calibrationQuaternion != null) {
                    initialAccel = calibrationQuaternion * Input.acceleration;
                    savedAccel = initialAccel;
                    //Debug.Log("initial Accel: " + initialAccel);
                }
            }

            if (calibrationQuaternion != null) {
                currentAccel = calibrationQuaternion * Input.acceleration;
                //Debug.Log("currentAccel: " + currentAccel);
            }
        }

        private void FixedUpdate() {
            if (!dropping) {
                dir = _Rotator.forward;

                //add force
                bool effectiveforce = EffectiveForce();
                if (effectiveforce) {
                    var v = rb.velocity;
                    //verify velocity
                    var addForce = PlayerAccelwForce(rb, dir, currentAccel.y, accelRate, ForceMode.Impulse);

                    //Debug.Log("add force acceleration" + addForce);

                } else {
                    rb.AddForce(dir * constantRate, ForceMode.Impulse);
                    //Debug.Log("add constant force acceleration" + dir * constantRate);
                }
                setMaxVelocity();
                savedAccel = currentAccel;

            }
        }


        private bool EffectiveForce() {
            float fInput = currentAccel.y;
            float fNewChange = fInput - savedAccel.y;

            bool feffctiveMove = fNewChange * fInput > 0;
            bool finsideRange = Mathf.Abs(fNewChange) < 1;
            float factiveRange = 0.1f;
            bool fintendMove = Mathf.Abs(fInput) > factiveRange;
            //Debug.Log(" " + feffctiveMove + finsideRange + fintendMove);

            return feffctiveMove && finsideRange && fintendMove;
        }

        private void setMaxVelocity() {
            Vector3 v = rb.velocity;
            if (v.magnitude > vMax) {
                rb.velocity = v.normalized * vMax;
            }
        }


        /*
        private Vector3 PlayerDecelwV(Rigidbody rb, Vector3 direction, float inputChange, float decelRate) {
            var v = rb.velocity;
            v.z = Mathf.Lerp(v.z, 0, Time.fixedDeltaTime * decelRate * inputChange);
            v.x = Mathf.Lerp(v.x, 0, Time.fixedDeltaTime * decelRate * inputChange);
            rb.velocity = v;
            return rb.velocity;
        }
        */

        public Vector3 PlayerAccelwForce(Rigidbody rig, Vector3 direction, float inputChange, float accelRate, ForceMode forceMode) {
            // Combine direction with UserinputChange to get final applied force
            Vector3 forceAddAtDirection = direction * inputChange;
            // Add a physical force to our Player rigidbody using added force 
            rig.AddForce(forceAddAtDirection * accelRate, forceMode);

            return forceAddAtDirection * accelRate;
        }

        private void calibrateAccelerometer() {
            Vector3 originalInitialacceleration = Input.acceleration;
            Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), originalInitialacceleration);
            calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
        }
    }
}
