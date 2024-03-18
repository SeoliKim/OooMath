using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class RecMonsterFunction : MonoBehaviour {
        // Start is called before the first frame update

        private GameObject player;

        private void Start() {
            player= transform.parent.GetComponent<GameSetUp>().player;
        }
        private void OnCollisionStay(Collision collision) {
            if (collision.collider.CompareTag("Obstacle")) {
                GameObject obstacle = collision.collider.gameObject;
                obstacle.transform.position += obstacle.transform.right*2;
            }
        }

        IEnumerator TransportObstacle(GameObject obstacle) {
            yield return new WaitForSeconds(0.05f);
            Vector3 position = player.transform.position;
            yield return new WaitForSeconds(0.5f);
            this.gameObject.transform.position = position;
        }
    }
}
