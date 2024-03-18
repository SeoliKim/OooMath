using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


namespace Calculation {
    public class RoundManager : MonoBehaviour {
        public static RoundManager RoundManagerInstance;

        public static event Action<RoundArgs> RoundUpdated;

        public class RoundArgs : EventArgs {
            public int RoundCount;
            public int HardLevel;
        }

        public static event Action<GameObject> GameIntialized;
        public static event Action GameDestroyed;

        [SerializeField]
        private GameObject _GameLevelPrefeb, _CalculationGamePrefab;
        public GameObject calculationGame;

        private static int roundCount;
        private static int hardLevel;

        // Start is called before the first frame update
        void Awake() {
            RoundManagerInstance = this;
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
                     
        }

        private void Start() {
            roundCount = User.I.GetCalcQuestion(GameManager.GameManagerInstance.gameIndex);
            roundCount = roundCount / 4 * 4; //start at the every forth round
            if (roundCount == 0) {
                roundCount = 1;
            } 
            hardLevel = 1;
        }


        private void GameManager_OnGameStateChanged(GameState gameState) {
            if (gameState == GameState.GetReady) {
                AudioManager.AudioManagerInstance.StopAll();
                Debug.Log("round manager receives get ready");
                InitializeGame(roundCount);
            }
            if(gameState == GameState.GameOn) {
                AudioManager.AudioManagerInstance.PlayMusic("theme");
            }
            if(gameState == GameState.LevelPass) {
                DestroyCurrentGame();
                roundCount++;
            }
            if (gameState == GameState.Fail) {
                AudioManager.AudioManagerInstance.Stop("theme");
                
            }
            Debug.Log("Game State Changed" + gameState);
        }

        public void InitializeGame(int roundCount) {
            hardLevel = roundCount / 12;
            RoundUpdated?.Invoke(new RoundArgs { RoundCount = roundCount, HardLevel = hardLevel });
            calculationGame = Instantiate(_CalculationGamePrefab, transform);
            calculationGame.GetComponent<GameSetUp>().SetGameSetUp(_GameLevelPrefeb, roundCount);
            calculationGame.name = _CalculationGamePrefab.name;
            GameIntialized?.Invoke(calculationGame);
        }

        public void DestroyCurrentGame() {
            foreach (Transform child in calculationGame.transform) {
                Destroy(child.gameObject);
            }
            Destroy(calculationGame);
            Debug.Log("Game destroyed, invoke event");
            AudioManager.AudioManagerInstance.Stop("theme");
            GameDestroyed?.Invoke();
        }

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        }
        public int GetRoundCount() {
            return roundCount;
        }
        public void SetRoundCount(int r) {
            roundCount = r;
        }

    }
}

