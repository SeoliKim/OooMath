using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

namespace Calculation {
    public class ClawCoreGrabber : MonoBehaviour {

        [SerializeField] private UnityEvent PlayerGrabBallNum, PlayerReleaseBallNum;
        private GameObject grabBallNum;
        private GameObject calculationGame;
        private bool onGrab;

        private void LateUpdate() {
            if(onGrab && grabBallNum!= null) {
                grabBallNum.transform.localPosition = Vector3.zero;
                grabBallNum.transform.localRotation = Quaternion.Euler(new Vector3(-180, 0, 0));
            }
        }
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("BallNum")) {
                PlayerGrabBallNum.Invoke();
                Debug.Log("Claw Grab Ball Num");
                onGrab = true;
                grabBallNum = other.gameObject;
                calculationGame = grabBallNum.transform.parent.gameObject;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                BallNumAttachToClaw(grabBallNum);
            }
        }

        private void BallNumAttachToClaw(GameObject grabBallNum) {
            grabBallNum.GetComponent<NavMeshAgent>().enabled = false;
            grabBallNum.GetComponent<AIMoveTo>().enabled = false;
            grabBallNum.transform.SetParent(gameObject.transform);
            grabBallNum.transform.localPosition = Vector3.zero;
        }

        public void OnClaw_ClawButtonRelease() {
            Debug.Log("Claw receive release info from player's clawControler");
            onGrab = false;
            PlayerReleaseBallNum.Invoke();
            if (grabBallNum != null) {
                grabBallNum.transform.SetParent(calculationGame.transform);
                grabBallNum.GetComponent<NavMeshAgent>().enabled = true;
                grabBallNum.GetComponent<RandomMovePoint>().enabled = true;
                grabBallNum.GetComponent<AIMoveTo>().enabled = true;
            }
            gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject claw = gameObject.transform.parent.parent.parent.gameObject;
            if (claw.CompareTag("Claw")) {
                claw.SetActive(false);
            }
            
        }

        public void ReceiveBullNum() {
            if(grabBallNum != null) {
                gameObject.SendMessageUpwards("FromGrabber_ReceiveBallNum", grabBallNum);
            }
            
        }

        public void ClawStartToReturn() {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
