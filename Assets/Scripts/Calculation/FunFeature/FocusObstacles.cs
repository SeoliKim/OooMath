using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class FocusObstacles : MonoBehaviour {
        
        private GameObject[] _obstaclePrefabs ;

        public List<GameObject> obstacles;

        private GameObject player;
        private GameObject platform;
        private int hardLevel;

        private float timeInterval;
        private float timer;
        private bool timerOn;

        public List<GameObject> SetFocusObstacle(List<GameObject> obstacles, GameObject[] _obstaclePrehabs, int hardLevel) {
            this.obstacles = obstacles;
            this._obstaclePrefabs = _obstaclePrehabs;
            this.hardLevel = hardLevel;
            HandleHardLevel();
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
            gameObject.GetComponentInChildren<GameSetUp>().GameObjectInitializeDone += LinkToGameObject;
            return obstacles;
        }

        private void HandleHardLevel() {
            switch (hardLevel) {
                case 1:
                case 2:
                case 3:
                case 4:
                    timeInterval = 7;
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    timeInterval = 6;
                    break;
                case 9:
                case 10:
                    timeInterval = 5;
                    break;
                case 11:
                case 12:
                    timeInterval = 4;
                    break;
                default:
                    timeInterval = 4;
                    break;
            }
            Invoke("GetReady", .1f);
        }
        private void GetReady() {
            timerOn = false;
            timer = this.timeInterval;
        }

        private void GameManager_OnGameStateChanged(GameState gameState) {
            if(gameState == GameState.GameOn) {
                timerOn = true;
            }
        }

        private void Update() {
            if (timerOn) {
                timeInterval -= Time.deltaTime;
            }
            if (timeInterval < 0) {
                Vector3 surfacePostion = GetFocusPoint(player.transform.position, player.GetComponent<Rigidbody>().velocity);
                AddFocusObstacle(surfacePostion);
                timeInterval = timer;
            }
        }
        private void AddFocusObstacle(Vector3 surfacePosition) {
            GameObject obstaclePrefab = GetObstacle();
            Vector3 position = new Vector3 (surfacePosition.x, 3, surfacePosition.z);
            
            GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity, transform) as GameObject;
            obstacle.transform.localScale = NumberGenerator.RandomScale(0.5f, 2f);
            obstacles.Add(obstacle);
        }

        private Vector3 GetFocusPoint(Vector3 playerPosition, Vector3 playervelocity) {
            Vector3 finalPosition = playerPosition + playervelocity * 0.04f;
            return finalPosition;
        }

        private GameObject GetObstacle() {
            int randomLabel = NumberGenerator.getRandomNumber(0, 8);
            GameObject obstaclePrefab = _obstaclePrefabs[randomLabel];
            if (obstaclePrefab != null) {
                GameObject obstacle = Instantiate(obstaclePrefab, transform);
                return obstacle;
            }
            return null;
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
