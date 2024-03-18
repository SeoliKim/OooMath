using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI1 : MonoBehaviour {

    [SerializeField] 
    private GameObject _GameTypeTab, _CalcGameTab;

    private void Awake() {
        _ArrowPlayDefaultPage.SetActive(false);
        _ArrowSelectCalcGame.SetActive(false);
        _ArrowCalcTutorial.SetActive(false);
    }

    [SerializeField] private GameObject _ArrowPlayDefaultPage;
    private RectTransform _ArrowPlayDefaultPageRect;
    
    public IEnumerator Step1() {
        _ArrowPlayDefaultPageRect = _ArrowPlayDefaultPage.GetComponent<RectTransform>();
        _ArrowPlayDefaultPage.SetActive(true);
        while (!_GameTypeTab.activeSelf) {
            if (_GameTypeTab.activeSelf) {
                break;
            }
            _ArrowPlayDefaultPageRect.anchoredPosition = new Vector3(-400, -150, 0);
            AudioManager.AudioManagerInstance.PlayAudio("pop-light");
            LeanTween.move(_ArrowPlayDefaultPageRect, new Vector3(-150f, -50, 0f), 1f);
            yield return new WaitForSeconds(1.2f);

        }
        yield return new WaitUntil(() => _GameTypeTab.activeSelf);
        _ArrowPlayDefaultPage.SetActive(false);
        StartCoroutine(Step2());
    }



    [SerializeField] private GameObject _ArrowSelectCalcGame;
    private RectTransform _ArrowSelectCalcGameRect;

    public IEnumerator Step2() {
        _ArrowSelectCalcGameRect = _ArrowSelectCalcGame.GetComponent<RectTransform>();
        _ArrowSelectCalcGame.SetActive(true);
        while (!_CalcGameTab.activeSelf) {
            if (_CalcGameTab.activeSelf) {
                break;
            }
            _ArrowSelectCalcGameRect.anchoredPosition = new Vector3(-400, -200, 0);
            AudioManager.AudioManagerInstance.PlayAudio("pop-light");
            LeanTween.move(_ArrowSelectCalcGameRect, new Vector3(-200, -100, 0), 1f);
            yield return new WaitForSeconds(1.2f);
           
        }
        yield return new WaitUntil(() => _CalcGameTab.activeSelf);
        _ArrowSelectCalcGame.SetActive(false);
        StartCoroutine(Step3());
    }


    [SerializeField] private GameObject _ArrowCalcTutorial;
    private RectTransform _ArrowCalcTutorialRect;

    public IEnumerator Step3() {
        _ArrowCalcTutorialRect = _ArrowCalcTutorial.GetComponent<RectTransform>();
        _ArrowCalcTutorial.SetActive(true);
        while (true) {
            _ArrowCalcTutorialRect.anchoredPosition = new Vector3(-200, -150, 0);
            AudioManager.AudioManagerInstance.PlayAudio("pop-light");
            LeanTween.move(_ArrowCalcTutorialRect, new Vector3(0, -50, 0), 1f);
            yield return new WaitForSeconds(1.2f);
            
        }
        yield return null;
    }
}
