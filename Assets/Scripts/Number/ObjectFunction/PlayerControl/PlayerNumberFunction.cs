using UnityEngine;
using System;

namespace Number {
    public class PlayerNumberFunction : MonoBehaviour {

        public GameObject powerBubbleHolder, throwLine;
        private Thrower thrower;

        private bool GameOn;

        public float bloodValue;

        public event Action<GameObject> PlayerCollideMonster;
        public event Action<float> ChangePlayerBlood;


        private void FixedUpdate() {
            if (GameOn) {
                PlayerFallCheck();
                BloodValueCheck();
            }
        }

        public void SetPlayerNumberFunction(GameObject powerBubbleHolder, GameObject _throwLinePrefab) {
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
            this.powerBubbleHolder = powerBubbleHolder;
            throwLine = Instantiate(_throwLinePrefab, transform);
            throwLine.name = _throwLinePrefab.name;
            thrower = gameObject.AddComponent<Thrower>();
            thrower.shootrate = 50f;
            thrower.SetBubbleHolder(powerBubbleHolder);
            thrower.SetDrawTrajectory(throwLine.GetComponent<DrawTrajectory>());
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.collider.CompareTag("Monster")) {
                CollideWithMonster(collision.collider.gameObject);
            }
        }

        private void GameManager_OnGameStateChanged(GameState state) {
            if (state == GameState.GameOn) {
                GameOn = true;
                bloodValue = 100;
                ChangePlayerBlood?.Invoke(100f);
            }
            if(state == GameState.Fail) {
                GameOn= false;
            }
        }

        private void PlayerFallCheck() {
            if (gameObject.transform.position.y < -10) {
                GameManager.GameManagerInstance.UpdateGameState(GameState.Fail);
            }
        }

        private void CollideWithMonster(GameObject monster) {
            PlayerCollideMonster?.Invoke(monster);
            AudioManager.AudioManagerInstance.PlayAudio("loseBlood");
            float loseBlood = monster.GetComponent<NumMonsterFunction>().GetAttackBloodStrength();
            bloodValue -= loseBlood;
            ChangePlayerBlood?.Invoke(bloodValue);
        }

        public void ChangeBloodValue(float amount) {
            bloodValue = amount;
            ChangePlayerBlood?.Invoke(bloodValue);
        }
        private void BloodValueCheck() {
            if(bloodValue < 0) {
                GameManager.GameManagerInstance.UpdateGameState(GameState.Fail);
            }
        }

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        }
    }
}
