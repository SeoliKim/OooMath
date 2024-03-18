using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Number {
    public class BasicNumMonsterAdder : NumMonsterAdder {
        public List<GameObject> basicNumMonsters = new List<GameObject>();
        private float speedMax, speedMin;

        protected override void InitializeMotion() {
            speedMax = 2;
            speedMin = .5f;
            accer = 5;
            angleAccer = 60;

        }

        protected override int CalculateMonsterCount(int waveNum, int round) {
            int waveInround = waveNum - (round - 1) * 5;
            int count = 0;
            int roundIncrease = round - 1;
            switch (waveInround) {
                case 1:
                    count = 3 + roundIncrease;
                    break;
                case 2:
                case 4:
                    count = 1 + roundIncrease;
                    break;
                case 3:
                    count = 2 + roundIncrease;
                    break;
                case 0:
                    count = 5 + roundIncrease;
                    break;
                default:
                    Debug.LogError("fail to generate new wave of basic NM, wrong waveNum" + waveNum + "in round: " + round);
                    break;
            }
            return count;
        }

        protected override void AssignAI(GameObject numMonster) {
            AIFollow aIFollow = numMonster.AddComponent<AIFollow>();
            aIFollow.SetAIFollow(base.player);
        }

        protected override void AssignMotion(GameObject numMonster, int waveNum) {
            int round = waveNum / 5 +1;
            speedLevel = CalculateSpeedLevel(round);
            numMonster.GetComponent<NavMeshAgent>().speed = CalcaculateSpeed(speedLevel);
            numMonster.GetComponent<NavMeshAgent>().acceleration = accer+speedLevel;
            numMonster.GetComponent<NavMeshAgent>().angularSpeed = angleAccer;
        }

        protected override int CalculateSpeedLevel(int round) {
            return (int) (round/2);
        }

        protected float CalcaculateSpeed(int speedLevel) {
            speedMin = speedMin + speedLevel*0.2f;
            speedMax = speedMax + speedLevel*0.2f;
            float speed = Random.Range(speedMin, speedMax);
            return speed;
        }
    }
}
