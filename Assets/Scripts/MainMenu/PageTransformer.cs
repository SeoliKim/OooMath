using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageTransformer : MonoBehaviour {

    [SerializeField]
    private GameObject _Close_Page, _Open_Page;

    public void changePage() {
        _Open_Page.SetActive(true);
        _Close_Page.SetActive(false);
    }
}
