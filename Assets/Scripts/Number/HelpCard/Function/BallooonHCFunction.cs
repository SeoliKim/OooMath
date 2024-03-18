using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Number {
    public class BallooonHCFunction : HelpCardFunction {

        [SerializeField] private TMP_Text _StarPtCost;

        //Link to GameBasic
        private GameObject player;

        //Function
        private bool onFunctionTime;
        private float existTimeInterval;
        private float timer;

        private void Awake() {
            starPtCost = 10;
            _StarPtCost.text = starPtCost.ToString();
            existTimeInterval = 12f;
        }

        public override void SetHelpCardFunction(HelpCardManager helpCardManager) {
            this.helpCardManager = helpCardManager;
            this.uIMessageReceiver = helpCardManager.uIMessageReceiver;
            player = helpCardManager.player;

        }

        private void Update() {
            if (onFunctionTime) {
                timer -= Time.deltaTime;
                PlayerWBalloon();
                if (timer < 0) {
                    onFunctionTime = false;
                }
            }
        }

        private void PlayerWBalloon() {
            Vector3 pos = new Vector3(player.transform.position.x, 10, player.transform.position.z);
            player.transform.position = pos;
        }

    }
}
