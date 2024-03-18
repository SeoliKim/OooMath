using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Number {
    public class LittleMeHCFunction : HelpCardFunction {

        [SerializeField] private TMP_Text _StarPtCost;
        [SerializeField] private GameObject _OooPrefab;

        //Link to GameBasic
        private GameObject player;

        //Function
        private bool onFunctionTime;
        private float existTimeInterval;
        private float timer;
        private GameObject littleMePrefab;
        private GameObject littleMe1;
        private GameObject littleMe2;

        private void Awake() {
            starPtCost = 16;
            _StarPtCost.text = starPtCost.ToString();
            existTimeInterval = 40f;
            littleMePrefab = Instantiate(_OooPrefab, helpCardManager.gameObject.transform);
            littleMePrefab.SetActive(false);
            littleMePrefab.transform.localScale = new Vector3(.8f, .8f, .8f);
        }

        private void Update() {
            if (onFunctionTime) {
                timer -= Time.deltaTime;
                if (timer < 0) {
                    DestroyLittleMe();
                    onFunctionTime = false;
                }
            }
        }


        public override void SetHelpCardFunction(HelpCardManager helpCardManager) {
            this.helpCardManager = helpCardManager;
            this.uIMessageReceiver = helpCardManager.uIMessageReceiver;
            player = helpCardManager.player;

        }

        protected override void StartFunction() {
            CraeteLittleMe();
            timer = existTimeInterval;
            onFunctionTime = true;
        }

        private void CraeteLittleMe() {
            Vector3 pos1 = new Vector3(player.transform.position.x - 1, player.transform.position.y, player.transform.position.z + 1);
            littleMe1 = Instantiate(littleMePrefab, pos1, Quaternion.identity, helpCardManager.gameObject.transform);
            Vector3 pos2 = new Vector3(player.transform.position.x + 1, player.transform.position.y, player.transform.position.z + 1);
            littleMe2 = Instantiate(littleMePrefab, pos2, Quaternion.identity, helpCardManager.gameObject.transform);
        }

        private void DestroyLittleMe() {
            Destroy(littleMe1);
            Destroy(littleMe2);
        }


    }
}
