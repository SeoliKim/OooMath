using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject _Background;

    // Start is called before the first frame update
    private void Awake() {
        gameObject.SetActive(false);
        for (int i = 0; i < _Background.transform.childCount; i++) {
            GameObject child = _Background.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
    }
    void Start()
    {
        int i = NumberGenerator.getRandomNumber(0, 4);
        _Background.transform.GetChild(i).gameObject.SetActive(true);
    }

   
}
