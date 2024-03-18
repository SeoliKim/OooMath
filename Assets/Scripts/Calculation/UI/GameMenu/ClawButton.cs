using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Calculation {
    public class ClawButton : MonoBehaviour {
        [SerializeField] private GameObject _Shadow;
        private GameObject claw;

        private void Awake() {
            GameSetUp gameSetUp = gameObject.GetComponentInParent<UIMessageReceiver>().calculationGame.GetComponent<GameSetUp>();
            claw = gameSetUp.claw;
        }


    }
}
