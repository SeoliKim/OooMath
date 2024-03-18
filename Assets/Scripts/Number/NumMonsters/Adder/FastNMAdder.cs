using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Number {
    public class FastNMAdder : NumMonsterAdder
    {
        public List<GameObject> bigNumMonsters = new List<GameObject>();

        protected override void InitializeMotion() {
            speed = 9f;
            accer = 20;
            angleAccer = 180;
        }

        protected override int CalculateMonsterCount(int waveNum, int round) {
            int waveInround = waveNum - (round - 1) * 5;
            int count = 0;
            if (round < 4) {
                return count;
            }
            if(round % 5 == 4) {//major
                int roundIncrease = round / 5;
                switch (waveInround) {
                    case 1:
                        count = 1 + roundIncrease;
                        break;
                    case 2:
                    case 4:
                        count = 0 + roundIncrease;
                        break;
                    case 3:
                        count = 2 + roundIncrease;
                        break;
                    case 0:
                        count = 3 + roundIncrease;
                        break;
                    default:
                        Debug.LogError("fail to generate new wave of fast NM, wrong waveNum" + waveNum + "in round: " + round);
                        break;
                }
            if (round % 5 == 0|| round % 5 == 2) { //minor
                    switch (waveInround) {
                        case 1:
                        case 3:
                        case 0:
                            count = 0;
                            break;
                        case 2:
                        case 4:
                            count = round/5 +1;
                            break;
                        default:
                            Debug.LogError("fail to generate new wave of fast NM, wrong waveNum" + waveNum + "in round: " + round);
                            break;
                    }
                }
            }

            return count;
        }

        protected override void AssignAI(GameObject numMonster) {
            AIFollow aIFollow = numMonster.AddComponent<AIFollow>();
            aIFollow.SetAIFollow(base.player);
        }

        protected override void AssignMotion(GameObject numMonster, int roundNum) {
            speedLevel = CalculateSpeedLevel(roundNum);
            numMonster.GetComponent<NavMeshAgent>().speed = speed * speedLevel;
            numMonster.GetComponent<NavMeshAgent>().acceleration = accer * speedLevel;
            numMonster.GetComponent<NavMeshAgent>().angularSpeed = angleAccer * speedLevel;
        }

        protected override int CalculateSpeedLevel(int roundNum) {
            return (int)(roundNum / 4);
        }

    }
}
