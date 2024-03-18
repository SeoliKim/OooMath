using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabOpener : MonoBehaviour
{
    [SerializeField]
    private GameObject _OpenTab, _CloseTab;

    public void OpenTab() {
        if(_OpenTab != null) {
            _OpenTab.SetActive(true);
        }
        if (_CloseTab!= null) {
            _CloseTab.SetActive(false);
        }
        
    }
}
