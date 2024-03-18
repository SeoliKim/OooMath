using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Calculation {
    public class PassMenu : MonoBehaviour {
        [SerializeField]
        private GameObject _WindowTextGOOD, _WindowTextJOB;

        void OnEnable() {
            //RoundManager.GameDestroyed += GameDestroyed;
            Debug.Log("Pass Menur ready enable, ready to listen, Game Destroyed");
            this.GetComponent<RectTransform>().localRotation = Quaternion.identity;
            this.GetComponent<RectTransform>().localPosition = new Vector2(-Screen.width * 2, 0);
            _WindowTextGOOD.transform.localScale = Vector2.zero;
            _WindowTextJOB.transform.localScale = Vector2.zero;
            MovePassWindow();
        }

        /*
        private void GameDestroyed() {
            MovePassWindow();
        }
        */

        private void MovePassWindow() {
            var seq = LeanTween.sequence();
            seq.append(.5f);
            seq.append(() => { AudioManager.AudioManagerInstance.PlayAudio("slide-woosh-low"); });
            seq.append(LeanTween.moveX(this.GetComponent<RectTransform>(), 0, .8f).setEase(LeanTweenType.easeOutCirc));
            seq.append(() => { AudioManager.AudioManagerInstance.PlayAudio("Wowvoice", .8f); });
            seq.append(LeanTween.scale(_WindowTextGOOD.GetComponent<RectTransform>(), Vector3.one, 0.5f).setEase(LeanTweenType.easeInExpo));
            seq.append(() => { AudioManager.AudioManagerInstance.PlayAudio("levelSuccess", .8f); });
            seq.append(LeanTween.scale(_WindowTextJOB.GetComponent<RectTransform>(), Vector3.one, 0.5f).setEase(LeanTweenType.easeInExpo));
            seq.append(.3f);
            seq.append(LeanTween.rotate(this.GetComponent<RectTransform>(), 90.0f, 1f).setEase(LeanTweenType.easeInBack).setOnComplete(startGetReady));
        }

        private void startGetReady() {
            GameManager.GameManagerInstance.UpdateGameState(GameState.GetReady);
            Debug.Log("Gamestate is now Get Ready");
        }

        private void OnDisable() {
            //RoundManager.GameDestroyed -= GameDestroyed;
        }
    }
}
