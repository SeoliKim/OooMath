using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMenuSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject _OpenPage, _TouchMenu, _HomeTouch, _GamePanel;

    public void OpenPage() {
        for (int i = 0; i < _TouchMenu.transform.childCount; i++) {
            GameObject child = _TouchMenu.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
        _GamePanel.SetActive(false);
        _HomeTouch.SetActive(true);
        _OpenPage.SetActive(true);
    }
}
