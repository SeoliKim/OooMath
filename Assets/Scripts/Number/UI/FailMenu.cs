using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace Number {
    public class FailMenu : MonoBehaviour {
        [SerializeField]
        private TMP_Text _roundNumText, _killresultText;
        [SerializeField]
        private GameObject _rTitile, _roundNum, _retryButton, _menuButton, _GoalPanel;

        private UIMessageReceiver UIMessageReceiver;


        private void Awake() {
            UIMessageReceiver = transform.parent.gameObject.GetComponent<UIMessageReceiver>();
        }

        private void OnEnable() {
            AudioManager.AudioManagerInstance.Stop("theme");
            AudioManager.AudioManagerInstance.PlayMusic("failmusic");
            _roundNumText.text = UIMessageReceiver.round.ToString();
            _killresultText.text = UIMessageReceiver.killCount.ToString();
            _rTitile.GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, 5f));
            _rTitile.GetComponent<RectTransform>().position = new Vector2(50, Screen.height);
            _GoalPanel.transform.localScale = Vector2.zero;
            _roundNum.transform.localScale = Vector2.zero;
            _retryButton.transform.localScale = Vector2.zero;
            _menuButton.transform.localScale = Vector2.zero;
            showFinalWindow();
        }

        private void showFinalWindow() {
            var seq = LeanTween.sequence();
            seq.append(() => { AudioManager.AudioManagerInstance.PlayAudio("slide-woosh-low"); });
            seq.append(LeanTween.moveY(_rTitile.GetComponent<RectTransform>(), -238, .8f).setEase(LeanTweenType.easeOutCirc));
            seq.append(.8f);
            seq.append(() => { AudioManager.AudioManagerInstance.PlayAudio("levelComplete"); });
            seq.append(LeanTween.scale(_roundNum.GetComponent<RectTransform>(), Vector3.one * 1.2f, .8f).setEase(LeanTweenType.easeInOutExpo));
            seq.append(LeanTween.scale(_roundNum.GetComponent<RectTransform>(), Vector3.one, 0.6f).setEase(LeanTweenType.easeInSine));
            seq.append(LeanTween.scale(_GoalPanel.GetComponent<RectTransform>(), Vector3.one , .8f).setEase(LeanTweenType.easeInOutExpo));
            seq.append(1f);
            seq.append(() => {
                AudioManager.AudioManagerInstance.PlayAudio("buttonAppear");
                LeanTween.scale(_retryButton.GetComponent<RectTransform>(), Vector3.one, 1.3f).setEase(LeanTweenType.easeOutQuart);
                LeanTween.scale(_menuButton.GetComponent<RectTransform>(), Vector3.one, 1.3f).setEase(LeanTweenType.easeOutQuart);
            });

        }

    }
}
