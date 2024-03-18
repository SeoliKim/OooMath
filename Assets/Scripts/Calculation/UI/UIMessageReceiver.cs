using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Calculation {
    public class UIMessageReceiver : MonoBehaviour {

        public GameObject calculationGame;
        private PlayerMathGame playerMathGame;
        public GameState GameState;
        public float roundCount, hardLevel;
        public bool gameSetUp;
        public string equation;

        private void Awake() {
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
            RoundManager.RoundUpdated += RoundManager_RoundUpdated;
            RoundManager.GameIntialized += RoundManager_GameIntialized;
            RoundManager.GameDestroyed += GameDestroyed;
        }

        

        private void Update() {
            CheckMoneyMax();
        }
        private void GameManager_OnGameStateChanged(GameState gameState) {
            if(gameState == GameState.GameOn) {
               
                playerMathGame = this.calculationGame.GetComponentInChildren<PlayerMathGame>();
                playerMathGame.CollideWithMoney += CollideWithMoney;
            }
           if(gameState == GameState.LevelPass) {
                if(roundCount%3 == 0) {
                    SaveMoneyToUser();
                }
                playerMathGame.CollideWithMoney -= CollideWithMoney;
            }
            if (gameState == GameState.Fail) {
                playerMathGame.CollideWithMoney -= CollideWithMoney;
            }
        }

        private void RoundManager_GameIntialized(GameObject calculationGame) {
            this.calculationGame = calculationGame;
            this.calculationGame.GetComponent<GameSetUp>().GameObjectInitializeDone += GameObjectInitializeDone;
            this.calculationGame.GetComponentInChildren<Math_CalculationLevel>().MathProblemSetDone += MathProbalemDone;
        }

        private void GameObjectInitializeDone(GameSetUp.GameArgs obj) {
            gameSetUp = true;
            this.calculationGame.GetComponent<GameSetUp>().GameObjectInitializeDone -= GameObjectInitializeDone;
        }

        private void GameDestroyed() {
            gameSetUp = false;
        }
        private void MathProbalemDone(Math_CalculationLevel.MathProblemArgs mathProblemArgs) {
            equation = mathProblemArgs.equationA;
            calculationGame.GetComponentInChildren<Math_CalculationLevel>().MathProblemSetDone -= MathProbalemDone;
        }

        private void RoundManager_RoundUpdated(RoundManager.RoundArgs roundArgs) {
            roundCount = roundArgs.RoundCount;
            hardLevel = roundArgs.HardLevel;
        }


        private void OnDestroy() {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
            RoundManager.RoundUpdated -= RoundManager_RoundUpdated;
            RoundManager.GameIntialized -= RoundManager_GameIntialized;
        }

        #region GameInfo-money

        [Header("GameInfo-money")]
        [SerializeField] private TMP_Text _MoneyCount;
        public int moneyCount;

        private void CollideWithMoney(GameObject money) {
            AddMoney();
        }

        public void AddMoney() {
            moneyCount++;
            _MoneyCount.text = moneyCount.ToString();
        }

        private void CheckMoneyMax() {
            if(moneyCount > 12) {
                calculationGame.GetComponent<MoneyAdder>().enabled = false;
            }
        }

        private void SaveMoneyToUser() {
            PlayFabManager.PlayFabManagerInstance.AddCP(moneyCount);
        }

        #endregion

    }
}
