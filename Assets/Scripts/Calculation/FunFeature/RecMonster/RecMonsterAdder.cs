using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Calculation {
    public class RecMonsterAdder : MonoBehaviour {
        [SerializeField]
        private GameObject _RecMonsterNavmeshPrefab;
        public GameObject recMonster;

        private GameObject player, platform;
        private int roundNum, index, hardLevel;
        float speedIncrease = 1.05f;
        float recMonsterSpeed = 9;
        float recMonsterAccer = 30f;
        float recMonsterAngleSpeed = 60f;

        public void SetRecMonsterAdder(int roundNum, GameObject _RecMonsterNavmeshPrefab, int index) {
            this._RecMonsterNavmeshPrefab = _RecMonsterNavmeshPrefab;
            speedIncrease = 1.07f;
            hardLevel = roundNum / 10;
            if (hardLevel > 20) {
                hardLevel = 20;
            }
            recMonsterSpeed = 9 * Mathf.Pow(speedIncrease, hardLevel);
            recMonsterAccer = 30f * Mathf.Pow(speedIncrease, hardLevel);
            recMonsterAngleSpeed = 60f;
            this.roundNum = roundNum;
            this.index = index;
            gameObject.GetComponent<GameSetUp>().GameObjectInitializeDone += LinkToPlayer;
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        }

        private void GameManager_OnGameStateChanged(GameState gameState) {
            if(gameState == GameState.GameOn) {
                Vector3 position = GetMonsterPos();
                _RecMonsterNavmeshPrefab.GetComponent<NavMeshAgent>().speed = recMonsterSpeed;
                _RecMonsterNavmeshPrefab.GetComponent<NavMeshAgent>().acceleration = recMonsterAccer;
                _RecMonsterNavmeshPrefab.GetComponent<NavMeshAgent>().angularSpeed = recMonsterAngleSpeed;
                recMonster = Instantiate(_RecMonsterNavmeshPrefab, position, Quaternion.identity, transform);
                AIFollow aIFollow = recMonster.AddComponent<AIFollow>();
                aIFollow.SetAIFollow(player);
                RecMonsterFunction recMonsterFunction = recMonster.AddComponent<RecMonsterFunction>();
                RecMonsterAdvancedFunction recMonsterAdvancedFunction = recMonster.GetComponent<RecMonsterAdvancedFunction>();
                if (recMonsterAdvancedFunction != null) {
                    recMonsterAdvancedFunction.SetAdvanced(player);
                }
                if (hardLevel > 1) {
                    float scale = 1.1f * (hardLevel / 2);
                    recMonster.transform.localScale = new Vector3(scale, scale, scale);
                }
                
            }
        }

        private Vector3 GetMonsterPos() {
            if (roundNum > 8) {
                float distanceIncrease = 1.07f;
                float x = 18* Mathf.Pow(distanceIncrease, hardLevel);
                float z = 18 * Mathf.Pow(distanceIncrease, hardLevel);
                return new Vector3(x, 0, z);
            }
            return new Vector3(24, 0, 24);
        }

        private void LinkToPlayer(GameSetUp.GameArgs obj) {
            player = obj.player;
            platform = obj.platform;
        }

        

        private void OnDestroy() {
            gameObject.GetComponent<GameSetUp>().GameObjectInitializeDone -= LinkToPlayer;
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        }

    }
}
