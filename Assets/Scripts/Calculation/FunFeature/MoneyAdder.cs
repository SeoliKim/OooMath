using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class MoneyAdder : MonoBehaviour {

        private GameObject _moneyPrefab;

        public List<GameObject> money = new List<GameObject>();

        private float timeInterval;
        private float timer;
        private bool timerOn;
        private GameObject platform;

        public List<GameObject> SetMoneyAdder(GameObject _moneyPrefab, float timer) {
            this._moneyPrefab = _moneyPrefab;
            this.timer = timer;
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
            gameObject.GetComponentInChildren<GameSetUp>().GameObjectInitializeDone += LinkToPlatform;
            return money;
        }

        private void GameManager_OnGameStateChanged(GameState gameState) {
            if (gameState == GameState.GameOn) {
                timerOn = true;
                timeInterval = timer;
            }
        }
        private void Update() {
            if (timerOn) {
                timeInterval -= Time.deltaTime;
            }
            if (timeInterval < 0) {
                Debug.Log("times up! Add money");
                AddMoney();
                timeInterval = timer;
            }
        }

        private void AddMoney() {
            Vector3 surfaceposition = PlatformSurface.GetRandomPointOnPlatform(platform);
            Vector3 position = new Vector3(surfaceposition.x, 0, surfaceposition.z);
            GameObject moneyInstance = Instantiate(_moneyPrefab, position, Quaternion.identity, transform) as GameObject;
            moneyInstance.name = _moneyPrefab.name;
            money.Add(moneyInstance);
        }

        private void LinkToPlatform(GameSetUp.GameArgs obj) {
            platform = obj.platform;
        }
    }
}
