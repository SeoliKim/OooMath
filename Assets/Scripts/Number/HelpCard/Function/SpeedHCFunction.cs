using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Number {
    public class SpeedHCFunction : HelpCardFunction {

        [SerializeField] private TMP_Text _StarPtCost;

        //Link to GameBasic
        private GameObject player;
        private Rigidbody prb;

        //Function
        private bool onFunctionTime;
        private float speedTimeInterval;
        private float timer; 

        private void Awake() {
            starPtCost = 7;
            _StarPtCost.text = starPtCost.ToString();
            speedTimeInterval = 7f;
            prb = player.GetComponent<Rigidbody>();
        }

        public override void SetHelpCardFunction(HelpCardManager helpCardManager) {
            this.helpCardManager = helpCardManager;
            this.uIMessageReceiver = helpCardManager.uIMessageReceiver;
            player = helpCardManager.player;

        }

        private void Update() {
            if (onFunctionTime) {
                timer -= Time.deltaTime;
                SpeedUpPalyer();
                if (timer < 0) {
                    onFunctionTime = false;
                }
            }

        }
        

        protected override void StartFunction() {
            timer = speedTimeInterval;
            onFunctionTime = true;
        }

        private void SpeedUpPalyer() {
            player.GetComponent<Rigidbody>().velocity *= 1.5f;
        }
    }
}
