using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialPage : MonoBehaviour
{
    [SerializeField] private GameObject _FriendPanel, _Friendwhiteshadow, _GroupPanel, _Groupwhiteshadow;
    private List<GameObject> whiteShadows = new List<GameObject>();
    private List<GameObject> panels = new List<GameObject>();

    private void Start() {
        whiteShadows.Add(_Friendwhiteshadow);
        whiteShadows.Add(_Groupwhiteshadow);
        panels.Add(_FriendPanel);
        panels.Add(_GroupPanel);
        OnFriendPanel();
    }

    public void OnFriendPanel() {
        ChangePanel(_FriendPanel, _Friendwhiteshadow);
    }

    public void OnGroupPanel() {
        ChangePanel(_GroupPanel, _Groupwhiteshadow);
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
