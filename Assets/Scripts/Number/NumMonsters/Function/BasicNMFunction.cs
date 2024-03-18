using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Number {
    public class BasicNMFunction : NumMonsterFunction {

        protected override void Awake() {
            attackBloodStrength = 2;
            color = renderers[0].material.GetColor("_Color");
        }

        protected override void HitResponse(GameObject hitBubble) {
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
