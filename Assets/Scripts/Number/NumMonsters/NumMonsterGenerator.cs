using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using TMPro;

namespace Number {
    public class NumMonsterGenerator : MonoBehaviour {

        #region Field

        public List<GameObject> numMonsters = new List<GameObject>();

        [Header("NumMonster Prefab")]
        [SerializeField] private GameObject _BasicNMNavmeshPrefab;
         [SerializeField] private GameObject _BigNMNavmeshPrefab, _FastNMNavmeshPrefab, _SuperNMNavmeshPrefab;

        //NumMonsterAdder
        public List<NumMonsterAdder> numMonsterAdders = new List<NumMonsterAdder>();
        public BasicNumMonsterAdder basicNumMonsterAdder;
        public BigNMAdder bigNMAdder;
        public FastNMAdder fastNMAdder;
        

        [Header("Factory Component")]
        [SerializeField] private GameObject _FactoryDoor;
        [SerializeField] private GameObject _CountDownClock;
        [SerializeField] private TMP_Text _CountText;
        private bool monsterComing;
        public readonly Vector3 factoryMouthPos = new Vector3(0, 0, -25);
        private readonly Vector3 monsterMouthOpenPos = new Vector3(0, 0.2f, 0);

        [Space]
        //Assign in SetUp
        public GameObject player;

        public GameObject gameManager;
        public NumberType_Level numberType_Level;
        public GameSetUp gameSetUp;
        public NumMonsterManager numMonsterManager;


        public int waveNum, round;
        [SerializeField] private float timeInterval;
        private float secondTimer;
        private bool timerOn;
        public void StartTimer(bool b) {
            timerOn = b;
        }

        public event Action<int> GenerateNewWave;
        public event Action<int> FinishCurrentRound;

        #endregion

        private void Start() {
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
            gameManager = GameManager.GameManagerInstance.gameObject;
            numberType_Level = gameManager.GetComponentInChildren<NumberType_Level>();
            numMonsterManager = gameManager.GetComponentInChildren<NumMonsterManager>();
            gameSetUp = gameManager.GetComponentInChildren<GameSetUp>();
            gameSetUp.GameObjectInitializeDone += LinkToObject;

            timeInterval = 0f;
            secondTimer = timeInterval;
            round = User.I.GetNumMonsterRound(GameManager.GameManagerInstance.gameIndex) +1;
            waveNum = (round-1)*5+1;
            monsterComing = false;

            _FactoryDoor.transform.localPosition = Vector3.zero;
            SetMonsterAdder();
        }

        private void SetMonsterAdder() {
            basicNumMonsterAdder = gameObject.AddComponent<BasicNumMonsterAdder>();
            basicNumMonsterAdder.SetNumMonsterAdder(_BasicNMNavmeshPrefab, numMonsters, this);
            numMonsterAdders.Add(basicNumMonsterAdder);
            bigNMAdder = gameObject.AddComponent<BigNMAdder>();
            bigNMAdder.SetNumMonsterAdder(_BigNMNavmeshPrefab, numMonsters, this);
            numMonsterAdders.Add(bigNMAdder);
            fastNMAdder = gameObject.AddComponent<FastNMAdder>();
            fastNMAdder.SetNumMonsterAdder(_FastNMNavmeshPrefab, numMonsters, this);
            numMonsterAdders.Add(fastNMAdder);
        }

        public void LinkToObject(GameSetUp.GameArgs args) {
            Debug.Log("NumMonsterGenerator link to player");
            this.player = args.player;
            gameObject.SendMessage("LinkToPlayer", player);
        }

        private void GameManager_OnGameStateChanged(GameState gameState) {
            if(gameState == GameState.GameOn) {
                timerOn = true;
            }
            if(gameState == GameState.Fail) {
                gameSetUp.GameObjectInitializeDone -= LinkToObject;
            }
        }

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        }
        private void Update() {
            //Time count down
            if (timerOn) {
                secondTimer -= Time.deltaTime;
                _CountText.text = ((int)(secondTimer+1)).ToString();
                if(_CountText.text == "0") {
                    AudioManager.AudioManagerInstance.PlayAudio("beep-1-sec", 0.6f, 1);
                }
                if (secondTimer < 0) {
                    Debug.Log("times up! Add Num Monster");
                    StartCoroutine(GenerateWave());
                }
            }

        }

        private IEnumerator GenerateWave() {
            timerOn = false;
            GenerateNewWave?.Invoke(waveNum);
            User.I.AddExperience(5);
            yield return StartCoroutine(OpenMouth());
            yield return StartCoroutine(GenerateMonsterWave(waveNum, round));
            yield return new WaitForSeconds(1f);
            yield return CloseMouth();
            if (waveNum % 5 == 0) {
                timeInterval = 60f;
            } else {
                timeInterval = 30f;
            }
            secondTimer = timeInterval;
            waveNum++;
            round = waveNum / 5 + 1;
            timerOn = true;
        }
        private IEnumerator GenerateMonsterWave(int waveNum, int round) {
            Debug.Log("create Monster: total Wave Num: " + waveNum + "round: " + round);
            foreach (NumMonsterAdder numMonsterAdder in numMonsterAdders) {
                yield return StartCoroutine(numMonsterAdder.CreateNumMonsterWave(waveNum, round));
            }
        }

        #region Factory Graphic Control
        private IEnumerator OpenMouth() {
            yield return StartCoroutine(UtilityMove.LerpLocalPosition(_FactoryDoor, _FactoryDoor.transform.localPosition, monsterMouthOpenPos, 1f));
            monsterComing = true;
        }
        private IEnumerator CloseMouth() {
            yield return StartCoroutine(UtilityMove.LerpLocalPosition(_FactoryDoor, _FactoryDoor.transform.localPosition, Vector3.zero, 1f));
        }

        #endregion
    }
}

