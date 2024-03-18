using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InternetPanel : MonoBehaviour
{
    public static InternetPanel instance;

    private void Awake() {
        instance = this;
    }
    public event Action ReconnectToInternet;
        public void TryAgain() {
        InternetManager.instance.CheckInternetConnection();

    }

    private IEnumerator CheckIfConnect() {
        yield return new WaitUntil(() => InternetManager.instance.onInternet == true);
        ReconnectToInternet?.Invoke();

    }
}
