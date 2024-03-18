using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class ClawFunction : MonoBehaviour {

        [SerializeField] public Animator _clawAnimator;


        public void GrabBallNum() {
            _clawAnimator.SetBool("GrabBallNum", true);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(PlayClawGrabReturn());
        }

        IEnumerator PlayClawGrabReturn() {
            yield return new WaitForEndOfFrame();
            float extendedNormalizedTime = _clawAnimator.GetFloat("ExtendTime");
            float returnTime = 1 - (extendedNormalizedTime - Mathf.Floor(extendedNormalizedTime));
            _clawAnimator.Play("ClawGrabReturn", 0, returnTime);
            Debug.Log("return time normalized " + extendedNormalizedTime);
        }

        public void ReleaseBallNum() {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

        }

        /*
        private void PlayerHitBallNum(PlayerMathGame.PlayerHitBallNumArgs eventArg) {
            if (GameManager.GameManagerInstance.State == GameState.GameOn) {
                if (eventArg.hitNumberLabel == x) {
                    GameManager.GameManagerInstance.UpdateGameState(GameState.LevelPass);
                    playerMathGame.PlayerHitBallNum -= PlayerHitBallNum;
                }
            }

        }
        */


    }
}
