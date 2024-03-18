using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Calculation {
    public class AddCandyMonster : MonoBehaviour {

        /*
        public GameObject _CandyMonsterPrefab;

        public float secondTimer;
        public List<GameObject> candyMonsters;
        private bool timerOn;

        private GameObject gameManager;
        private RoundManager roundManager;

        private float candyMonsterSpeed;
        private float candyMonsterAccer;


        void Awake() {
            timerOn = false;
            eventsystem = GameObject.Find("EventSystem");
            gameLevelRunner = eventsystem.GetComponent<GameLevelRunner>();
            GameManager.OnGameStateChanged += OnGameStateChanged;
            gameLevelRunner.PassGameLevelInfo += GameLevelInitializeDone;
        }

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
            gameLevelRunner.PassGameLevelInfo -= GameLevelInitializeDone;
        }
        private void GameLevelInitializeDone(int r) {
            secondTimer = 6.0f;
            timerOn = true;
            Debug.Log("Second Timer started" + timerOn);
        }


        private void OnGameStateChanged(GameState state) {
            if (state != GameState.GameOn) {
                timerOn = false;
            }
        }

        private void Update() {
            if (timerOn) {
                secondTimer -= Time.deltaTime;
            }
            if (secondTimer < 0) {
                Debug.Log(" second past");
                GameObject candyMonster = Instantiate(_CandyMonsterPrefab) as GameObject;
                candyMonster.transform.position = candyMonster.GetComponent<RandomMovePoint>().GetRandomPoint();
                candyMonsters.Add(candyMonster);

                //update speed and acceleration
                candyMonsterSpeed = gameLevelRunner.CandyMonsterSpeed;
                candyMonsterAccer = gameLevelRunner.CandyMonsterAccer;
                candyMonster.GetComponent<NavMeshAgent>().speed = candyMonsterSpeed;
                candyMonster.GetComponent<NavMeshAgent>().acceleration = candyMonsterAccer;
                secondTimer = 6.0f;
            }
        }

        */
    }
}
