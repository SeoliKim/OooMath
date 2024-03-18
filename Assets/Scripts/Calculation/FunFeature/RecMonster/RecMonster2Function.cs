using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Calculation {
    public class RecMonster2Function : RecMonsterAdvancedFunction {

        private GameObject player;

        private float timeInterval=12f;
        private float timer;
        private bool timerOn;

        public override void SetAdvanced(GameObject player) {
            this.player = player;
            GetReady();
            
        }

       
        private void GetReady() {
            timerOn = false;
            timer = this.timeInterval;
        }

        private void GameManager_OnGameStateChanged(GameState gameState) {
            if (gameState == GameState.GameOn) {
                timerOn = true;
            }
        }

        private void Update() {
            if (timerOn) {
                timeInterval -= Time.deltaTime;
            }
            if (timeInterval < 0) {
                Vector3 surfacePostion = GetFocusPoint(player.transform.position, player.GetComponent<Rigidbody>().velocity);
                transform.position = surfacePostion;
                timeInterval = timer;
            }
        }
        
        private Vector3 GetFocusPoint(Vector3 playerPosition, Vector3 playervelocity) {
            Vector3 finalPosition = playerPosition + playervelocity * 0.05f;
            return finalPosition;
        }
    }
}
