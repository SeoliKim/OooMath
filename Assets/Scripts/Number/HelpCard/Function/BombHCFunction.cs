using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Number {
    public class BombHCFunction : HelpCardFunction {
        [SerializeField] private TMP_Text _StarPtCost;
        [SerializeField] private GameObject _NumberBombPrefab;

        //Link to GameBasic
        private GameObject player;

        //Function
        private GameObject bomb;
      
        private void Awake() {
            starPtCost = 8;
            _StarPtCost.text = starPtCost.ToString();
        }

        public override void SetHelpCardFunction(HelpCardManager helpCardManager) {
            this.helpCardManager = helpCardManager;
            this.uIMessageReceiver = helpCardManager.uIMessageReceiver;
            player = helpCardManager.player;
        }

        protected override void StartFunction() {
            CraeteBomb();
        }

        private void CraeteBomb() {
            bomb = Instantiate(_NumberBombPrefab, player.transform.position, Quaternion.identity, helpCardManager.gameObject.transform);
        }

    }
}
