using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTutorial0 : MoneyGameFunction {

    
    protected override void Start() {
        existTime = 25f;
        exist = true;
        timer = existTime;
    }

    protected override void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(GetMoney());
            SendMessageUpwards("ChipsAddOne");
        }
    }
}
