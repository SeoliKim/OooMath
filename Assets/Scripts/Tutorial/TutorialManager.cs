using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject _TutorialUI, _MainCanvas;
    private MainMenuManager mainMenuManager;
    private void Awake() {
        _TutorialUI.SetActive(true);
        for (int i = 0; i < _TutorialUI.transform.childCount; i++) {
            GameObject child = _TutorialUI.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
        mainMenuManager = _MainCanvas.GetComponent<MainMenuManager>();
    }

    private void Start() {
        int OnGoal = User.I.GetGoalOnNum();
        if (OnGoal < 0) {
            this.enabled = false;
        } else if (OnGoal == 0) {
            OnTutorial0();
        } else if (OnGoal == 1) {
            OnTutorial1();
        }
    }
    #region Tutorial 0- Welcome
    private void OnTutorial0() {
        for (int i = 0; i < _MainCanvas.transform.childCount; i++) {
            GameObject child = _MainCanvas.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
        _TutorialUI.SetActive(true);
        GameObject totorialUI0 = _TutorialUI.transform.GetChild(0).gameObject;
        totorialUI0.SetActive(true);
        totorialUI0.GetComponent<TutorialUI0>().SetTutorialUI0();
    }
    #endregion

    #region Tutorial 1- Start Calculation Game In Main Menu

    [Header("Tutorial 1")]
    [SerializeField] private GameObject _TutorialUI1;
    private TutorialUI1 tutorialUI1;
    private void OnTutorial1() {
        mainMenuManager.SetDefaultGameMenu();
        mainMenuManager.SetDefaultTouchMenu();
        _TutorialUI1.SetActive(true);
        for (int i = 0; i < _TutorialUI1.transform.childCount; i++) {
            GameObject child = _TutorialUI1.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
        tutorialUI1 = _TutorialUI1.GetComponent<TutorialUI1>();
        StartCoroutine(tutorialUI1.Step1());
       
    }
    #endregion

}
