using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class Calc10digitTutorialManager : MonoBehaviour {

        [SerializeField] private GameObject _TutorialUI;
        private bool startTutorial1;
        private void Awake() {
            int OnGoal = User.I.GetGoalOnNum();
            if (OnGoal < 0 || OnGoal >20) {
                _TutorialUI.SetActive(false);
                this.enabled = false;
            } else {
                _TutorialUI.SetActive(true);
                DisactiveTutorialChild();
            }
            if (OnGoal == 1) {
                if (User.I.IfTutorialOn(1)) {
                    startTutorial1 = true;
                }
                
            }

        }

        private void Start() {
            GameManager.OnGameStateChanged += OnGameStateChanged;

        }

        private void OnGameStateChanged(GameState state) {
            if(state == GameState.PreGame) {
                int OnGoal = User.I.GetGoalOnNum();
                int currentRound = User.I.GetCalcQuestion(GameManager.GameManagerInstance.gameIndex);
                if (OnGoal < 0 || currentRound > 0) {
                    _TutorialUI.SetActive(false);
                    this.enabled = false;
                }
            }
            if(state == GameState.GetReady) {
                if (startTutorial1) {
                    StartCoroutine(Step0());
                    Time.timeScale = 0;
                }
            }
            if(state == GameState.GameOn) {
                if (startTutorial1) {
                    StartCoroutine(Step2());
                }
            }

            if (state == GameState.LevelPass) {
                if (!User.I.CheckGoalDone(1)) {
                    StartCoroutine(Step5());
                }
            }
        }

        private void DisactiveTutorialChild() {
            for (int i = 0; i < _TutorialUI.transform.childCount; i++) {
                GameObject child = _TutorialUI.transform.GetChild(i).gameObject;
                child.SetActive(false);
            }
        }

        //0-Wanted Evil X poster
        private IEnumerator Step0() {
            AudioManager.AudioManagerInstance.PlayAudio("hit-impact-armor");
            AudioManager.AudioManagerInstance.PlayAudio("piano-piece-chill");
            _TutorialUI.transform.GetChild(0).gameObject.SetActive(true);
            yield return null;
        }

        //1-Goal Explained
        public void StartStep1() {
            AudioManager.AudioManagerInstance.StopAllSoundEffect();
            AudioManager.AudioManagerInstance.PlayAudio("levelSuccess");
            DisactiveTutorialChild();
            StartCoroutine(Step1());
        }

        private IEnumerator Step1() {
            _TutorialUI.transform.GetChild(1).gameObject.SetActive(true);
            yield return null;
        }
        public void FinishStep1() {
            DisactiveTutorialChild();
            Time.timeScale = 1;
        }
        //2-EquationShow
        private IEnumerator Step2() {
            _TutorialUI.transform.GetChild(2).gameObject.SetActive(true);
            Time.timeScale = 0;
            yield return null;
        }

        //3-ballNum Explained
        public void StartStep3() {
            AudioManager.AudioManagerInstance.PlayAudio("achievement-bell");
            DisactiveTutorialChild();
            StartCoroutine(Step3());
        }

        private IEnumerator Step3() {
            DisactiveTutorialChild();
            _TutorialUI.transform.GetChild(3).gameObject.SetActive(true);
            yield return null;
        }

        //4-claw explained
        public void StartStep4() {
            AudioManager.AudioManagerInstance.PlayAudio("achievement-bell");
            DisactiveTutorialChild();
            StartCoroutine(Step4());
        }

        private IEnumerator Step4() {
            DisactiveTutorialChild();
            _TutorialUI.transform.GetChild(4).gameObject.SetActive(true);
            yield return null;
        }

        public void FinishStep4() {
            DisactiveTutorialChild();
            startTutorial1 = false;
            User.I.FinishTutorial(1);
            Time.timeScale = 1;
        }

        //5- introduce Question Set
        private IEnumerator Step5() {
            Time.timeScale = 0;
            AudioManager.AudioManagerInstance.PlayAudio("achievement-bell");
            DisactiveTutorialChild();
            _TutorialUI.transform.GetChild(5).gameObject.SetActive(true);
            yield return null;
        }


        //6-complete basic tutorial

        public void StartStep6() {
            AudioManager.AudioManagerInstance.PlayAudio("video-game-win");
            DisactiveTutorialChild();
            StartCoroutine(Step6());
        }

        private IEnumerator Step6() {
            Time.timeScale = 0;
            DisactiveTutorialChild();
            _TutorialUI.transform.GetChild(6).gameObject.SetActive(true);
            yield return null;
        }

        public void FinishStep6() {
            DisactiveTutorialChild();
            startTutorial1 = false;
            User.I.FinishGoal(1);
            Time.timeScale = 1;
        }
    }
}
