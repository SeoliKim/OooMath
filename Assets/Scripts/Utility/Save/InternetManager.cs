using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.Collections.Generic;

public class InternetManager : MonoBehaviour{
    #region singleton
    public static InternetManager instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
    }

    private void Start() {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        GameObject mainCanvas = GameObject.Find("MainCanvas");
        InternetPanel = Instantiate(_InternaetPanelPrefab, mainCanvas.transform);
        InternetPanel.SetActive(false);
    }

    #endregion

    [SerializeField] private GameObject _InternaetPanelPrefab;
    private GameObject InternetPanel;

    bool onRunning = false;
    public bool onInternet = false;
    private void ChangedActiveScene(Scene current, Scene next) {
        GameObject mainCanvas = GameObject.Find("MainCanvas");
        InternetPanel = Instantiate(_InternaetPanelPrefab, mainCanvas.transform);
        InternetPanel.SetActive(false);
    }

    public void CheckInternetConnection() {
        if (onRunning) {
            StartCoroutine(AlreadyRunning(b => {
                bool isConnected = b;
            }));
        } else {
            StartCoroutine(ConnectInternet(b => {
                bool isConnected = b;
                StartCoroutine(ShowInternetPanel(isConnected));
            }));
        }
    }

    private IEnumerator AlreadyRunning(Action<bool> action) {
        yield return new WaitUntil(() => onRunning == false);
        action(onInternet);
    }


    IEnumerator ConnectInternet(Action<bool> action) {
        onRunning = true;
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        Debug.Log(request);
        yield return request.SendWebRequest();
        if (request.error != null) {
            onInternet = false;
            action(false);
        } else {
            onInternet = true;
            action(true);
        }
        onRunning = false;
    }

    private IEnumerator ShowInternetPanel(bool check) {
        Debug.Log("show internet panel" + check);
        if (check) {
            InternetPanel.SetActive(false);
            Debug.Log("InternetPanel Set disactive");
            yield break;
        }
        yield return new WaitUntil(() => InternetPanel != null);
        InternetPanel.SetActive(true);
        Debug.Log("InternetPanel Set active");
    }
}
