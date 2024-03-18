using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class SlowShadowAdder : MonoBehaviour {

        private GameObject _slowShadowPrefab;
        private GameObject slowShadow;

        private GameObject player;
        private GameObject platform;
        private int hardLevel;
        private float timeInterval;
        private float timer;
        private bool timerOn;

        public void SetSlowShadow( GameObject _slowShadowPrefab, int hardLevel) {
            this._slowShadowPrefab = _slowShadowPrefab;
            this.hardLevel = hardLevel;
            HandleHardLevel();
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
            gameObject.GetComponentInChildren<GameSetUp>().GameObjectInitializeDone += LinkToGameObject;
        }

        private void HandleHardLevel() {
            switch (hardLevel) {
                case 1:
                case 2:
                case 3:
                case 4:
                    timeInterval = 10;
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    timeInterval = 9;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                    timeInterval = 8;
                    break;
                default:
                    timeInterval = 5;
                    break;
            }
            Invoke("GetReady", .1f);
        }
        private void GetReady() {
            timerOn = false;
            timer = this.timeInterval;
        }

        private void GameManager_OnGameStateChanged(GameState gameState) {
            if (gameState == GameState.GameOn) {
                timerOn = true;
            }
        }

        private void Update() {
            if (timerOn) {
                timeInterval -= Time.deltaTime;
            }
            if (timeInterval < 0) {
                Vector3 surfacePostion = GetFocusPoint(player.transform.position, player.GetComponent<Rigidbody>().velocity);
                AddSlowShadow(surfacePostion);
                timeInterval = timer;
            }
        }
        private void AddSlowShadow(Vector3 surfacePosition) {
            Vector3 position = new Vector3(surfacePosition.x, 0, surfacePosition.z);
            GameObject slowShadow = Instantiate(_slowShadowPrefab, position, Quaternion.identity, transform) as GameObject;
            slowShadow.GetComponent<SlowShadowFunction>().SetSlowShadow(player);
        }

        private Vector3 GetFocusPoint(Vector3 playerPosition, Vector3 playervelocity) {
            Vector3 finalPosition = playerPosition + playervelocity * 2.5f;
            return finalPosition;
        }

        private void LinkToGameObject(GameSetUp.GameArgs obj) {
            player = obj.player;
            platform = obj.platform;
        }


        private void OnDestroy() {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        }
    }
}
