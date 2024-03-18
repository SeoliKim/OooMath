using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Calculation {
    public class RecMonster1Function : RecMonsterAdvancedFunction {
        [SerializeField] private NavMeshAgent navMeshAgent;
        private bool move;
        private float speedTime;
        private float regularTime;

        public override void SetAdvanced(GameObject player) {
            move = true;
            speedTime = 3;
            regularTime = 10;
            StartCoroutine(SpeedUp());
        }


        IEnumerator SpeedUp() {
            while (move) {
                navMeshAgent.speed *= 1.3f;
                yield return new WaitForSecondsRealtime(speedTime);
                navMeshAgent.speed /= 1.3f;
                yield return new WaitForSecondsRealtime(regularTime);
            }
        }

        private void OnTriggerStay(Collider other) {
            if (other.CompareTag("Monster")) {
                Transform m = transform.parent;
                float x = m.position.x;
                float z = m.position.z;
                m.position = new Vector3(x, 5, z);

            }
        }
    }

    
}
