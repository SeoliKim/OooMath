using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Calculation {
    public class SillyMonsterAdder : MonoBehaviour {

        private GameObject[] _sillyMonstersPrefab;
        public List<GameObject> sillyMonsters;
        public int countSillyMonster;
        public float sillyMonsterSpeed, sillyMonsterAccer;

        private GameObject platform;

        public void SetSillyMonsterAdder(GameObject[] _sillyMonstersPrefab, int count) {
            GameManager.OnGameStateChanged += OnGameStateChanged;
            gameObject.GetComponent<GameSetUp>().GameObjectInitializeDone += LinkToGameSetUp;
            sillyMonsters = new List<GameObject>();
            this._sillyMonstersPrefab = _sillyMonstersPrefab;
            countSillyMonster = count;
            GameSetUp gameSetUp = gameObject.GetComponent<GameSetUp>();
            this.sillyMonsterSpeed = gameSetUp.sillyMonsterSpeed;
            this.sillyMonsterAccer = gameSetUp.sillyMonsterAccer;
        }

        private void LinkToGameSetUp(GameSetUp.GameArgs obj) {
            platform = obj.platform;
            Invoke("AddSillyMonsters", .1f);
        }

        private void AddSillyMonsters() {
            for (int i = 0; i < countSillyMonster; i++) {
                Vector3 position = PlatformSurface.GetRandomPointOnPlatformAwayFromOrigin(platform);

                int rnum = NumberGenerator.getRandomNumber(0, 2);
                GameObject sillyMonster = Instantiate(_sillyMonstersPrefab[rnum], position, Quaternion.identity, transform) as GameObject;
                float scale = Random.Range(1f, 2f);
                sillyMonster.transform.localScale = new Vector3(scale, scale, scale);
                sillyMonsters.Add(sillyMonster);

                //stay no motion at first
                sillyMonster.GetComponent<NavMeshAgent>().speed = 0;
                sillyMonster.GetComponent<NavMeshAgent>().acceleration = 0;

                
            }
        }

        

        private void OnGameStateChanged(GameState gameState) {
            if (gameState == GameState.GameOn) {
                for (int i = 0; i < countSillyMonster; i++) {
                    AIMoveTo aIMoveTo = sillyMonsters[i].AddComponent<AIMoveTo>();
                    aIMoveTo.radius = 20;
                    sillyMonsters[i].GetComponent<NavMeshAgent>().speed = sillyMonsterSpeed;
                    sillyMonsters[i].GetComponent<NavMeshAgent>().acceleration = sillyMonsterAccer;
                }
            }
        }

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

    }
}
