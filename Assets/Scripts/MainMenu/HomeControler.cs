using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeControler : MonoBehaviour
{
    [SerializeField] private GameObject _GamePanel;
    private void OnEnable() {
        _GamePanel.SetActive(false);
    }

    private void OnDisable() {
        _GamePanel.SetActive(true);
    }
}
