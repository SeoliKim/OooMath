using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Calculation {
    public class GetReadyMenu : MonoBehaviour {
        [SerializeField]
        private TMP_Text _roundNum;
        [SerializeField]
        private GameObject _Countdown3, _Countdown2, _Countdown1;

        
        private UIMessageReceiver UIMessageReceiver;


        private void GetMessageValue() {
            _roundNum.text = UIMessageReceiver.roundCount.ToString();
            
        }


        void OnEnable() {
            StartCoroutine(SetDefault());
        }

        IEnumerator SetDefault() {
            this.GetComponent<RectTransform>().localRotation = Quaternion.identity;
            this.GetComponent<RectTransform>().localPosition = new Vector2(-Screen.width * 2, 50);
            UtilityColor.setAlphaLevelUI(_Countdown1, 1, 0, .1f);
            UtilityColor.setAlphaLevelUI(_Countdown2, 1, 0, .1f);
            UtilityColor.setAlphaLevelUI(_Countdown3, 1, 0, .1f);
            UIMessageReceiver = transform.parent.gameObject.GetComponent<UIMessageReceiver>();
            yield return new WaitUntil(()=> UIMessageReceiver.gameSetUp);
            GetMessageValue();
            StartGetReadyMenu();
        }

        private void StartGetReadyMenu() {
            LeanTween.moveX(this.GetComponent<RectTransform>(), 0, .8f).setEase(LeanTweenType.easeOutCirc);
            AudioManager.AudioManagerInstance.PlayAudio("slide-woosh-low");
            StartCoroutine(countDown());
        }


        IEnumerator countDown() {
            int timeCountDown = 3;
            while (timeCountDown > 0) {
                yield return new WaitForSeconds(1f);
                if (timeCountDown == 3) {
                    AudioManager.AudioManagerInstance.PlayAudio("beep-short");
                    lightCountDown(_Countdown3);
                }
                if (timeCountDown == 2) {
                    AudioManager.AudioManagerInstance.PlayAudio("beep-short");
                    lightCountDown(_Countdown2);
                }
                if (timeCountDown == 1) {
                    AudioManager.AudioManagerInstance.PlayAudio("beep-1-sec");
                    lightCountDown(_Countdown1);
                }
                timeCountDown--;
            }
            yield return new WaitForSeconds(.5f);
            rotateDown();
        }

        private void lightCountDown(GameObject cdCircle) {
            LeanTween.alpha(cdCircle.GetComponent<RectTransform>(), 1f, 0.5f).setEase(LeanTweenType.easeOutQuint).setLoopPingPong(1);

        }

        private void rotateDown() {
            LeanTween.rotate(this.GetComponent<RectTransform>(), -90.0f, .8f).setEase(LeanTweenType.easeInBack).setOnComplete(startGameOn);
        }

        private void startGameOn() {
            GameManager.GameManagerInstance.UpdateGameState(GameState.GameOn);
            Debug.Log("Gamestate is now GameOn");
        }

        
    }
}
