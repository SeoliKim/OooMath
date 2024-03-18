using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialUI0 : MonoBehaviour
{
    public void SetTutorialUI0() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void Start() {
        AudioManager.AudioManagerInstance.StopAll();
        AudioManager.AudioManagerInstance.PlayMusic("WelcomeMusic");
    }
    public void StartTutorial0Scene() {
        User.I.StartTutorial(0);
        SceneLoader.instance.LoadScene(2);
    }

    public void SkipTutorial() {
        User.I.FinishGoal(0);
        User.I.FinishGoal(1);
        SceneLoader.instance.LoadScene(1);
    }
}
