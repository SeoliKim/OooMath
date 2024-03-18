using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class MoneyGameFunction : MonoBehaviour
{
    protected float existTime;
    protected bool exist;
    protected float timer;

    
    protected virtual void Start() {
        existTime = 20f;
        exist = true;
        timer = existTime;
    }

    private void Update() {
        if (exist) {
           timer -= Time.deltaTime;
        }
        if (timer < 0) {
            //sDebug.Log("Ohno Money gone");
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(GetMoney());
        }
    }

    protected IEnumerator GetMoney() {
        AudioManager.AudioManagerInstance.PlayAudio("collectcoin");
        yield return null;
        Destroy(gameObject);

    }

}
