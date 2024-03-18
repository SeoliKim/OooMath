using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class NullSetBombAdder : MonoBehaviour {

        private int hardLevel;
        private GameObject _NullSetBombPrefab;
        public List<GameObject> nullSetBombs;

        private GameObject platform;

        private float timeInterval;
        private float timer;
        private bool timerOn;

        public List<GameObject> SetNullSetBombAdder(GameObject _NullSetBombPrefab, int hardLevel) {
            this._NullSetBombPrefab = _NullSetBombPrefab;
            nullSetBombs = new List<GameObject>();
            this.hardLevel = hardLevel;
            HandleHardLevel();
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
            gameObject.GetComponentInChildren<GameSetUp>().GameObjectInitializeDone += LinkToPlatform;
            return nullSetBombs;

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
                    timeInterval = 7;
                    break;
                default:
                    timeInterval = 6;
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
                timer -= Time.deltaTime;
            }
            if (timer < 0) {
                Debug.Log("Add one Null Set Bomb");
                AddNullSetBomb();
                timer = timeInterval;
            }
        }

        private void AddNullSetBomb() {
            Vector3 surfaceposition = PlatformSurface.GetRandomPointOnPlatform(platform);
            Vector3 position = new Vector3(surfaceposition.x, 0.5f, surfaceposition.z);
            GameObject nullSetBomb = Instantiate(_NullSetBombPrefab, position, Quaternion.identity, transform);
            nullSetBombs.Add(nullSetBomb);
        }
        private void LinkToPlatform(GameSetUp.GameArgs obj) {
            platform = obj.platform;
            gameObject.GetComponentInChildren<GameSetUp>().GameObjectInitializeDone -= LinkToPlatform;
        }


        private void OnDestroy() {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        }
    }
}
