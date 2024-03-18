using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace Calculation {
    public class FailMenu : MonoBehaviour {
        [SerializeField]
        private TMP_Text _questionNumText, _text_QLeft;
        [SerializeField]
        private GameObject _qTitile, _questionNum, _choiceButtonPanel, _menuButton;

        [Space]
        [Header("Goal Panel")]
        [SerializeField] private GameObject _GoalPanel;
        [SerializeField] private GameObject _RecordStamp, _ExperienceBar;
        private Image XPBarImage;
        private float initialXPBarFillAmount;
        [SerializeField] private TMP_Text _LvText, _WinStrikeNum;




        //Goal Record Info
        private int initialmaxQ, currentRound, initialXP, initialLv, winStrike;

        

        private void Awake() {
            XPBarImage = _ExperienceBar.GetComponent<Image>();
        }

        private void OnEnable() {
            AudioManager.AudioManagerInstance.StopAll();
            AudioManager.AudioManagerInstance.PlayMusic("failmusic");
            _qTitile.GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, 5f));
            _qTitile.GetComponent<RectTransform>().position = new Vector2(50, Screen.height);
            _questionNum.transform.localScale = Vector2.zero;
            _choiceButtonPanel.transform.localScale = Vector2.zero;
            _menuButton.transform.localScale = Vector2.zero;
            _RecordStamp.transform.localScale = Vector2.zero;
            _GoalPanel.GetComponent<RectTransform>().localPosition = new Vector2(-Screen.width, -160);
        }

        public void SetFailMenu(int initialmaxQ, int currentRound, int initialXP, int initialLv, int winStrike) {
            _questionNumText.text = currentRound.ToString();
            int leftQ = (currentRound / 4 + 1) * 4- currentRound;
            _text_QLeft.text = leftQ.ToString();
            if (leftQ == 4) {
                _text_QLeft.text = "0";
            }
            
            this.initialmaxQ = initialmaxQ;
            this.currentRound = currentRound;
            this.initialXP = initialXP;
            this.initialLv = initialLv;
            SetInitialXPUI();
            this.winStrike = winStrike;
            StartCoroutine(showFinalWindow());
        }

        private void SetInitialXPUI() {
            _LvText.text = initialLv.ToString();
            int preLvXP = 0;
            for (int i = 1; i < initialLv; i++) {
                preLvXP += (int)(90 * i + 10 * Mathf.Pow(i, 2));
            }
            float XPcurrent = initialXP - preLvXP;
            float XPtotal = 90 * initialLv + 10 * Mathf.Pow(initialLv, 2);
            initialXPBarFillAmount = XPcurrent / XPtotal;
            XPBarImage.fillAmount = initialXPBarFillAmount;

        }

        private IEnumerator showFinalWindow() {
            var seq = LeanTween.sequence();
            seq.append(() => { AudioManager.AudioManagerInstance.PlayAudio("slide-woosh-low"); });
            seq.append(LeanTween.moveY(_qTitile.GetComponent<RectTransform>(), -238, .8f).setEase(LeanTweenType.easeOutCirc));
            yield return new WaitForSeconds(1f);

            seq.append(() => { AudioManager.AudioManagerInstance.PlayAudio("levelComplete"); });
            seq.append(LeanTween.scale(_questionNum.GetComponent<RectTransform>(), Vector3.one * 1.2f, .8f).setEase(LeanTweenType.easeInOutExpo));
            seq.append(LeanTween.scale(_questionNum.GetComponent<RectTransform>(), Vector3.one, 0.6f).setEase(LeanTweenType.easeInSine));
            yield return new WaitForSeconds(1f);
            if (currentRound > initialmaxQ) {
                seq.append(LeanTween.scale(_RecordStamp.GetComponent<RectTransform>(), Vector3.one * 1.2f, .8f).setEase(LeanTweenType.easeInOutExpo));
                seq.append(() => {
                    AudioManager.AudioManagerInstance.PlayAudio("hit-impact-armor");
                    seq.append(LeanTween.scale(_RecordStamp.GetComponent<RectTransform>(), Vector3.one, 0.6f).setEase(LeanTweenType.easeInSine));
                });
            }

            yield return StartCoroutine(ShowGoalRecord());

            seq.append(() => {
                AudioManager.AudioManagerInstance.PlayAudio("buttonAppear");
                LeanTween.scale(_choiceButtonPanel.GetComponent<RectTransform>(), Vector3.one, 1.3f).setEase(LeanTweenType.easeOutQuart);
                LeanTween.scale(_menuButton.GetComponent<RectTransform>(), Vector3.one, 1.3f).setEase(LeanTweenType.easeOutQuart);
            });
        }

        private IEnumerator ShowGoalRecord() {
            var seq = LeanTween.sequence();
            seq.append(LeanTween.moveX(_GoalPanel.GetComponent<RectTransform>(), 250f, .8f).setEase(LeanTweenType.easeOutCirc));
            yield return new WaitForSeconds(1f);

            //Record Break
           
            yield return new WaitForSeconds(1.5f);

            //XP panel
            if (User.I.GetLv() > initialLv) {//Increase Lv
                seq.append(() => {
                    AudioManager.AudioManagerInstance.PlayAudio("small-win");
                    LeanTween.value(_ExperienceBar, initialXPBarFillAmount, 1f, 1f).setOnUpdate(UpdateValueFillAmount);
            });
                seq.append(1f);
                seq.append(() => { 
                    _LvText.text = User.I.GetLv().ToString();
                });
                float newXPBarFillAmount = UpdateNewXPUI();
                seq.append(() => {
                    AudioManager.AudioManagerInstance.PlayAudio("small-win");
                    LeanTween.value(_ExperienceBar, 0, newXPBarFillAmount, 1f).setOnUpdate(UpdateValueFillAmount);
                });
                seq.append(1f);
                
            } else {//in same Lv
                float newXPBarFillAmount = UpdateNewXPUI();
                seq.append(() => {
                    AudioManager.AudioManagerInstance.PlayAudio("small-win");
                    LeanTween.value(_ExperienceBar, initialXPBarFillAmount, newXPBarFillAmount, 1f).setOnUpdate(UpdateValueFillAmount);
                });
                seq.append(1f);
            }
            void UpdateValueFillAmount(float val) {
               XPBarImage.fillAmount= val;
            }

            //Win Strike
            seq.append(() => AudioManager.AudioManagerInstance.PlayAudio("slop-machine-win"));
            for (int i = 0; i <= winStrike; i++) {
                seq.append(() => { _WinStrikeNum.text = i.ToString(); });
                seq.append(.5f);
            }
            seq.append(() => AudioManager.AudioManagerInstance.Stop("slop-machine-win"));

        }
        

        private float UpdateNewXPUI() {
            int lv = User.I.GetLv();
            int experience = User.I.GetExperience();
            int preLvXP = 0;
            for (int i = 1; i < lv; i++) {
                preLvXP += (int)(90 * i + 10 * Mathf.Pow(i, 2));
            }
            float XPcurrent = experience - preLvXP;
            float XPtotal = 90 * lv + 10 * Mathf.Pow(lv, 2);
            return XPcurrent / XPtotal;
        }

    }
}
