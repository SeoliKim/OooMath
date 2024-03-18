using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUI : MonoBehaviour {
    [SerializeField] private float existTime;

    private void OnEnable() {
        StartCoroutine(TimeOn());
    }

    IEnumerator TimeOn() {
        yield return new WaitForSeconds(existTime);
        gameObject.SetActive(false);
    }
}
