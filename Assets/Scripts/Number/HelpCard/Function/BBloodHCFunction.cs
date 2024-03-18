using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Number {
    public class BBloodHCFunction : HelpCardFunction {

        [SerializeField] private TMP_Text _StarPtCost;

        //Link to GameBasic
        private GameObject player;

        private float bloodRecover;

        private void Awake() {
            starPtCost = 9;
            _StarPtCost.text = starPtCost.ToString();
            bloodRecover = 60f;
        }

        public override void SetHelpCardFunction(HelpCardManager helpCardManager) {
            this.helpCardManager = helpCardManager;
            this.uIMessageReceiver = helpCardManager.uIMessageReceiver;
            player = helpCardManager.player;

        }


        protected override void StartFunction() {
            PlayerNumberFunction playerNumberFunction = player.GetComponent<PlayerNumberFunction>();
            float bloodValue = playerNumberFunction.bloodValue;
            bloodValue += bloodRecover;
            if (bloodValue > 100) {
                bloodValue = 100;
            }

            playerNumberFunction.ChangeBloodValue(bloodValue);
        }

    }
}

