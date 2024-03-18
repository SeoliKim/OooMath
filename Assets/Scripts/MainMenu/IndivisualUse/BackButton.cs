using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _CloseTab;
    public void goBack() {
        _CloseTab.SetActive(false);
    }
}
