using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Calculation {
    public class WinLoseState : MonoBehaviour {
        private float x;
        private GameObject player;
        private PlayerMathGame playerMathGame;
        private ClawControler clawControler;

        private GameObject endGameObject;
        private Vector3 playerlastPos;
        private bool onEndAnim;

        private void Awake() {
            Debug.Log("WinLoseState Awake");
            GameManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void LateUpdate() {
            if (onEndAnim) {
                if(player!= null) {
                    player.transform.position = playerlastPos;
                    player.transform.rotation = Quaternion.Euler(new Vector3(-7f, 180f, 0));
                }
                if (endGameObject != null) {
                    endGameObject.transform.localPosition = new Vector3(0.7f, 0, 0);
                    endGameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
                }
            }
        }

        private void OnGameStateChanged(GameState gameState) {
            if(gameState == GameState.GameOn) {
                GameSetUp gameSetUp = gameObject.GetComponent<GameSetUp>();
                this.x = gameSetUp.x;
                player = gameSetUp.player;
                playerMathGame = player.GetComponent<PlayerMathGame>();
                playerMathGame.MonsterCatchPlayer += MonsterCatchPlayer;
                playerMathGame.PlayerFallFromPlatform += PlayerFallFromPlatform;
                clawControler = player.GetComponent<ClawControler>();
                clawControler.ReceiveBallNum += ReceiveBallNum;
            }
        }
        private void OnDestroy() {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        private void PlayerFallFromPlatform() {
            if (GameManager.GameManagerInstance.State == GameState.GameOn) {
                GameFail();
            }
        }

        private void MonsterCatchPlayer(GameObject obj) {
            if (GameManager.GameManagerInstance.State == GameState.GameOn) {
                endGameObject = obj;
                GameFail();
            }
            
        }

        private void ReceiveBallNum(GameObject grabBallNum) {
            float numberLabel = grabBallNum.GetComponent<BallNumVariable>().numberLabel;
            endGameObject = grabBallNum;
            if (numberLabel == x) {
                GamePass();
            } else {
                GameFail();
            }
        }

        private void GamePass() {
            StartCoroutine(EndAnimation(true));
            BroadcastMessage("GamePassResponse", SendMessageOptions.DontRequireReceiver);
        }

        private void GameFail() {
            StartCoroutine(EndAnimation(false));
            BroadcastMessage("GameFailResponse", SendMessageOptions.DontRequireReceiver);
        }

        private IEnumerator EndAnimation(bool win) {
            onEndAnim = true;
            if (player!= null) {
                player.GetComponent<Player3DMover>().enabled = false;
                playerlastPos = player.transform.position;
            }
            if(endGameObject != null) {
                endGameObject.transform.parent = player.transform;
            }
            playerMathGame.PlayerFallFromPlatform -= PlayerFallFromPlatform;
            playerMathGame.MonsterCatchPlayer -= MonsterCatchPlayer;
            clawControler.ReceiveBallNum -= ReceiveBallNum;
            yield return new WaitForSeconds(1f);
            if (win) {
                AudioManager.AudioManagerInstance.PlayAudio("child-yes");
                yield return new WaitForSeconds(2f);
                GameManager.GameManagerInstance.UpdateGameState(GameState.LevelPass);
            } else {
                yield return new WaitForSeconds(1f);
                AudioManager.AudioManagerInstance.PlayAudio("child-Ohoh");
                yield return new WaitForSeconds(2f);
                GameManager.GameManagerInstance.UpdateGameState(GameState.Fail);
            }
        }
    }
}
