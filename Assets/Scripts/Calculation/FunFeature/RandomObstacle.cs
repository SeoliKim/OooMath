using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Calculation {
    public class RandomObstacle : MonoBehaviour {

        
        private GameObject[] _obstaclePrefabs = new GameObject[9];

        public List<GameObject> obstacles;

        private GameObject platform;
        private int hardLevel;

        private float timeInterval;
        private float timer;
        private bool timerOn;
        

        public List<GameObject> SetRandomObstacle(List<GameObject> obstacles, GameObject[] _obstaclePrehabs, int hardLevel) {
            this.obstacles = obstacles;
            this.hardLevel = hardLevel;
            HandleHardLevel();
            this._obstaclePrefabs = _obstaclePrehabs;
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
            gameObject.GetComponentInChildren<GameSetUp>().GameObjectInitializeDone += LinkToPlatform;
            return obstacles;
        }

        private void HandleHardLevel() {
            switch (hardLevel) {
                case 1:
                case 2:
                case 3:
                case 4:
                    timeInterval = 8;
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    timeInterval = 7;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                    timeInterval = 6;
                    break;
                case 13:
                case 14:
                case 15:
                case 16:
                    timeInterval = 5;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                    timeInterval = 4;
                    break;
                default:
                    timeInterval = 3;
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
                Debug.Log("times up! Add one obstacle");
                AddRandomObstacle();
                timeInterval = timer;
            }
        }

        private void AddRandomObstacle() {
            GameObject obstaclePrefab = GetObstacle();
            Vector3 surfaceposition = PlatformSurface.GetRandomPointOnPlatform(platform);
            Vector3 position = new Vector3(surfaceposition.x, 3, surfaceposition.z);
            GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity, transform) as GameObject;
            obstacle.transform.localScale = NumberGenerator.RandomScale(0.5f, 2.5f);
            obstacles.Add(obstacle);
        }

        private GameObject GetObstacle() {
            int randomLabel = NumberGenerator.getRandomNumber(0, 8);
            GameObject obstaclePrefab = _obstaclePrefabs[randomLabel];
            return obstaclePrefab;
        }

        private void LinkToPlatform(GameSetUp.GameArgs obj) {
            platform = obj.platform;
        }


        private void OnDestroy() {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        }
    }
}
