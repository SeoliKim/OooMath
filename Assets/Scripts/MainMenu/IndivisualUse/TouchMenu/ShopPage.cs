using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPage : MonoBehaviour
{
    [SerializeField] private GameObject _SkinPanel, _Skinwhiteshadow, _BrainPanel, _Brainwhiteshadow, _DiamondPanel, _Diamondwhiteshadow;
    private List<GameObject> whiteShadows = new List<GameObject>();
    private List<GameObject> panels = new List<GameObject>();

    private void Start() {
        whiteShadows.Add(_Brainwhiteshadow);
        whiteShadows.Add(_Diamondwhiteshadow);
        whiteShadows.Add(_Skinwhiteshadow);
        panels.Add(_SkinPanel);
        panels.Add(_BrainPanel);
        panels.Add(_DiamondPanel);
        OnSkinPanel();
    }


    public void OnSkinPanel() {
        ChangePanel(_SkinPanel, _Skinwhiteshadow);
    }

    public void OnBrainPanel() {
        ChangePanel(_BrainPanel, _Brainwhiteshadow);
    }

    public void OnDiamondPanel() {
        ChangePanel(_DiamondPanel, _Diamondwhiteshadow);
    }
    private void ChangePanel(GameObject panel, GameObject whiteShadow) {
        foreach (GameObject ws in whiteShadows) {
            ws.SetActive(true);
        }
        foreach (GameObject p in panels) {
            p.SetActive(false);
        }
        whiteShadow.SetActive(false);
        panel.SetActive(true);

    }
}
