using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class DataRecorder : MonoBehaviour {
        [SerializeField] private FailMenu failMenu;

        private int initialMaxQ, initialXP, currentRound;
        private int initialLv;
        private int gainedXP;
        public int GetGainedXP() {
            return gainedXP;
        }

        private int winStrike;
        public int GetWinStrike() {
            return winStrike;
        }

        private void Start() {
            initialMaxQ = User.I.GetCalcQuestion(GameManager.GameManagerInstance.gameIndex);
            initialXP = User.I.GetExperience();
            initialLv = User.I.GetLv();
            winStrike = 0;
            gainedXP = 0;
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
            RoundManager.RoundUpdated += RoundUpdated;
        }

        private void RoundUpdated(RoundManager.RoundArgs args) {
            if (args.RoundCount > User.I.GetCalcQuestion(GameManager.GameManagerInstance.gameIndex)) {
                User.I.UpdateCalcQuestion(GameManager.GameManagerInstance.gameIndex,args.RoundCount);
            }
            currentRound = args.RoundCount;
        }

        private void GameManager_OnGameStateChanged(GameState gameState) {
            if (gameState == GameState.LevelPass) {
                User.I.AddTotalCalcGame(1);
                User.I.AddDailyCalcGameTotal(GameManager.GameManagerInstance.gameIndex, 1);
                winStrike++;
                int passRound = RoundManager.RoundManagerInstance.GetRoundCount();
                AddXP(passRound);

            }
            if (gameState == GameState.Fail) {
                failMenu.SetFailMenu(initialMaxQ, currentRound, initialXP, initialLv, winStrike);
                
                if (initialMaxQ < currentRound) {
                    User.I.AddDailyCalcRecordBreak(GameManager.GameManagerInstance.gameIndex, 1);
                }
                User.I.UpdateDailyCalcAnswerStrike(GameManager.GameManagerInstance.gameIndex, winStrike);
            }
        }

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
            RoundManager.RoundUpdated -= RoundUpdated;
        }

        private void AddXP(int passRound) {
            int addXP = (passRound) * (int)Mathf.Pow(2, winStrike);
            if(passRound > User.I.GetCalcQuestion(GameManager.GameManagerInstance.gameIndex)) {
                addXP *= 10;
            }
            gainedXP += addXP;
            User.I.AddExperience(addXP);
        }


    }
}
