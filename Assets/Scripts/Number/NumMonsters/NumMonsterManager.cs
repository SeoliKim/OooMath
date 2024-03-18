using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Number {
    public class NumMonsterManager : MonoBehaviour {

        private NumMonsterGenerator numMonsterGenerator;
        public event Action<GameObject> MonsterHitByCorrectBubble;
        public event Action<GameObject> MonsterHitByWrongBubble;
        public event Action<GameObject> MonsterDie;

        private int[] NMroundRecord = new int[100000];
        public int[] GetNMroundRecord() {
            return NMroundRecord;
        }

        public void SaveNMToRecord(int count, int round) {
            NMroundRecord[round] += count;
        }

        private int currentRound;
        private int successfulRound;
        public int deadMonsterCount;
        private int leftNMForRound;
        

        private void Start() {
            GameManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }
        private void OnGameStateChanged(GameState state) {
            if(state == GameState.GameOn) {
                GameObject gameManager = GameManager.GameManagerInstance.gameObject;
                numMonsterGenerator = gameManager.GetComponentInChildren<NumMonsterGenerator>();
                successfulRound = User.I.GetNumMonsterRound(GameManager.GameManagerInstance.gameIndex);
                currentRound = numMonsterGenerator.round;
                numMonsterGenerator.FinishCurrentRound += FinishCurrentRound;
            }
            if (state == GameState.Fail) {
                UpdateGameRecord();
            }
        }
        private void FinishCurrentRound(int finishedRound) {
            currentRound = finishedRound;
            CheckRoundSuccess();
        }

        private void CheckRoundSuccess() {
            numMonsterGenerator.GenerateNewWave += GenerateNewWave;
            if (leftNMForRound >= NMroundRecord[successfulRound]) {
                leftNMForRound -= NMroundRecord[successfulRound];
                successfulRound++;
                User.I.AddExperience(10);
            }
        }

        private void GenerateNewWave(int waveNum) {
            CheckRoundSuccess();
        }


        private void UpdateGameRecord() {
            if (deadMonsterCount > User.I.GetNumMonsterKillMax(GameManager.GameManagerInstance.gameIndex)) {
                User.I.UpdateNumMonsterKillMax(GameManager.GameManagerInstance.gameIndex,deadMonsterCount);
            }
            if(successfulRound > User.I.GetNumMonsterRound(GameManager.GameManagerInstance.gameIndex)) {
                User.I.UpdateNumMonsterRound(GameManager.GameManagerInstance.gameIndex,successfulRound);
            }
        }

        

        #region individual monsters
        public void HitByCorrectBubble (GameObject monster) {
            MonsterHitByCorrectBubble?.Invoke(monster);
        }

        public void HitByWrongBubble(GameObject monster) {
            MonsterHitByWrongBubble?.Invoke(monster);
        }

        public void Die(GameObject monster) {
            Destroy(monster);
            deadMonsterCount++;
            User.I.AddDailyMonsterKill(GameManager.GameManagerInstance.gameIndex, 1);
            leftNMForRound++;
            MonsterDie?.Invoke(monster);
        }

        #endregion
    }
}
