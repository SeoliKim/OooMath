using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Number {
    public class NumberBomb : MonoBehaviour {

        [SerializeField] private Animator NullSetBombAnimator;

        //Function
        private List<GameObject> monstersInRange = new List<GameObject>();
        private GameObject bomb;
        private float bombTime;
        private bool bombing;

        private void Start() {
            bombTime = 1f;
            bombing = true;
        }

        private void Update() {
            if (bombing) {
                bombTime -= Time.deltaTime;
                if (bombTime < 0) {
                    bombing = false;
                    StartCoroutine(Bombing());
                }
            }
        }


        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Monster")) {
                monstersInRange.Add(other.gameObject);
            }
        }

        IEnumerator Bombing() {
            NullSetBombAnimator.Play("NullSetBombExplode");
            yield return new WaitForSeconds(1.5f);
            foreach (GameObject monster in monstersInRange) {
                Destroy(monster);
            }

            yield return new WaitForFixedUpdate();
            Destroy(gameObject);
        }
    }
}
