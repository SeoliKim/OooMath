using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Number {
    public class BigNMFunction : NumMonsterFunction {

        private int defenseCount;

        protected override void Awake() {
            defenseCount = 10;
            attackBloodStrength = 10;
            color = renderers[0].material.GetColor("_Color");
        }

        protected override void HitResponse(GameObject hitBubble) {
            defenseCount--;
            int randomBool = NumberGenerator.getRandomNumber(0, 1);
            int shift = 0;
            if(randomBool == 0) {
                shift = 1;
            } else {
                shift = -1;
            }
            LeanTween.moveX(gameObject, transform.position.x + shift, .5f);

            if(defenseCount == 1) {
                bool correct = numberType_Level.CheckHit(this.gameObject, hitBubble);
                if (correct) {
                    numMonsterManager.HitByCorrectBubble(this.gameObject);
                    numMonsterManager.Die(this.gameObject);
                    Destroy(gameObject);
                } else {
                    numMonsterManager.HitByWrongBubble(this.gameObject);
                    Vector3 pos1 = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                    Vector3 pos2 = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                    StartCoroutine(numMonsterGenerator.basicNumMonsterAdder.AddNumMonster(pos1, 1, numMonsterGenerator.waveNum));
                    StartCoroutine(numMonsterGenerator.basicNumMonsterAdder.AddNumMonster(pos2, 1, numMonsterGenerator.waveNum));
                }
            }
        }
    }
}
