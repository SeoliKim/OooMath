using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class PlayerAnimation : MonoBehaviour {

        [SerializeField] private Animator _OooAnimator;
        private GameObject player; 

        private void Awake() {
            player = transform.parent.gameObject;
        }

        public void GameFailResponse() {
            StartCoroutine(FailAnimation());
        }

        private IEnumerator FailAnimation() {
            yield return new WaitForSeconds(1f);
            AudioManager.AudioManagerInstance.PlayAudio("child-Ohoh");
            yield return new WaitForSeconds(1.2f);
            _OooAnimator.Play("OooTalk");
            yield return new WaitForSeconds(1f);
            GameManager.GameManagerInstance.UpdateGameState(GameState.Fail);
        }

        public void GamePassResponse() {
            player.GetComponent<Player3DMover>().enabled = false;
            StartCoroutine(PassAnimation());
        }

        private IEnumerator PassAnimation() {
            yield return new WaitForSeconds(1f);
            AudioManager.AudioManagerInstance.PlayAudio("child-yes");
            yield return new WaitForSeconds(1.2f);
            _OooAnimator.Play("OooTalk");
            yield return new WaitForSeconds(1f);
            GameManager.GameManagerInstance.UpdateGameState(GameState.LevelPass);
        }
    }
}
