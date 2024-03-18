using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Calculation {
    public class ClawControler : MonoBehaviour {
        // Start is called before the first frame update
        public event Action<GameObject> ReceiveBallNum;

        private GameObject claw;
        private UIEventSender UIEventSender;

        private bool freezePlayerRotation;
        private GameObject grabBallNum;
        private Transform cameraRotator;
        public void AssignCameraRotator(Transform rotator) {
            cameraRotator = rotator;
        }

        private void Awake() {
            Debug.Log("Claw Behavrior Start");
            UIEventSender = GameObject.Find("MainCanvas").GetComponent<UIEventSender>();
            freezePlayerRotation = false;
            UIEventSender.ClawButtonDown += ClawButtonDown;
            UIEventSender.ClawButtonRelease += ClawButtonRelease;
            //GameManager.OnGameStateChanged += OnGameStateChanged;
            claw = gameObject.transform.GetChild(2).gameObject;
            if (claw.name.CompareTo("claw")<0) {
                Debug.LogError("Claw Controler get wrong object");
            }
            
        }

        private void ClawButtonDown() {
            gameObject.transform.rotation = Quaternion.Euler(cameraRotator.forward);
            freezePlayerRotation = true;
            claw.SetActive(true);
            Animator clawAnimator = claw.GetComponent<Animator>();
            clawAnimator.enabled = true;
            clawAnimator.SetFloat("ExtendTime", 0);
            gameObject.BroadcastMessage("OnClaw_ClawButtonDown",SendMessageOptions.DontRequireReceiver);
        }


        private void LateUpdate() {
            if (freezePlayerRotation) {
                gameObject.transform.localEulerAngles = new Vector3(0, cameraRotator.localEulerAngles.y, 0);
                claw.transform.rotation = gameObject.transform.rotation;
                claw.transform.localPosition = Vector3.zero;
            }
        }

        private void ClawButtonRelease() {
            freezePlayerRotation = false;
            AudioManager.AudioManagerInstance.Stop("clawmove");
            gameObject.BroadcastMessage("OnClaw_ClawButtonRelease",SendMessageOptions.DontRequireReceiver);
        }  
        
        public void FromGrabber_ReceiveBallNum(GameObject grabBallNum) {
            this.grabBallNum = grabBallNum;
            freezePlayerRotation = false;
            ReceiveBallNum?.Invoke(grabBallNum);
        }

        private void OnDestroy() {
            UIEventSender.ClawButtonDown -= ClawButtonDown;
            UIEventSender.ClawButtonRelease -= ClawButtonRelease;
            //GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

    }
}
