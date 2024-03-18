using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorial0 : MonoBehaviour
{
    private bool onPauseMotion;
    private Vector3 pausePos;
    public void StartPauseMotion() {
        pausePos = gameObject.transform.position;
        onPauseMotion = true;
    }

    public void StopPauseMotion() {
        pausePos = Vector3.zero;
        onPauseMotion = false;
    }


    private void Update() {
        ReStore();
        OnPauseMotion();
    }

    private void ReStore() {
        if(gameObject.transform.position.y < -5) {
            gameObject.transform.position = Vector3.zero;
        }
    }

    private void OnPauseMotion() {
        if (onPauseMotion) {
            gameObject.transform.position = pausePos;
        } 
    }
}
