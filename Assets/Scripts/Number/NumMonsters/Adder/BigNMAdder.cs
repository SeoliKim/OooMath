using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Number {
    public class BigNMAdder : NumMonsterAdder {
        public List<GameObject> bigNumMonsters = new List<GameObject>();

        protected override void InitializeMotion() {
            speed = 1f;
            accer = 3;
            angleAccer = 60;
        }

        protected override int CalculateMonsterCount(int waveNum, int round) {
            int waveInround = waveNum - (round - 1) * 5;
            int count = 0;
            if(round < 3) {
                return count;
            }
            if(round%3 ==2) {
                return count;
            }
            int roundIncrease = (round - 3)/3;
            switch (waveInround) {
                case 1:
                    count = 2 + roundIncrease;
                    break;
                case 2:
                case 4:
                    count = 1 + roundIncrease;
                    break;
                case 3:
                    count = 0;
                    break;
                case 0:
                    count = 3 + roundIncrease;
                    break;
                default:
                    Debug.LogError("fail to generate new wave of big NM, wrong waveNum" + waveNum + "in round: " + round);
                    break;
            }
            return count;
        }

        protected override void AssignAI(GameObject numMonster) {
            AIFollow aIFollow = numMonster.AddComponent<AIFollow>();
            aIFollow.SetAIFollow(base.player);
        }

        protected override void AssignMotion(GameObject numMonster, int roundNum) {
            speedLevel = CalculateSpeedLevel(roundNum);
            numMonster.GetComponent<NavMeshAgent>().speed = speed*Mathf.Pow(1.2f, speedLevel);
            numMonster.GetComponent<NavMeshAgent>().acceleration = accer* Mathf.Pow(1.2f, speedLevel);
            numMonster.GetComponent<NavMeshAgent>().angularSpeed = angleAccer*Mathf.Pow(1.2f, speedLevel);
        }

        protected override int CalculateSpeedLevel(int roundNum) {
            return (int)(roundNum / 5);
        }


    }
}
