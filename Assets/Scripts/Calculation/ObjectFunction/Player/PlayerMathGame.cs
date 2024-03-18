using System;
using UnityEngine;

namespace Calculation {
    public class PlayerMathGame : MonoBehaviour {


        private bool ifCollision;
        private float collisionTime;

        private Rigidbody rb;
        private GameObject claw;

        private GameObject mainCanvas;


        public event Action<GameObject> MonsterCatchPlayer;
        public event Action PlayerFallFromPlatform;
        public event Action<GameObject> CollideWithMoney;





        private void FixedUpdate() {
            if (ifCollision) {
                collisionTime += Time.deltaTime;

            }

            //fall from platform
            if (transform.position.y < -3) {
                PlayerFallFromPlatform?.Invoke();
            }
        }

        private void OnCollisionEnter(Collision collision) {
            if(GameManager.GameManagerInstance.State == GameState.GameOn) {
                AudioManager.AudioManagerInstance.PlayAudio("tap");
            }
        }
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Money")) {
                CollideWithMoney?.Invoke(other.gameObject);
            }
        }

        private void OnCollisionStay(Collision collision) {
            ifCollision = true;


            if (collision.collider.CompareTag("Monster")) {
                GameObject monster = collision.collider.gameObject;
                MonsterCatchPlayer?.Invoke(monster);
               
            }

            /*
        if (collision.collider.CompareTag("")) {
            GameObject monster = collision.collider.gameObject;
            MonsterCatchPlayer?.Invoke(monster);
            Debug.Log("RecMonsterCatchPlayer event is invoked");
        }
            */
            //}
        }

        private void OnCollisionExit(Collision collision) {
            ifCollision = false;
            collisionTime = 0;
        }
    }
}

